using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog;
using System.Diagnostics;
namespace Exchanges.Utils;

public class LoggerExtentsions : ILogEventEnricher
{
    public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
    {
        var skip = 3;
        while (true)
        {
            var stack = new StackFrame(skip);
            if (!stack.HasMethod())
            {
                logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue("<unknown method>")));
                return;
            }

            var method = stack.GetMethod();
            if (method.DeclaringType.Assembly != typeof(Log).Assembly)
            {
                StackTrace trace = new StackTrace(0, true);
                var stackFrame = trace.GetFrame(5);
                var lineNumber = stackFrame.GetFileLineNumber();
                var methodName = method.IsConstructor ? "Constructor" : method.Name;
                var className = method.ReflectedType.Name;
                var caller = $"{className} - {methodName}() - {lineNumber} -";
                logEvent.AddPropertyIfAbsent(new LogEventProperty("Caller", new ScalarValue(caller)));
                return;
            }

            skip++;
        }
    }
}

static class LoggerCallerEnrichmentConfiguration
{
    public static LoggerConfiguration WithCaller(this LoggerEnrichmentConfiguration enrichmentConfiguration)
    {
        return enrichmentConfiguration.With<LoggerExtentsions>();
    }
}
