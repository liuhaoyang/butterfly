using System;
using System.Linq;
using System.Threading.Tasks;

namespace Butterfly.APM.Core.OpenTracing
{
    public class Tracer : ITracer
    {
        private readonly ISpanContextFactory _spanContextFactory;
        private readonly ISpanChannel _spanQueue;
        private readonly ISampler _sampler;

        public Tracer(ISpanContextFactory spanContextFactory, ISpanChannel spanQueue, ISampler sampler)
        {
            _spanContextFactory = spanContextFactory ?? throw new ArgumentNullException(nameof(spanContextFactory));
            _spanQueue = spanQueue ?? throw new ArgumentNullException(nameof(spanQueue));
            _sampler = sampler ?? throw new ArgumentNullException(nameof(sampler));
        }

        public ISpanContext Extract(ICarrierReader carrierReader, ICarrier carrier)
        {
            if (carrierReader == null)
            {
                throw new ArgumentNullException(nameof(carrierReader));
            }
            return carrierReader.Read(carrier);
        }

        public Task<ISpanContext> ExtractAsync(ICarrierReader carrierReader, ICarrier carrier)
        {
            return carrierReader.ReadAsync(carrier);
        }

        public void Inject(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
            if (carrierWriter == null)
            {
                throw new ArgumentNullException(nameof(carrierWriter));
            }
            if (spanContext == null)
            {
                throw new ArgumentNullException(nameof(spanContext));
            }
            carrierWriter.Write(spanContext.Package(), carrier);
        }

        public Task InjectAsync(ISpanContext spanContext, ICarrierWriter carrierWriter, ICarrier carrier)
        {
            if (carrierWriter == null)
            {
                throw new ArgumentNullException(nameof(carrierWriter));
            }
            if (spanContext == null)
            {
                throw new ArgumentNullException(nameof(spanContext));
            }
            return carrierWriter.WriteAsync(spanContext.Package(), carrier);
        }

        public ISpan Start(ISpanBuilder spanBuilder)
        {
            if (spanBuilder == null)
            {
                throw new ArgumentNullException(nameof(spanBuilder));
            }

            var traceId = spanBuilder.References?.FirstOrDefault()?.SpanContext?.TraceId ?? Guid.NewGuid().ToString();
            var spanId = Guid.NewGuid().ToString();

            Baggage baggage = new Baggage();

            foreach (var reference in spanBuilder.References)
            {
                baggage.Merge(reference.SpanContext.Baggage);
            }
            var spanContext = _spanContextFactory.Create(new SpanContextPackage(traceId, spanId, _sampler.ShouldSample(), baggage));
            return new Span(spanBuilder.OperationName, spanContext, _spanQueue);
        }
    }
}