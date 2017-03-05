using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Evorine.Logger.Abstractions
{
    public interface ILogger
    {
        string Name { get; }

        void Trace(string message, Exception exception = null, [CallerFilePath] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
        
        void Debug(string message, Exception exception = null, [CallerFilePathAttribute] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
        
        void Info(string message, Exception exception = null, [CallerFilePathAttribute] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
        
        void Warning(string message, Exception exception = null, [CallerFilePathAttribute] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
        
        void Error(string message, Exception exception = null, [CallerFilePathAttribute] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
        
        void Fatal(string message, Exception exception = null, [CallerFilePathAttribute] string callerPath = "", [CallerMemberName] string callerMember = "", [CallerLineNumber] int callerLine = 0);
    }
}
