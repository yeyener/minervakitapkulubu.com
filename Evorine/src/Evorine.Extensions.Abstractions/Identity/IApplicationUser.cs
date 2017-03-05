using Evorine.Identifiers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identity.Abstractions
{
    public interface IApplicationUser : IIntegerIdentifier
    {
        string FullName { get; }

        DateTime LastLoginAt { get; }
    }
}
