using System.Collections.Generic;

namespace Butterfly.OpenTracing
{
    public interface ISpanBuilder
    {
        SpanReferenceCollection References { get; }

        string OperationName { get; }

        Baggage Baggage { get; }

        bool? Sampled { get; }
    }
}