using BizNest.Core.Common;
using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Domain.Model.App;
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
            _optionsBuilder.UseSqlServer("Server=tcp:127.0.0.1,1433;Initial Catalog=BizNestDataDb;Persist Security Info=False;" +
                "User ID=SA;Password=Dev@12345;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;");
            _context = new AppDbContext(_optionsBuilder.Options);
          

            AutoMapperConfig.RegisterMappings();

            var filepath = @"C:\Users\Vincent Nwonah\tmp\dataset.txt";

            //LoadDataInSQLServer(filepath);
            //IndexDataInElasticClient().Wait();

            var srv = new SearchClient();
            //var j1 = srv.IndexBusiness(biz1);
            //var j2 = srv.IndexBusiness(biz2);


            var data = srv.SearchBusinessPro("sweet inter");

            Console.WriteLine("Hello World!");
        }

        private static void LoadDataInSQLServer(string filepath)
        {
            var businesses = File.ReadAllLines(filepath);

            foreach (var line in businesses)
            {
                _context.Businesses.Add(new Business { Name = line.Trim() });
                _context.SaveChanges();
            }

        }

        private async static Task IndexDataInElasticClient()
        {
            var srv = new SearchClient();
            var businesses = await _context.Businesses.ToListAsync();


            foreach (var business in businesses)
            {
                var res = srv.IndexBusiness(new BusinessModel { Name = business.Name.ToLower(), Id = business.Id });
            }
        }

        
    }
}
