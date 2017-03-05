using Evorine.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Minerva.Web.Core
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string GetDefaultConnectionString()
        {
            return "Server=(localdb)\\v11.0;Integrated Security=true;";
        }
    }
}
