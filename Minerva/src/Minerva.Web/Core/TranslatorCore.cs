using Evorine.Localization.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Minerva.Web.Core
{
    public class TranslatorCore : ITranslatorCore
    {
        public string Translate(string module, CultureInfo culture, string text)
        {
            return text;
        }

        public Task<string> TranslateAsync(string module, CultureInfo culture, string text)
        {
            return Task.FromResult(text);
        }
    }
}
