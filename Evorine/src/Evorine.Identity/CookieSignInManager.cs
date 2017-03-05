using Evorine.Identity.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity
{
    public class CookieSignInManager : ISignInManager
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly IUserClaimsPrincipalFactory<IApplicationUser> claimsFactory;

        public CookieSignInManager(IHttpContextAccessor httpContextAccessor, IUserClaimsPrincipalFactory<IApplicationUser> claimsFactory)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.claimsFactory = claimsFactory;
        }
        
        public virtual async Task SignIn(IApplicationUser user)
        {
            var claims = await claimsFactory.CreateAsync(user);
            await httpContextAccessor.HttpContext.Authentication.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claims);
        }

        public virtual async Task SignOut()
        {
            await httpContextAccessor.HttpContext.Authentication.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
