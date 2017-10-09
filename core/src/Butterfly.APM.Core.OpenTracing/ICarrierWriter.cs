using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ICarrierWriter
    {
        void Write(ISpanContext spanContext, ICarrier carrier);

        Task WriteAsync(ISpanContext spanContext, ICarrier carrier);
    }
}