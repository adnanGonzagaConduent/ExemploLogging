using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lib
{
    public static class LoggerExtensions
    {
        //public static async Task Log(this HttpRequestMessage httpRequest,ILogger logger)
        //{
        //    var requestContent = await httpRequest.Content.ReadAsStringAsync();
        //    var headers = httpRequest.Headers.Select(header => new { Header = header.Key, Value = string.Join(",", header.Value) });
        //    var version = httpRequest.Version.ToString();
        //    logger.Info("{version} {httpMethod} {url} -> {headers} \n {requestContent}", version, httpRequest.Method.Method, httpRequest.RequestUri.ToString(), headers, requestContent);
        //}
    }
}
