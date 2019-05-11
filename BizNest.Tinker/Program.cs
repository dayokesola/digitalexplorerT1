using BizNest.Core.Common;
using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model.App;
using BizNest.Core.Logic.App;
using BizNest.Search;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BizNest.Tinker
{
    class Program
    {
        private static AppDbContext _context;
        private static DbContextOptionsBuilder<AppDbContext> _optionsBuilder;
        static void Main(string[] args)
        {
            _optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            _optionsBuilder.UseSqlServer("Server=.;Initial Catalog=BizNestDb;User ID=appusr;Password=appusr;MultipleActiveResultSets=False;");
            _context = new AppDbContext(_optionsBuilder.Options);
          

            AutoMapperConfig.RegisterMappings();

            var filepath = @"C:\tmp\dataset.csv";

            LoadDataInSQLServer(filepath);
            //IndexDataInElasticClient().Wait();

            var srv = new SearchClient();
            //var j1 = srv.IndexBusiness(biz1);
            //var j2 = srv.IndexBusiness(biz2);


            //var data = srv.SearchBusinessPro("sweet inter");

            Console.WriteLine("Hello World!");
        }

        private static void LoadDataInSQLServer(string filepath)
        {
            var businesses = File.ReadAllLines(filepath);
            var srv = new ElasticSearchService();
            var cnt = businesses.Length;
            foreach (var line in businesses)
            {
                if (line.Length > 150) continue;
                var k = new Business { Name = line.Trim(), Code = Util.TimeStampCode("BIZ") };
                _context.Businesses.Add(k);
                _context.SaveChanges();
                cnt--;
                Console.WriteLine("Remains: " + cnt);
                srv.IndexBusinessSync(k); 
            }

        }

        //private async static Task IndexDataInElasticClient()
        //{
        //    var srv = new SearchClient();
        //    var businesses = await _context.Businesses.ToListAsync();


        //    foreach (var business in businesses)
        //    {
        //        var res = srv.IndexBusiness(new BusinessModel { Name = business.Name.ToLower(), Id = business.Id });
        //    }
        //}

        
    }
}
