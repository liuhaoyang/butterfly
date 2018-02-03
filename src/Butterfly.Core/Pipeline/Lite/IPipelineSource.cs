using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace Butterfly.Pipeline.Lite
{
    public interface IPipelineSource<T>
    { 
        ISourceBlock<T> SourceBlock { get; }

        void Post(T item);

        Task Complete();
    }
}