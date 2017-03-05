using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Engine
{
    public static class EngineRegistrarExtensions
    {
        /// <summary>
        /// Creates a registrar instance for configuring Evorine Framework.
        /// </summary>
        public static EngineRegistrar AddEvorineEngine(this IServiceCollection services)
        {
            return new EngineRegistrar(services);
        }
    }
}
