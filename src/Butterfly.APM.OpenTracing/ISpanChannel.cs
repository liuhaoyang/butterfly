using System.Threading.Tasks;

namespace Butterfly.APM.OpenTracing
{
    public interface ISpanChannel
    {
        Task FlowAsync(ISpan span);
    }
}