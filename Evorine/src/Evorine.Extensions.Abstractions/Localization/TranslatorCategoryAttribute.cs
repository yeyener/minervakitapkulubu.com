using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization.Abstractions
{
    public class TranslatorCategoryAttribute : Attribute
    {
        public string TranslatorCategory { get; }

        public TranslatorCategoryAttribute(string translatorCategory)
        {
            TranslatorCategory = translatorCategory;
        }
    }
}
