using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model;
using BizNest.Core.Domain.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BizNest.Core.Data.Repository.App
{
    public class ProhibitedNameRepository : BaseRepository<ProhibitedName, ProhibitedWordModel, long>
    {
        public ProhibitedNameRepository(AppDbContext context) : base(context)
        {
        
        }

        public async Task<bool> WordExists(string word)
        {
            var fmtedWrd = word.ToLower();
            var num = await Task.Run(()=>Query().Count(x=>x.Word.ToLower().Contains(fmtedWrd)));
            return num > 0;

        }


        public IQueryable<ProhibitedName> SearchForWord(string word)
        {
            var fmtedWrd = word.ToLower();
            return Query().Where(x=>x.Word.ToLower().Contains(fmtedWrd));;
        }
    }
}