using System.Threading.Tasks;

namespace Butterfly.Pipeline.Lite
{
    public interface IPipelineTarget
    {
        Task Executing();

        Task Complete();
    }
}
