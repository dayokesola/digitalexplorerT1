using BizNest.Core.Data.Repository.App;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Data.DB;
using System.Threading.Tasks;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System.Collections.Generic;
using System;


namespace BizNest.Core.Logic.App
{
    public class ProhibitedNameService : IProhibitedService
    {
        ProhibitedNameRepository repo;
        public ProhibitedNameService(AppDbContext context)
        {
            repo = new ProhibitedNameRepository(context);
        }

        public async Task<List<ProhibitedName>> SearchForWords(string query,int max)
        {
            max = Math.Min(20,max);
            return await repo.SearchForWord(query,max);
        }

         public async Task InsertWord (ProhibitedWordModel model)=> await Task.Run(()=> repo.Insert(new ProhibitedName{ Word = model.Word }));

         public async Task UpdateWord (ProhibitedWordModel model)=>await Task.Run(()=> repo.Update(new ProhibitedName{ Word = model.Word , Id = model.Id }));

         public async Task DeleteWord(ProhibitedWordModel model)
         {

         }
    }
}