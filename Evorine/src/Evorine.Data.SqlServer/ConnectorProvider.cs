using Evorine.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.SqlServer
{
    public class ConnectorProvider : IConnectorProvider
    {
        readonly IConnectionStringProvider connectionStringProvider;

        public ConnectorProvider(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        public IConnector GetConnector()
        {
            return new Connector(connectionStringProvider);
        }
    }
}
