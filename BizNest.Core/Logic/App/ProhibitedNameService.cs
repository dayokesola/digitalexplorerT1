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

        public async Task<List<ProhibitedName>> SearchForWords(string query,int max)
        {
            max = Math.Min(20,max);
            return await repo.SearchForWord(query,max);
        }

         public async Task InsertWord (ProhibitedWordModel model)
         {
              var item = DataMapper.Map<ProhibitedName,ProhibitedWordModel>(model);
              await Task.Run(()=> repo.Insert(new ProhibitedName{ Word = model.Word }));
              await searchService.InsertProhibitedNameAsync(model.Word);
         }

         public async Task UpdateWord (ProhibitedWordModel model) 
         {
             var item = repo.Query().Where(x=>x.Id == model.Id).FirstOrDefault();
             await searchService.RemoveProhibitedNameAsync(item.Word);
             item.Word = model.Word;
             await Task.Run(()=> repo.Update(item));
              await searchService.InsertProhibitedNameAsync(model.Word);
         }

         public async Task DeleteWord(ProhibitedWordModel model)
         {
             var item = DataMapper.Map<ProhibitedName,ProhibitedWordModel>(model);
             await Task.Run(()=> repo.Delete(item));
             await searchService.RemoveProhibitedNameAsync(model.Word);
         }
    }
}