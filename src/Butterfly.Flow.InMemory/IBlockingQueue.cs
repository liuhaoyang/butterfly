using System.Collections.Generic;

namespace Butterfly.Flow.InMemory
{
    public interface IBlockingQueue<T>
    {
        bool IsActived { get; }

        void Enqueue(IEnumerable<T> spans);

        IEnumerable<T> Dequeue();

        IEnumerable<IEnumerable<T>> DequeueEnumerable();

        void Complete();
    }
}