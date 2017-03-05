using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization.Abstractions
{
    public interface ITranslatorCore
    {
        string Translate(string module, CultureInfo culture, string text);

        Task<string> TranslateAsync(string module, CultureInfo culture, string text);
    }
}
