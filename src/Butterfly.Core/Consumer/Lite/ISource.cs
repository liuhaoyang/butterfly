using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Butterfly.Consumer.Lite
{
    public interface ISource<T>
    { 
        ISourceBlock<T> SourceBlock { get; }

        void Post(T item);

        Task Complete();
    }
}