using Serilog;
using Serilog.Core;
using System;

namespace Lib
{
    /// <summary>
    /// Implementação padrão para realização de logging
    /// </summary>
    public class StandardLogger : ILogger
    {
        private readonly Logger _provider;
        
        public StandardLogger(Logger logger)
        {
            _provider = logger;
        }
        public StandardLogger(string connectionString)
            : this(SerilogFactory.CreateLogger(connectionString))
        {
                
        }
        public void Verbose(string messageTemplate, params object[] args)
            => _provider?.Verbose(messageTemplate, args);
        
        public void Debug(string messageTemplate, params object[] args)
            => _provider?.Debug(messageTemplate, args);

        public void Error(string messageTemplate, params object[] args)
            => _provider?.Error(messageTemplate, args);

        public void Error(Exception ex)
            => _provider?.Error(ex, "a exception was throwed: {ex}");

        public void Warning(string messageTemplate, params object[] args)
            => _provider?.Warning(messageTemplate, args);

        public void Info(string messageTemplate, params object[] args)
            => _provider?.Information(messageTemplate, args);        
    }
}