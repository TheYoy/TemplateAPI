using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
namespace APITemplate.DependencyInjecyions
{
    public class IServiceHeadersHandler: DelegatingHandler
    {
        public IServiceHeadersHandler() :  base(new HttpClientHandler() ) {}
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken);
        }
    }
}