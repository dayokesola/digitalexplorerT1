using BizNest.Core.Domain.Entity.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Data.DB
{ 

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        { }

        public DbSet<BusinessType> BusinessTypes { get; set; }
    }
}
