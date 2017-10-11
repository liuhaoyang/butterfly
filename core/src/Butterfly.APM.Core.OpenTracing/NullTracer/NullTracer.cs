using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing.NullTracer
{
    public class NullTracer : ITracer
    {
        public static readonly ITracer Instance = new NullTracer();

        public ISpanContext Extract(ICarrierReader carrierReader, ICarrier carrier)
        {
            return new NullSpanContext();
        }

        public Task<ISpanContext> ExtractAsync(ICarrierReader carrierReader, ICarrier carrier)
        {
            return Task.FromResult<ISpanContext>(new NullSpanContext());
        }

        public void Inject(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
           
        }

        public Task InjectAsync(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
            return Task.FromResult(0);
        }

        public ISpan Start(ISpanBuilder spanBuilder)
        {
            return new NullSpan();
        }
    }
}
