using Evorine.Identity.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity
{
    public abstract class IdentityProviderBase : IIdentityProvider
    {
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly IUserProvider userProvider;

        public IdentityProviderBase(IHttpContextAccessor httpContextAccessor, IUserProvider userProvider)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userProvider = userProvider;
        }

        public virtual AuthenticationType AuthenticationType
        {
            get
            {
                return AuthenticationType.ApplicationAuthentication;
            }
        }

        public virtual IApplicationUser User
        {
            get
            {
                if (!IsSignedIn)
                    throw new NotSignedInException("Unable to access to user without sign-in!");
                return userProvider.GetUser(int.Parse(httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == ClaimsPrincipalFactory._identityOptions.UserIdClaimType).Value));
            }
        }

        public abstract bool HasPermission(params IPermission[] permissions);

        public abstract bool HasRole(params IRole[] roles);

        public virtual bool IsSignedIn
        {
            get
            {
                try
                {
                    return httpContextAccessor.HttpContext.User.Identity.IsAuthenticated;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
