using Serilog.Core;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Formatting.Compact;

namespace Lib
{    
    public class LoggerOptions
    {
        public LoggerOptions(string connectionString)
        {
            ConnectionString = connectionString;
        }
        public string ConnectionString { get; set; }
    }
    public static class SerilogFactory
    {
        public static Serilog.Core.Logger CreateLogger(LoggerOptions opt)
        {
            Logger logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    new CompactJsonFormatter()
                    ,"log.txt"                    
                    , rollingInterval: RollingInterval.Day
                    , buffered: true
                    )
                .WriteTo.MSSqlServer(
                    connectionString: opt.ConnectionString
                    , sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        AutoCreateSqlDatabase = true,
                        AutoCreateSqlTable = true,
                    }
                )
                .CreateLogger();
            return logger;
        }
        public static Logger CreateLegacyWebApiLogger(LoggerOptions opt)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithWebApiRouteTemplate()
                .Enrich.WithWebApiActionName()                
                .WriteTo.File(
                    new CompactJsonFormatter()
                    , "log.txt"
                    , rollingInterval: RollingInterval.Day
                    , buffered: true
                )
                .WriteTo.MSSqlServer(
                    connectionString: opt.ConnectionString
                    , sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        AutoCreateSqlDatabase = true,
                        AutoCreateSqlTable = true,
                    }
                )
                .CreateLogger();
            return logger;
        }
        public static Logger CreateWebFormsLogger(LoggerOptions opt)
        {
            var logger = new LoggerConfiguration()
                .WriteTo.Console()
                .Enrich.WithHttpRequestClientHostIP()
                .Enrich.WithHttpRequestUrl()
                .Enrich.WithHttpSessionId()
                .Enrich.WithHttpRequestId()
                .Enrich.WithUserName()                
                .WriteTo.File(
                    new CompactJsonFormatter()
                    , "log.txt"
                    , rollingInterval: RollingInterval.Day
                    , buffered: true
                )
                .WriteTo.MSSqlServer(
                    connectionString: opt.ConnectionString
                    , sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
                    {
                        AutoCreateSqlDatabase = true,
                        AutoCreateSqlTable = true,
                    }
                )
                .CreateLogger();
            return logger;
        }
    }
}
