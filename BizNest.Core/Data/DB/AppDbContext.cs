using BizNest.Core.Domain.Entity.App;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizNest.Core.Data.DB
{ 

    public class BloggingContext : DbContext
    {
        public BloggingContext(DbContextOptions<BloggingContext> options)
            : base(options)
        { }

        public DbSet<BusinessType> BusinessTypes { get; set; }
    }
}
