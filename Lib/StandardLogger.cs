using Serilog;
using Serilog.Core;
using System;

namespace Lib
{    
    public enum Ambiente
    {
        NetWebApi,
        WebForms,
        NetWebMvc
    }
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
            : this(SerilogFactory.CreateLogger(new LoggerOptions(connectionString)))
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
        public static StandardLogger Create(Ambiente ambiente)
        {
            var opt = new LoggerOptions("Server=127.0.0.1,1401;Database=ExampleDb;User Id=SA;Password=asdf1234$;TrustServerCertificate=True;");
            switch (ambiente)
            {
                case Ambiente.NetWebApi:
                    return new StandardLogger(SerilogFactory.CreateLegacyWebApiLogger(opt));
                case Ambiente.WebForms:
                    return new StandardLogger(SerilogFactory.CreateWebFormsLogger(opt));
                case Ambiente.NetWebMvc:
                    return new StandardLogger(SerilogFactory.CreateLogger(opt));                
                default:
                    return new StandardLogger(opt.ConnectionString);
            }
        }
    }
}