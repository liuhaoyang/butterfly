using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanChannel
    {
        Task FlowAsync(ISpan span);
    }
}