using Evorine.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Evorine.Data.Abstractions;

namespace Minerva.Web.Models
{
    public class MinervaContext : EvorineDbContext
    {
        public MinervaContext(IConnectionStringProvider connectionStringProvider) : base(connectionStringProvider)
        {

        }
    }
}
