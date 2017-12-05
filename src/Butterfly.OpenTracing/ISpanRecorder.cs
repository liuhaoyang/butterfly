using System.Threading.Tasks;

namespace Butterfly.OpenTracing
{
    public interface ISpanRecorder
    {
        Task RecordAsync(ISpan span);
    }
}