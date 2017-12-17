using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Butterfly.Flow.InMemory
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

        public IEnumerable<IEnumerable<T>> DequeueEnumerable()
        {
            foreach (var consumingEnumerable in _blocking.GetConsumingEnumerable())
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