using System.Threading.Tasks;

namespace Butterfly.APM.OpenTracing
{
    public interface ICarrierWriter
    {
        void Write(SpanContextPackage spanContext, ICarrier carrier);

        Task WriteAsync(SpanContextPackage spanContext, ICarrier carrier);
    }
}