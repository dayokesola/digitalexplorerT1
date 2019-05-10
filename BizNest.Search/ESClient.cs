using BizNest.Core.Common;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.Domain.Search.App;
using BizNest.Search.Form;
using BizNest.Search.Model;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace BizNest.Search
{
    public class SearchClient
    {
        ElasticClient client;
        public SearchClient()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("biznest");

            client = new ElasticClient(settings);
        }

        public IIndexResponse IndexBusiness(BusinessModel model)
        {
            var esmodel = DataMapper.Map<BusinessIndexModel, BusinessModel>(model);

            return client.IndexDocument(esmodel);
        }

 


        private QueryContainer SearchBusinessQueryFilter(string name, int countryId)
        {
            QueryContainer queryFilter = new QueryContainer(); 
            if (countryId > 0)
            {
                queryFilter &= Query<BusinessIndexModel>.Term(f => f.AddressCountryId, countryId);
            }
            if (!string.IsNullOrEmpty(name))
            {
                queryFilter &= Query<BusinessIndexModel>.Regexp(f => f.Field(g => g.Name).Value(searchLike(name)));
            } 
            return queryFilter;
        } 

        private string searchLike(string txt)
        {
            return ".*" + txt.ToLower().Trim() + ".*";
        }


        public IEnumerable<BusinessIndexModel> SearchBusiness(string name, int countryId = 0, int page = 1, int pageSize = 10)
        {
            int offset = pageSize * (page - 1); 
            var query = new SearchDescriptor<BusinessIndexModel>();
            var filter = SearchBusinessQueryFilter(name, countryId);
            query.Query(q => filter);
            //query.Type<BusinessIndexModel>();
            //query.TypedKeys(false);
            query.From(offset);
            query.Size(pageSize);
            query.Sort(f => f.Descending(x => x.Name));
            ISearchResponse<BusinessIndexModel> sr = client.Search<BusinessIndexModel>(query);
            return sr.Documents;
        }

        public BusinessSearchModel SearchBusinessPro(string name, int countryId = 0, int page = 1, int pageSize = 10)
        {
            var resp = new BusinessSearchModel();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:9200");
                HttpResponseMessage responseMessage = null;

                var form = new SearchForm();
                form.SetName(name);

                var model = new SearchModel();
                HttpContent contentPost = new StringContent(Util.SerializeJSON(form), Encoding.UTF8, "application/json");
                responseMessage = client.PostAsync("/biznest/business/_search", contentPost).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var txt = responseMessage.Content.ReadAsStringAsync().Result;
                    model = Util.DeserializeJSON<SearchModel>(txt);

                    resp.Time = model.took;
                    resp.Results = new List<BusinessNameSearchModel>();

                    if (!model.timed_out)
                    {
                        resp.MaxScore = model.hits.max_score;
                        foreach (var m in model.hits.hits)
                        {
                            resp.Results.Add(new BusinessNameSearchModel()
                            {
                                Id = m._source.id,
                                Name = m._source.name,
                                Match = m._score
                            });
                        }
                    }

                }
            }
            catch { }

            return resp;
        }

    }
}
