using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

namespace Evorine.Engine
{
    public class EngineConfigurator
    {
        readonly IServiceProvider serviceProvider;
        readonly IApplicationBuilder appBuilder;
        
        public EngineConfigurator(IApplicationBuilder appBuilder, IServiceProvider serviceProvider)
        {
            this.appBuilder = appBuilder;
            this.serviceProvider = serviceProvider;
        }

        public void UseMemoryCache()
        {
            var cacheProvider = (Data.Cache.CacheProvider)serviceProvider.GetService<Data.Abstractions.ICacheProvider>();
            cacheProvider.UseMemoryCache();
        }

        #region Logger
        public void RegisterLoggers()
        {
            var loggerProvider = (Logger.LoggerProvider)serviceProvider.GetService<Logger.Abstractions.ILoggerProvider>();
            var loggerFactory = serviceProvider.GetService<Microsoft.Extensions.Logging.ILoggerFactory>();
            loggerFactory.AddProvider(loggerProvider);
        }
        public Logger.Abstractions.ILogger ConfigureDefaultLogger(Action<Logger.Abstractions.LogSettings> logSettingsSetup)
        {
            var settings = new Logger.Abstractions.LogSettings();
            logSettingsSetup(settings);
            var loggerProvider = (Logger.LoggerProvider)serviceProvider.GetService<Logger.Abstractions.ILoggerProvider>();
            return loggerProvider.CreateDefaultLogger(settings);
        }
        public Logger.Abstractions.ILogger ConfigureDatabaseLogger(Action<Logger.Abstractions.LogSettings> logSettingsSetup)
        {
            var settings = new Logger.Abstractions.LogSettings();
            logSettingsSetup(settings);
            var loggerProvider = (Logger.LoggerProvider)serviceProvider.GetService<Logger.Abstractions.ILoggerProvider>();
            return loggerProvider.CreateDatabaseLogger(settings);
        }

        public Logger.Abstractions.ILogger ConfigureScriptingLogger(Action<Logger.Abstractions.LogSettings> logSettingsSetup)
        {
            var settings = new Logger.Abstractions.LogSettings();
            logSettingsSetup(settings);
            var loggerProvider = (Logger.LoggerProvider)serviceProvider.GetService<Logger.Abstractions.ILoggerProvider>();
            return loggerProvider.CreateScriptingLogger(settings);
        }
        #endregion

        public void UseCookieAuthentication()
        {
            appBuilder.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AutomaticAuthenticate = true
            });
        }


        #region Localization
        public void RegisterTranslatorCore(Localization.Abstractions.ITranslatorCore translatorCore)
        {
            Localization.TranslatorProvider.SetTranslatorCore(translatorCore);
        }

        public void ConfigureLocalization(CultureInfo[] supportedLanguages)
        {
            var options = new RequestLocalizationOptions();

            options.SupportedCultures.Clear();
            foreach (var language in supportedLanguages)
            {
                options.SupportedCultures.Add(language);
                options.SupportedUICultures.Add(language);
            }
            options.RequestCultureProviders.Clear();
            options.RequestCultureProviders.Add(new Localization.RequestCultureProvider());

            appBuilder.UseRequestLocalization(options);
        }
#endregion
    }
}
