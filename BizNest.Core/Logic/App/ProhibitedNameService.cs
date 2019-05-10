using BizNest.Core.Data.Repository.App;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Data.DB;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;
using System;
using BizNest.Core.Common;
using System.Linq;
using BizNest.Core.Domain.Form;
using BizNest.Core.Domain.Form.App;
using System.Linq;


namespace BizNest.Core.Logic.App
{
    public class ProhibitedNameService : IProhibitedService
    {
        ProhibitedNameRepository repo;
        ISearchService searchService;
        public ProhibitedNameService(AppDbContext context, ISearchService searchService)
        {
            repo = new ProhibitedNameRepository(context);
            this.searchService = searchService;
        }

        public async Task<BaseDataModel<ProhibitedForm>> SearchForWords(string query,int skip,int max)
        {
            query = query.ToLower();
            max = Math.Min(20,max);
            return new BaseDataModel<ProhibitedForm>
            {
                 Items = repo.SearchForWord(query).Skip(skip).Take(max).Select(x=> new ProhibitedForm{ Id = x.Id, Word = x.Word }).ToList(),
                 Total = repo.SearchForWord(query).Count()
            };
        }

         public async Task InsertWord (ProhibitedWordModel model)
         {
              if(await repo.WordExists(model.Word)) throw new ArgumentException("This word already exists");
              var item = DataMapper.Map<ProhibitedName,ProhibitedWordModel>(model);
              var xr = await Task.Run(()=> repo.Insert(new ProhibitedName{ Word = model.Word }));
              await searchService.InsertProhibitedNameAsync(xr);
         }

         public async Task UpdateWord (ProhibitedWordModel model) 
         {
             var item = repo.Query().Where(x=>x.Id == model.Id).FirstOrDefault();
             await searchService.RemoveProhibitedNameAsync(item);
             item.Word = model.Word;
             await Task.Run(()=> repo.Update(item));
              await searchService.InsertProhibitedNameAsync(new ProhibitedName{ Id = item.Id, Word = item.Word });
         }

         public async Task DeleteWord(ProhibitedWordModel model)
         {
             var item = DataMapper.Map<ProhibitedName,ProhibitedWordModel>(model);
             await Task.Run(()=> repo.Delete(item));
             await searchService.RemoveProhibitedNameAsync(item);
         }
    }
}