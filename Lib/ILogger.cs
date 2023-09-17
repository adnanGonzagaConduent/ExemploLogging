using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib
{
    public interface ILogger
    {
        void Verbose(string messageTemplate, params object[] args);
        void Info(string messageTemplate,params object[] args);
        void Warning(string messageTemplate, params object[] args);
        void Debug(string messageTemplate, params object[] args);
        void Error(string messageTemplate, params object[] args);
        void Error(Exception ex);
    }
}
