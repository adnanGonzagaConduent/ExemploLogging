using Serilog.Core;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Formatting.Compact;

namespace Lib
{    
    public static class SerilogFactory
    {
        public static Serilog.Core.Logger CreateLogger(string connectionString)
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
                    connectionString: connectionString
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
