using BizNest.Core.Common;
using NPoco;

namespace BizNest.Core.Data.DB
{
    public static class AppPoco
    {
        /// <summary>
        /// 
        /// </summary>
        public static DatabaseFactory DbFactory { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static void Setup()
        { 
            DbFactory = DatabaseFactory.Config(x =>
            {
                //x.UsingDatabase(() => new NPoco.Database(new ));  
            });
        }
    }
}
