using Microsoft.Extensions.Options;

namespace Butterfly.Streaming.InMemory
{
    public class InMemoryStreamingOptions : IOptions<InMemoryStreamingOptions>
    {
        public InMemoryStreamingOptions Value => this;

        public int MaxConsumerParallelism { get; set; }

        public int ProducerCapacity { get; set; }

        public int ConsumerCapacity { get; set; }
    }
}