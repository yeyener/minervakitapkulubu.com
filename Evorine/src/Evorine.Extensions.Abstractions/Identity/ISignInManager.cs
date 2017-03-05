using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public interface ISignInManager
    {
        Task SignIn(IApplicationUser user);

        Task SignOut();
    }
}
