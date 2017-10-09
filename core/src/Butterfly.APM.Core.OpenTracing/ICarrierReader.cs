using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ICarrierReader
    {
        ISpanContext Read(ICarrier carrier);

        Task<ISpanContext> ReadAsync(ICarrier carrier);
    }
}