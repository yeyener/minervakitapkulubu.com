using Evorine.Localization.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace Minerva.Web.Core
{
    public class CultureProvider : ICultureProvider
    {
        public CultureInfo CultureInfo
        {
            get
            {
                return new CultureInfo("tr-TR");
            }
        }

        public string DisplayLanguageCode
        {
            get
            {
                return "tr";
            }
        }

        public string FormattingLanguageCode
        {
            get
            {
                return "tr";
            }
        }
    }
}
