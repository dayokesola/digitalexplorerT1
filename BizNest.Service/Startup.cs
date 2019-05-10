using BizNest.Core.Common;
using BizNest.Core.Data.DB;
using BizNest.Core.Domain.Entity;
using BizNest.Core.Domain.Entity.App;
using BizNest.Core.Logic.App;
using BizNest.Core.Logic.Definations;
using BizNest.Service.Helpers;
using BizNest.Service.Interfaces;
using BizNest.Service.Models.AuthModels;
using BizNest.Service.Services;
using BizNest.Service.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BizNest.Service
{
    public class Startup
    {

        /*=================================================
         *          Essential Commands
         * ================================================
         * Before running any of these commands set ASPNETCORE_ENVIRONMENT variable first.
         * This lets entity framework know which configuration files and connection string to use for the database operations
         * 
         * How to set:
         * Mac OS: export ASPNETCORE_ENVIRONMENT=<Environment Name> eg: export ASPNETCORE_ENVIRONMENT=Production
         * Windows CMD: set ASPNETCORE_ENVIRONMENT=<Environment Name> eg: set ASPNETCORE_ENVIRONMENT=Production
         * Windows CMD 2: setx ASPNETCORE_ENVIRONMENT=<Environment Name> eg: setx ASPNETCORE_ENVIRONMENT=Production.
         * Use second command if first fails. 
         * 
         * Info: change directory into API Project first with cd BizNest.Service before running Migrations and DB
         * Update Commands
         * 
         * Migrations are Added Per DB Context
         * 
         * Sample Migration Command: 
         * > dotnet ef migrations add "Initial Migration" --context AuthenticationDbContext
         * -------------------------------------------------
         * Database is Updated Per DB Context
         * 
         * Sample Update Command:
         * > dotnet ef database update --context AuthenticationDbContext
         * 
         * You can add the verbose flag to commands to see whats going on with --verbose
         * eg: > dotnet ef database update --context AuthenticationDbContext --verbose
         */


        private const string SecretKey = "iNivDmHLpUA223sqsfhqGbMRdRj1PVkH"; // todo: get this from somewhere secure
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                configureOptions.TokenValidationParameters = tokenValidationParameters;
                configureOptions.SaveToken = true;
            });

            // api user claim policy
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess));
                options.AddPolicy("SysUser", policy => policy.RequireClaim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.SysAccess));
            });

            services.AddOptions();

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

            services.AddSwaggerGen(options =>
            {
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

            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationDb"), b => b.MigrationsAssembly("BizNest.Service")));

#if DEBUG
            services.AddDbContext<AppDbContext>((obj) => obj.UseSqlServer(Configuration.GetConnectionString("Default"),b=>b.MigrationsAssembly("BizNest.Service")));
#else
            services.AddDbContext<AppDbContext>((obj) => Environment.GetEnvironmentVariable("db"));
#endif


            services.AddScoped<IProhibitedService,ProhibitedNameService>();
            services.AddScoped<ISearchService,ElasticSearchService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // add identity
            var builder = services.AddIdentityCore<ApplicationUser>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<AuthenticationDbContext>().AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigin",
                builder1 => builder1
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
            if (env.IsDevelopment() || env.Equals("Local"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                //context.Response.AddApplicationError(error.Error.Message);
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                }
               );

            app.UseHttpsRedirection();
            //app.UseCors(x => x
            //   .AllowAnyOrigin()
            //   .AllowAnyMethod()
            //   .AllowAnyHeader());
            app.UseAuthentication();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Biznest API");
                c.RoutePrefix = string.Empty;


            });
           
            app.UseMvc();

           
        }



    }

    public class MockedSearchService : ISearchService
    {
        public async Task InsertBusinessNamesAsync(params Business[] businesses)
        {
            //throw new NotImplementedException();
        }

        public async Task InsertProhibitedNameAsync(params ProhibitedName[] names)
        {
           // throw new NotImplementedException();
        }

       

        public async Task RemoveBusinessNameAsync(Business name)
        {
            //throw new NotImplementedException();
        }

        public async Task RemoveProhibitedNameAsync(ProhibitedName name)
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
