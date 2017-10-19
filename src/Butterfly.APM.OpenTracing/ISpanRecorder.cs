using System.Threading.Tasks;

namespace Butterfly.APM.OpenTracing
{
    public interface ISpanRecorder
    {
        Task RecordAsync(ISpan span);
    }
}