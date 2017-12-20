using Microsoft.Extensions.Options;

namespace Butterfly.Flow.InMemory
{
    public class InMemoryFlowOptions : IOptions<InMemoryFlowOptions>
    {
        public InMemoryFlowOptions Value => this;

        public int MaxConsumer { get; set; }
    }
}