using Microsoft.Extensions.Options;

namespace Butterfly.Consumer.Lite
{
    public class ConsumerOptions : IOptions<ConsumerOptions>
    {
        public ConsumerOptions Value => this;

        public int MaxConsumerParallelism { get; set; }

        public int ProducerBoundedCapacity { get; set; }

        public int ConsumerBoundedCapacity { get; set; }
    }
}