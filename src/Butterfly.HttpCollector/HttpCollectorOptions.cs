using Microsoft.Extensions.Options;

namespace Butterfly.HttpCollector
{
    public class HttpCollectorOptions : IOptions<HttpCollectorOptions>
    {
        public HttpCollectorOptions Value => this;

        public bool EnableHttpCollector { get; set; }
    }
}
