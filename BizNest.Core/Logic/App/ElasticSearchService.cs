using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BizNest.Core.Common;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.Domain.Search.App;
using BizNest.Core.Domain.Search.Form;
using BizNest.Core.Domain.Search.Model;
using BizNest.Core.Logic.Definations;
using Nest;

namespace BizNest.Core.Logic.App
{
    public class ElasticSearchService : ISearchService
    {
        ElasticClient client;
        public ElasticSearchService()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("biznest"); 
            client = new ElasticClient(settings);
        }

        public async Task IndexBusiness(Business model)
        {
            var esmodel = DataMapper.Map<BusinessIndexModel, Business>(model);
            esmodel.Fullname = esmodel.Name;
            esmodel.Name += " " + Util.Reverse(esmodel.Name);
            await client.IndexDocumentAsync(esmodel);
        }

        public  void IndexBusinessSync(Business model)
        {
            var esmodel = DataMapper.Map<BusinessIndexModel, Business>(model);
            esmodel.Fullname = esmodel.Name;
            esmodel.Name += " " + Util.Reverse(esmodel.Name);
            esmodel.Name = Util.CleanText(esmodel.Name);
            client.IndexDocument(esmodel);
        }

        private string CleanBusinessName(string txt)
        {
            var k = new StringBuilder(txt.ToLower().Trim());
            k.Replace("limited", "");
            k.Replace("ltd", "");
            k.Replace("llc", "");
            k.Replace("plc", "");
            k.Replace("inc", "");
            k.Replace("incorporated", "");
            k.Replace("company", ""); 
            k.Replace("group", ""); 
            k.Replace("groups", "");
            return k.ToString();
        }

        public async Task<SearchResult> SearchBusinessPro(string name, int countryId = 0, int fuzz = 0, int page = 1, int pageSize = 10)
        {

            name = CleanBusinessName(name);

            var resp = new SearchResult();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:9200");
                HttpResponseMessage responseMessage = null; 
                var form = new SearchForm();
                form.SetName(name, fuzz);

                var model = new SearchModel();
                HttpContent contentPost = new StringContent(Util.SerializeJSON(form), Encoding.UTF8, "application/json");
                responseMessage = await client.PostAsync("/biznest/business/_search", contentPost);
                if (responseMessage.IsSuccessStatusCode)
                {
                    var txt = await responseMessage.Content.ReadAsStringAsync();
                    model = Util.DeserializeJSON<SearchModel>(txt); 
                    resp.SearchTime = TimeSpan.FromMilliseconds(model.took);
                    resp.Results = new List<SearchItem>();

                    if (!model.timed_out)
                    {
                        resp.Summary = "Maximum Score: " + model.hits.max_score;
                        resp.MaxHit = model.hits.max_score;
                        foreach (var m in model.hits.hits)
                        {
                            resp.Results.Add(new SearchItem()
                            {
                                Id = m._source.id,
                                Word = m._source.fullname,
                                MatchPercentage = m._score
                            });
                        }
                    } 
                }
            }
            catch { }

            return resp;
        }
         
        public async Task InsertBusinessNamesAsync(params Business[] businesses)
        {
            foreach(var b in businesses)
            {
                await IndexBusiness(b);
            }
        }

        public Task InsertProhibitedNameAsync(params ProhibitedName[] words)
        {
            throw new System.NotImplementedException();
        }

        public async Task RemoveBusinessNameAsync(Business model)
        {
            var esmodel = DataMapper.Map<BusinessIndexModel, Business>(model);
            await client.DeleteAsync<BusinessIndexModel>(esmodel);
        }

        public Task RemoveProhibitedNameAsync(ProhibitedName name)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SearchResult> SearchAsync(string query)
        {
            var p1 = await SearchBusinessPro(query, 0, 0);
            var p2 = await SearchBusinessPro(query, 0, 1);

            p1.MaxHitExtra = p2.MaxHit;
            p1.ResultsExtra = p2.Results;

            return p1;

        }
    }
}