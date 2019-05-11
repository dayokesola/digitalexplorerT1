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
    public class StakeHolderRepository : BaseRepository<StakeHolder, StakeHolderModel, long>
    {
        public StakeHolderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
