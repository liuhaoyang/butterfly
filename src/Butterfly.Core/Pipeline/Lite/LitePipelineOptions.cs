using Microsoft.Extensions.Options;

namespace Butterfly.Pipeline.Lite
{
    public class LitePipelineOptions : IOptions<LitePipelineOptions>
    {
        public LitePipelineOptions Value => this;

        public int MaxConsumerParallelism { get; set; }

        public int ProducerBoundedCapacity { get; set; }

        public int ConsumerBoundedCapacity { get; set; }
    }
}