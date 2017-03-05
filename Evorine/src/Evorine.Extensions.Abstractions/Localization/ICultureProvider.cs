using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Localization.Abstractions
{
    public interface ICultureProvider
    {
        CultureInfo CultureInfo { get; }

        string DisplayLanguageCode { get; }
        string FormattingLanguageCode { get; }
    }
}
