using BizNest.Core.Common;
using BizNest.Core.Domain.Model.App;
using BizNest.Search;
using System;

namespace BizNest.Tinker
{
    class Program
    {
        static void Main(string[] args)
        {
            AutoMapperConfig.RegisterMappings();
            var biz1 = new BusinessModel()
            {
                Id = 1,
                Name = "Flutterwave Inc"
            };
            var biz2 = new BusinessModel()
            {
                Id = 2,
                Name = "Interswitch Limited"
            };

            var srv = new SearchClient();
            //var j1 = srv.IndexBusiness(biz1);
            //var j2 = srv.IndexBusiness(biz2);


            var data = srv.SearchBusinessPro("sweet inter");

            Console.WriteLine("Hello World!");
        }
    }
}
