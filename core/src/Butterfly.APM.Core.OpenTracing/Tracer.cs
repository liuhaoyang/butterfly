using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing
{
    public class Tracer : ITracer
    {
        public ISpanContext Extract(ICarrierReader carrierReader, ICarrier carrier)
        {
            throw new NotImplementedException();
        }

        public Task<ISpanContext> ExtractAsync(ICarrierReader carrierReader, ICarrier carrier)
        {
            throw new NotImplementedException();
        }

        public void Inject(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
            if (carrierWriter == null)
            {
                throw new ArgumentNullException(nameof(carrierWriter));
            }
            carrierWriter.Write(spanContext, carrier);
        }

        public Task InjectAsync(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
            if (carrierWriter == null)
            {
                throw new ArgumentNullException(nameof(carrierWriter));
            }
            return carrierWriter.WriteAsync(spanContext, carrier);
        }

        public ISpan Start(ISpanBuilder spanBuilder)
        {
            throw new NotImplementedException();
        }
    }
}
