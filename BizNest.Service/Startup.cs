using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BizNest.Service.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Swashbuckle.AspNetCore.Swagger;
using BizNest.Core.Data.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using BizNest.Core.Common;
using BizNest.Core.Logic.Definations;
using BizNest.Core.Logic.App;
using BizNest.Core.Domain.Entity;
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

            services.AddSwaggerGen(options =>
            {
                AutoMapperConfig.RegisterMappings();
                options.SwaggerDoc("v1", new Info
                {
                    Title = "BizNest API",
                    Version = "v0.1.0",
                    Description = "BizNest REST API",
                    Contact = new Contact
                    {
                        Name = "BizNest API Developers",
                        Email = "dev@biznest.local",
                        Url = "https://localhost:5000"
                    }
                });
                options.AddSecurityDefinition("oauth2", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
                    In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var fileName = Path.GetFileName(assemblyName + ".xml");
                options.IncludeXmlComments(System.IO.Path.Combine(basePath, fileName));

                options.SchemaFilter<SchemaFilter>();
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            #if DEBUG
                        services.AddDbContext<AppDbContext>((obj) => obj.UseSqlServer(Configuration.GetConnectionString("Default"),b=>b.MigrationsAssembly("BizNest.Service")));
            #else
                        services.AddDbContext<AppDbContext>((obj) => Environment.GetEnvironmentVariable("db"));
            #endif



            services.AddScoped<IProhibitedService,ProhibitedNameService>();
            services.AddScoped<ISearchService,MockedSearchService>();


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


            AutoMapperConfig.RegisterMappings();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "BizNest API");
                c.RoutePrefix = string.Empty;


            });
            app.UseMvc();
        }



    }

    public class MockedSearchService : ISearchService
    {
        public async Task InsertBusinessNamesAsync(params string[] names)
        {
            //throw new NotImplementedException();
        }

        public async Task InsertProhibitedNameAsync(params string[] names)
        {
           // throw new NotImplementedException();
        }

        public async Task RemoveBusinessNameAsync(string name)
        {
            //throw new NotImplementedException();
        }

        public async Task RemoveProhibitedNameAsync(string name)
        {
            //throw new NotImplementedException();
        }

        public async Task<SearchResult> SearchAsync(string query)
        {
            return new SearchResult
            {
                SearchTime = TimeSpan.FromSeconds(1),
                Summary = "The search was inconslusive",
                Results = new List<SearchItem>
                 {
                     new SearchItem{ IsProhibited = false, Advice = "xname is prohibitted", MatchPercentage = 10, Word = "cve"  },
                     new SearchItem{ IsProhibited = true, Advice = "xname is prohibitted", MatchPercentage = 40, Word = "cvs32e"  },
                     new SearchItem{ IsProhibited = false, Advice = "xname is prohibitted", MatchPercentage = 10, Word = "word"  }
                 }
            };
           
        }
    }

}
