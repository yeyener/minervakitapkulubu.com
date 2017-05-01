using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Globalization;
using Evorine.Engine;
using Microsoft.Extensions.Configuration;

namespace Minerva.Web
{
    public class Startup
    {
        EngineRegistrar registrar;

        public IConfiguration Configuration { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            var builder = new ConfigurationBuilder()
                          .SetBasePath(hostingEnvironment.ContentRootPath)
                          .AddJsonFile("settings.json")
                          .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            registrar = services.AddEvorineEngine();
            registrar.AddCaching()
                     .AddConnectionProvider<Core.ConnectionStringProvider>()
                     //.AddIdentity<Services.Core.IdentityProvider, Services.Core.UserProviderService, Evorine.Identity.CookieSignInManager>()
                     .AddSession(TimeSpan.FromMinutes(5))
                     .AddLogging()
                     .AddLocalization<Core.CultureProvider, Core.TranslationCategoryProvider>();


            var awsOptions = Configuration.GetAWSOptions();
            services.AddDefaultAWSOptions(awsOptions);
            services.AddAWSService<Amazon.SimpleEmail.IAmazonSimpleEmailService>();


            registrar.AddMvc((config) => { });

            return registrar.BuildProvider();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider, IHostingEnvironment env)
        {
            var evorineConfigurator = registrar.BuildConfigurator(app, serviceProvider);

            evorineConfigurator.UseMemoryCache();

            evorineConfigurator.ConfigureDefaultLogger(settings =>
            {
                settings.ErrorLogEnabled = true;
                settings.FatalLogEnabled = true;
                settings.LoggerDirectory = @"C:\Playground\logs";
            });
            evorineConfigurator.RegisterLoggers();

            evorineConfigurator.UseCookieAuthentication();

            evorineConfigurator.ConfigureLocalization(new CultureInfo[] { new CultureInfo("tr-TR") });
            evorineConfigurator.RegisterTranslatorCore(new Core.TranslatorCore());

            app.UseStaticFiles();
            app.UseDeveloperExceptionPage();

            app.UseMvc((routes) =>
            {
                routes.MapRoute(name: "areaRoute", template: "{area:exists}/{controller=Home}/{action=Index}");

                routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
