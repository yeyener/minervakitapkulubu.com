using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Evorine.Localization.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Evorine.Localization
{
    public class TranslatorProvider : ITranslatorProvider
    {
        readonly IDictionary<CultureInfo, IDictionary<string, ITranslator>> translators;
        readonly IServiceProvider serviceProvider;
        private static ITranslatorCore translatorCore;

        public TranslatorProvider(IServiceProvider serviceProvider)
        {
            translators = new Dictionary<CultureInfo, IDictionary<string, ITranslator>>();
            this.serviceProvider = serviceProvider;
        }

        public static void SetTranslatorCore(ITranslatorCore translatorCore)
        {
            TranslatorProvider.translatorCore = translatorCore;
        }

        private ITranslator createTranslator(CultureInfo culture, string module)
        {
            return new Translator(translatorCore, culture, module);
        }

        public ITranslator GetTranslator()
        {
            return GetTranslator(serviceProvider.GetService<ICultureProvider>().CultureInfo, serviceProvider.GetService<ITranslationCategoryProvider>().TranslationCategory);
        }

        public ITranslator GetTranslator(CultureInfo culture, string module)
        {
            if (!translators.ContainsKey(culture))
            {
                lock (this)
                {
                    if (!translators.ContainsKey(culture))
                    {
                        translators[culture] = new Dictionary<string, ITranslator>();
                    }
                }
            }
            var modules = translators[culture];

            if (!modules.ContainsKey(module))
            {
                lock (this)
                {
                    if (!modules.ContainsKey(module))
                    {
                        modules[module] = createTranslator(culture, module);
                    }
                }
            }

            return modules[module];
        }
    }
}
