using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BizNest.Core.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace BizNest.Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #if DEBUG
                        services.AddDbContext<AppDbContext>((obj) => obj.UseSqlServer(Configuration.GetConnectionString("Default"),b=>b.MigrationsAssembly("BizNest.Service")));
            #else
                        services.AddDbContext<AppDbContext>((obj) => Environment.GetEnvironmentVariable("db"));
            #endif
           
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder => builder
                .WithOrigins("*")
                .WithHeaders("*")
                .WithMethods("*")
                .WithExposedHeaders("X-Pagination")
                .SetPreflightMaxAge(TimeSpan.FromSeconds(2520))
                );
            });

            // Apply as default to all controllers. API etc
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("AllowMyOrigin"));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
