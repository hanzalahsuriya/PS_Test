using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Paymentsense.Coding.Challenge.Api.Tests.Fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly Dictionary<(HttpMethod, string), HttpResponseMessage> _responses = new Dictionary<(HttpMethod, string), HttpResponseMessage>();

        public void AddResponse(HttpMethod method, string url, HttpResponseMessage response)
        {
            _responses.Add((method, url.ToLower()), response);
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (_responses.TryGetValue((request.Method, request.RequestUri.ToString().ToLower()), out var responseMessage))
            {
                return Task.FromResult(responseMessage);
            }

            throw new NotImplementedException();
        }
    }
}
