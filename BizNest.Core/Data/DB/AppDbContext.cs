using BizNest.Core.Domain.Entity.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Data.DB
{

    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<BusinessType> BusinessTypes { get; set; }
        public DbSet<StakeHolder> StakeHolders { get; set; }
        public DbSet<Business> Businesses { get; set; }
    }
}
