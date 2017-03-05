using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Logger.Abstractions
{
    public interface ILoggerProvider
    {
        ILogger GetDatabaseLogger();
        ILogger GetScriptingLogger();
        ILogger GetDefaultLogger();

        IEnumerable<ILogger> Loggers { get; }
    }
}
