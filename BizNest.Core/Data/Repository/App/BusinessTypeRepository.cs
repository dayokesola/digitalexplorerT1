using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model.App;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BizNest.Core.Data.Repository.App
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessTypeRepository : BaseRepository<BusinessType, BusinessTypeModel, int>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BusinessTypeRepository(AppDbContext context) : base(context)
        {
        }
        /// <summary>
        /// IQueryable BusinessType Entity Search
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public IQueryable<BusinessType> Search(string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "")
        {
            var table = Query();
            if (!string.IsNullOrEmpty(name))
            {
                table = table.Where(x => x.Name == name);
            }
            if (minStakeHolder > 0)
            {
                table = table.Where(x => x.MinStakeHolder == minStakeHolder);
            }
            if (maxStakeHolder > 0)
            {
                table = table.Where(x => x.MaxStakeHolder == maxStakeHolder);
            }
            if (minCapital > 0)
            {
                table = table.Where(x => x.MinCapital == minCapital);
            }
            if (!string.IsNullOrEmpty(info))
            {
                table = table.Where(x => x.Info == info);
            }

            return table;
        }

    
     

        /// <summary>
        /// Check exists
        /// </summary>
        /// <param name="name"></param>
        /// <param name="minStakeHolder"></param>
        /// <param name="maxStakeHolder"></param>
        /// <param name="minCapital"></param>
        /// <param name="info"></param>
        /// 
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ItemExists(string name = "", int minStakeHolder = 0, int maxStakeHolder = 0, decimal minCapital = 0, string info = "", int Id = 0)
        {
            var check = Search(name, minStakeHolder, maxStakeHolder, minCapital, info);
            if (Id > 0)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }

        /// <summary>
        /// check exists
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool ItemExists(BusinessTypeModel model, int Id = 0)
        {
            var check = Search(model.Name, model.MinStakeHolder, model.MaxStakeHolder, model.MinCapital, model.Info);
            if (Id > 0)
            {
                check = check.Where(x => x.Id != Id);
            }
            return check.Any();
        }
    }

}
