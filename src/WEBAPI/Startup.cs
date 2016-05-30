using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
//using WEBAPI.Repository;
using WEBAPI.Middleware.CustomHeader;
using Microsoft.AspNetCore.Hosting;
using WEBAPI.Data;
using WEBAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using OpenIddict;
using OpenIddict.Models;
using WEBAPI.Infrastructure;

namespace WEBAPI
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            if (env.IsEnvironment("Development"))
            {
                // For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
                builder.AddUserSecrets();
                
                // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
                builder.AddApplicationInsightsSettings(developerMode: true);
            }

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            // Add framework services.
            services.AddApplicationInsightsTelemetry(Configuration);

            //services.AddEntityFramework()
            //    .AddSqlServer()
            //    .AddDbContext<ApplicationDbContext>(options =>
            //        options.UseSqlServer(Configuration["DefaultConnection"]));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o =>
            {
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false; ;
                o.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders()
            .AddOpenIddictCore<Application>(config => config.UseEntityFramework()); ;

            services.AddMvc();

            services.AddScoped<OpenIddictManager<ApplicationUser, Application>, CustomOpenIddictManager>();
            //services.AddTransient<IRepository<Category>, Repository<Category>>();

            //services.AddScoped<IArticleRepository, ArticleRepository>();
            //services.AddScoped<IRepository<Article>, IRepository<Article>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseCors(builder =>
            builder.WithOrigins("*")
                   .WithMethods("*")
                   //.WithMethods("GET", "POST")
                   .AllowAnyHeader()
            );

            //app.UseMiddleware<AuthorizationMiddleware>();

            //app.UseIISPlatformHandler();
            
            
            app.UseAuthorization();

            app.UseApplicationInsightsRequestTelemetry();

            app.UseApplicationInsightsExceptionTelemetry();


            app.UseOpenIddictCore(builder =>
            {
                // tell openiddict you're wanting to use jwt tokens
                builder.Options.UseJwtTokens();
                // NOTE: for dev consumption only! for live, this is not encouraged!
                builder.Options.AllowInsecureHttp = true;
                builder.Options.ApplicationCanDisplayErrors = true;
            });

            // use jwt bearer authentication
            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                RequireHttpsMetadata = false,
                Audience = "http://localhost:58292/",
                Authority = "http://localhost:58292/"
            });

            app.UseStaticFiles();

            app.UseMvc();

            // Custom Middleware
            app.UseCustomHeader();
            app.UsePing();
        }

        // Entry point for the application.
        //public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
