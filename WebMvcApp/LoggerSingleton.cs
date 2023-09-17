using Lib;
using System;
namespace WebMvcApp
{
    public static class LoggerSingleton
    {        
        private static readonly ILogger Instance = StandardLogger.Create(Ambiente.NetWebMvc);
        public static void Verbose(string messageTemplate, params object[] args)
            => Instance?.Verbose(messageTemplate, args);

        public static void Debug(string messageTemplate, params object[] args)
            => Instance?.Debug(messageTemplate, args);

        public static void Error(string messageTemplate, params object[] args)
            => Instance?.Error(messageTemplate, args);

        public static void Error(Exception ex)
            => Instance?.Error(ex);

        public static void Warning(string messageTemplate, params object[] args)
            => Instance?.Warning(messageTemplate, args);

        public static void Info(string messageTemplate, params object[] args)
            => Instance?.Info(messageTemplate, args);        
    }
}