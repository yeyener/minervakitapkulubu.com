using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization.Abstractions
{
    public interface ITranslatorProvider
    {
        ITranslator GetTranslator();
        ITranslator GetTranslator(CultureInfo culture, string Module);
    }
}
