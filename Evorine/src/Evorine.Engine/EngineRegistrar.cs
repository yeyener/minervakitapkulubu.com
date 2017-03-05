using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;

namespace Evorine.Engine
{
    /// <summary>
    /// Evorine Framework service registrar
    /// </summary>
    public class EngineRegistrar
    {
        readonly IServiceCollection services;
        readonly ContainerBuilder containerBuilder;
        private IContainer container;

        bool localizationAdded = false;

        internal EngineRegistrar(IServiceCollection services)
        {
            this.services = services;
            containerBuilder = new ContainerBuilder();
            RegisterDefaults();
        }

        private void RegisterDefaults()
        {
            services.TryAddSingleton<Microsoft.AspNetCore.Http.IHttpContextAccessor, Microsoft.AspNetCore.Http.HttpContextAccessor>();
            
        }

        public EngineRegistrar AddCaching()
        {
            services.AddDistributedMemoryCache();
            services.AddSingleton<Data.Abstractions.ICacheProvider, Data.Cache.CacheProvider>();
            return this;
        }

        public EngineRegistrar AddSession(TimeSpan timeOut)
        {
            services.AddSession(o =>
            {
                o.IdleTimeout = timeOut;
            }); return this;
        }

        public EngineRegistrar AddLogging()
        {
            services.AddLogging();
            services.AddSingleton<Logger.Abstractions.ILoggerProvider, Logger.LoggerProvider>();
            return this;
        }

        public EngineRegistrar AddLocalization<TCultureProvider, TTranslationCategoryProvider>()
            where TCultureProvider : Localization.Abstractions.ICultureProvider
            where TTranslationCategoryProvider : Localization.Abstractions.ITranslationCategoryProvider
        {
            localizationAdded = true;

            services.AddLocalization();
            services.AddSingleton<Localization.Abstractions.ITranslatorProvider, Localization.TranslatorProvider>();
            services.AddScoped(typeof(Localization.Abstractions.ICultureProvider), typeof(TCultureProvider));
            services.AddScoped(typeof(Localization.Abstractions.ITranslationCategoryProvider), typeof(TTranslationCategoryProvider));
            
            return this;
        }

        public EngineRegistrar AddIdentity<TIdentityInformationProvider, TUserProvider, TSignInManager>()
            where TIdentityInformationProvider : Identity.Abstractions.IIdentityProvider
            where TUserProvider : Identity.Abstractions.IUserProvider
            where TSignInManager : Identity.Abstractions.ISignInManager
        {
            services.AddScoped(typeof(Identity.Abstractions.IIdentityProvider), typeof(TIdentityInformationProvider));
            services.AddSingleton(typeof(Identity.Abstractions.IUserProvider), typeof(TUserProvider));
            services.AddScoped(typeof(Identity.Abstractions.ISignInManager), typeof(TSignInManager));
            services.AddSingleton(typeof(IUserClaimsPrincipalFactory<Identity.Abstractions.IApplicationUser>), typeof(Identity.ClaimsPrincipalFactory));
            
            return this;
        }


        public IMvcBuilder AddMvc(Action<Microsoft.AspNetCore.Mvc.MvcOptions> setupAction)
        {
            return services.AddMvc(options =>
            {
                setupAction(options);
            });
        }
        

        public EngineRegistrar AddConnectionProvider<TConnectionStringProvider>()
            where TConnectionStringProvider : Data.Abstractions.IConnectionStringProvider
        {
            services.AddScoped(typeof(Data.Abstractions.IConnectionStringProvider), typeof(TConnectionStringProvider));
            return this;
        }


        public IServiceProvider BuildProvider()
        {
            containerBuilder.Populate(services);
            container = containerBuilder.Build();
            return container.Resolve<IServiceProvider>();
        }
        
        public EngineConfigurator BuildConfigurator(IApplicationBuilder appBuilder, IServiceProvider serviceProvider)
        {
            return new EngineConfigurator(appBuilder, serviceProvider);
        }
    }
}
