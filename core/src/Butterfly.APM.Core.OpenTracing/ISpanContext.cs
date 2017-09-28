using System;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanContext
    {
        string TraceId { get; }

        string SpanId { get; }

        bool Sampled { get; }

        ReadOnlyBaggage GetBaggage();
    }
}