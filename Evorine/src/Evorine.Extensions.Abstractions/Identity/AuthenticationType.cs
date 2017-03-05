using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public enum AuthenticationType : byte
    {
        ApplicationAuthentication = 1,
        WindowsAuthentication = 2
    }
}
