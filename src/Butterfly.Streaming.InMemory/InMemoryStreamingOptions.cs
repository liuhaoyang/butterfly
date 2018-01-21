using Microsoft.Extensions.Options;

namespace Butterfly.Streaming.InMemory
{
    public class InMemoryStreamingOptions : IOptions<InMemoryStreamingOptions>
    {
        public InMemoryStreamingOptions Value => this;

        public int MaxConsumer { get; set; }
    }
}