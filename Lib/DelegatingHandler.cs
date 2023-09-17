using System.Linq;
using System;
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
        private readonly ILogger _logger;
        public StandardLogHandler(ILogger logger)
        {
            _logger = logger;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var requestContent = await request.Content.ReadAsStringAsync();
            var headers = request.Headers.Select(header => new { Header = header.Key, Value = string.Join(",", header.Value) });
            var version = request.Version.ToString();
            _logger.Info("{version} {httpMethod} {url} -> {headers} \n {requestContent}", version, request.Method.Method, request.RequestUri.ToString(), requestContent, headers);
            try
            {
                var response = await base.SendAsync(request, cancellationToken);
                _logger.Info("{statusCode} {} {response}", response.StatusCode, response.Headers, await response.Content.ReadAsStringAsync());
                return response;
            }
            catch (Exception ex)
            {
                _logger.Error("erro interno durante requisição para {url} {ex}", request.RequestUri.ToString(), ex);
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError);
            }
        }
    }
}
