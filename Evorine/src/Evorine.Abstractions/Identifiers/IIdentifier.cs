using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Identifiers
{
    public interface IIdentifier
    {
    }

    public interface IIdentifier<TID> : IIdentifier
    {
        TID ObjectID { get; }
    }
}
