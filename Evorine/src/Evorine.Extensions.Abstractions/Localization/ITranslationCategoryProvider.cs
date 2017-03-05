using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization.Abstractions
{
    public interface ITranslationCategoryProvider
    {
        string TranslationCategory { get; }
    }
}
