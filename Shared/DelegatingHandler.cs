using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Lib
{
    /// <summary>
    /// Loga as requisições e respostas de uma aplicação MVC.
    /// </summary>
    public class StandardLogHandler : DelegatingHandler
    {        
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}
