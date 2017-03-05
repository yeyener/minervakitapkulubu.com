using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.Abstractions
{
    public interface IConnectorProvider
    {
        IConnector GetConnector();
    }
}
