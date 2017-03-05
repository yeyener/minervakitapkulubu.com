using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Logger.Abstractions
{
    public class LogSettings
    {
        public LogSettings()
        {
            TraceLogEnabled = false;
            DebugLogEnabled = false;
            InfoLogEnabled = false;
            WarningLogEnabled = true;
            ErrorLogEnabled = true;
            FatalLogEnabled = true;
        }

        public string LoggerDirectory { get; set; }

        public bool TraceLogEnabled { get; set; }
        public bool DebugLogEnabled { get; set; }
        public bool InfoLogEnabled { get; set; }
        public bool WarningLogEnabled { get; set; }
        public bool ErrorLogEnabled { get; set; }
        public bool FatalLogEnabled { get; set; }
    }
}
