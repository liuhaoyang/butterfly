using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Butterfly.Streaming.InMemory
{
    public class BlockingQueue<T> : IBlockingQueue<T>
    {
        private readonly BlockingCollection<IEnumerable<T>> _blocking;

        public BlockingQueue()
        {
            _blocking = new BlockingCollection<IEnumerable<T>>();
        }

        public bool IsActived => !_blocking.IsAddingCompleted && !_blocking.IsCompleted;

        public void Enqueue(IEnumerable<T> spans)
        {
            _blocking.TryAdd(spans);
        }

        public IEnumerable<T> Dequeue()
        {
            foreach (var consumingEnumerable in _blocking.GetConsumingEnumerable())
            {
                foreach (var consumingItem in consumingEnumerable)
                {
                    yield return consumingItem;
                }
            }
        }

        public IEnumerable<IEnumerable<T>> DequeueEnumerable(CancellationToken cancellationToken)
        {
            foreach (var consumingEnumerable in _blocking.GetConsumingEnumerable(cancellationToken))
            {
                yield return consumingEnumerable;
            }
        }

        public void Complete()
        {
            if (!_blocking.IsAddingCompleted)
                _blocking.CompleteAdding();
        }
    }
}