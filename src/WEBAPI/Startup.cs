using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WEBAPI.Services;
using App.Data.Repository.Blog;
using App.Data.Context;
using App.Data.Models;
using Swashbuckle.SwaggerGen.Generator;
using WEBAPI.Filters;
using Newtonsoft.Json.Serialization;

namespace WEBAPI
{
    public partial class Startup
    {
        private readonly string pathToDoc = @"C:\Users\TinLVV\Documents\dev\ASPNETCORE\src\WEBAPI\bin\Debug\netcoreapp1.0\WEBAPI.xml";
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsDevelopment())
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();

                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Cors service.
            services.AddCors(options => options
                .AddPolicy("AllowAll", p => p
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                )
            );

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //services.AddDbContext<AppDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            //services.AddDbContext<ApplicationContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DbConnection")));

            //services.AddDbContext<AppDbContext>();
            services.AddDbContext<ApplicationContext>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.SignIn.RequireConfirmedEmail = false;
                o.SignIn.RequireConfirmedPhoneNumber = false;
                o.Lockout.AllowedForNewUsers = true;
            })
            .AddEntityFrameworkStores<App.Data.Context.ApplicationContext>()
            .AddDefaultTokenProviders();

            //services.AddMvc();
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "ASP.NET CORE",
                    Description = "An API API With Swagger for RC2",
                    TermsOfService = "None",
                });
                options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
                options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.AddSingleton<IArticleRepository, ArticleRepository>();
            // Add application services.
            services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddSingleton<IArticleRepository, ArticleRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            //app.UseExceptionHandler(appBuilder =>
            //{
            //    appBuilder.Use(async (context, next) =>
            //    {
            //        var error = context.Features[typeof(IExceptionHandlerFeature)] as IExceptionHandlerFeature;
            //        // This should be much more intelligent - at the moment only expired 
            //        // security tokens are caught - might be worth checking other possible 
            //        // exceptions such as an invalid signature.
            //        if (error != null && error.Error is SecurityTokenExpiredException)
            //        {
            //            context.Response.StatusCode = 401;
            //            // What you choose to return here is up to you, in this case a simple 
            //            // bit of JSON to say you're no longer authenticated.
            //            context.Response.ContentType = "application/json";
            //            await context.Response.WriteAsync(
            //                JsonConvert.SerializeObject(
            //                    new { authenticated = false, tokenExpired = true }));
            //        }
            //        else if (error != null && error.Error != null)
            //        {
            //            context.Response.StatusCode = 500;
            //            context.Response.ContentType = "application/json";
            //            // TODO: Shouldn't pass the exception message straight out, change this.
            //            await context.Response.WriteAsync(
            //                JsonConvert.SerializeObject
            //                (new { success = false, error = error.Error.Message }));
            //        }
            //        // We're not trying to handle anything else so just let the default 
            //        // handler handle.
            //        else await next();
            //    });
            //});

            //app.UseAuthorization();

            app.UseApplicationInsightsRequestTelemetry();
            // Before we setup the pipeline, get the database up
            //AppDatabase.InitializeDatabase(app.ApplicationServices,
            //    isProduction: env.IsProduction());
            AppDatabase.EnsureIdentityDatabaseExists(app.ApplicationServices,
                isProduction: env.IsProduction());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseIdentity();

            ConfigureAuth(app);

            app.UseApplicationInsightsExceptionTelemetry();

            //app.UseAuthorization();
            //app.UseIdentity();

            // Add external authentication middleware below. To configure them please see http://go.microsoft.com/fwlink/?LinkID=532715

            app.UseMvc();

            app.UseSwaggerGen();
            app.UseSwaggerUi();
        }
    }
}
