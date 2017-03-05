using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Evorine.Identity.Abstractions;
using Evorine.Localization.Abstractions;

namespace Evorine.Localization
{
    public class RequestCultureProvider : IRequestCultureProvider
    {
        public Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            var cultureProvider = (ICultureProvider)httpContext.RequestServices.GetService(typeof(ICultureProvider));
            return Task.FromResult(new ProviderCultureResult(cultureProvider.FormattingLanguageCode, cultureProvider.DisplayLanguageCode));
        }
    }
}
