using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public interface IIdentityProvider
    {
        bool IsSignedIn { get; }
        
        IApplicationUser User { get; }
        AuthenticationType AuthenticationType { get; }

        bool HasPermission(params IPermission[] permissions);
        bool HasRole(params IRole[] roles);
    }
}
