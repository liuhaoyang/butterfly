using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Butterfly.Streaming.InMemory
{
    public interface IStreamingSource<T>
    { 
        ISourceBlock<T> SourceBlock { get; }
        void Post(T item);

        Task Complete();
    }
}