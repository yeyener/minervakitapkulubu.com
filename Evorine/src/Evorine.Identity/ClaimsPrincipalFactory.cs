using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Evorine.Identity.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Evorine.Identity
{
    public class ClaimsPrincipalFactory : IUserClaimsPrincipalFactory<IApplicationUser>
    {
        internal static ClaimsIdentityOptions _identityOptions = new ClaimsIdentityOptions();

        public Task<ClaimsPrincipal> CreateAsync(IApplicationUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var id = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, _identityOptions.UserNameClaimType, _identityOptions.RoleClaimType);
            id.AddClaim(new Claim(_identityOptions.UserIdClaimType, user.ObjectID.ToString()));
            id.AddClaim(new Claim(_identityOptions.UserNameClaimType, user.FullName));

            return Task.FromResult(new ClaimsPrincipal(id));
        }
    }
}
