using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.Options;

namespace Butterfly.Streaming.InMemory
{
    internal class StreamingSource<T> : IStreamingSource<T>
    {
        private readonly static Func<T, T> closingFunction = x => x;
        private readonly BroadcastBlock<T> _broadcastBlock;
        public ISourceBlock<T> SourceBlock => _broadcastBlock;

        public StreamingSource(IOptions<InMemoryStreamingOptions> options)
        {
            if (options.Value.ProducerCapacity <= 0)
                _broadcastBlock = new BroadcastBlock<T>(closingFunction);
            else
                _broadcastBlock = new BroadcastBlock<T>(closingFunction, new DataflowBlockOptions { BoundedCapacity = options.Value.ProducerCapacity });
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