using System.Threading.Tasks;

namespace Butterfly.APM.OpenTracing
{
    public interface ICarrierReader
    {
        ISpanContext Read(ICarrier carrier);

        Task<ISpanContext> ReadAsync(ICarrier carrier);
    }
}