using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Shared
{
    public abstract class FlurlHttpClientTestsBase
    {
        protected async Task<string> GetRequestUrl(string baseAddress, string requestUri)
        {
            var httpClient = new HttpClient(new DelegatingHandlerStub((message, _) => Task.FromResult(new HttpResponseMessage
                {StatusCode = HttpStatusCode.OK, Content = new StringContent(message.RequestUri!.ToString())})))
            {
                BaseAddress = new Uri(baseAddress)
            };

            return await httpClient.GetStringAsync(requestUri);
        }

        private class DelegatingHandlerStub : DelegatingHandler
        {
            private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _handlerFunc;

            public DelegatingHandlerStub(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> handlerFunc)
            {
                _handlerFunc = handlerFunc;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return await _handlerFunc(request, cancellationToken);
            }
        }
    }
}