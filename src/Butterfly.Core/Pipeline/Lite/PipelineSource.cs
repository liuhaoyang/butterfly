using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;

namespace Butterfly.Pipeline.Lite
{
    internal class PipelineSource<T> : IPipelineSource<T>
    {
        private readonly static Func<T, T> closingFunction = x => x;
        private readonly BroadcastBlock<T> _broadcastBlock;
        public ISourceBlock<T> SourceBlock => _broadcastBlock;

        public PipelineSource(IOptions<LitePipelineOptions> options)
        {
            if (options.Value.ProducerBoundedCapacity <= 0)
                _broadcastBlock = new BroadcastBlock<T>(closingFunction);
            else
                _broadcastBlock = new BroadcastBlock<T>(closingFunction, new DataflowBlockOptions { BoundedCapacity = options.Value.ProducerBoundedCapacity });
        }

        public void Post(T item)
        {
            _broadcastBlock.Post(item);
        }

        public async Task Complete()
        {
            await _broadcastBlock.Completion;
        }
    }
}