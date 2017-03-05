using Evorine.Data.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.EntityFramework
{
    public abstract class EvorineDbContext : DbContext
    {
        readonly IConnectionStringProvider connectionStringProvider;

        public EvorineDbContext(IConnectionStringProvider connectionStringProvider)
        {
            this.connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionStringProvider.GetDefaultConnectionString());
        }
    }
}
