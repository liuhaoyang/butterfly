using System.Collections.Generic;

namespace Butterfly.APM.OpenTracing
{
    public interface ISpanBuilder
    {
        SpanReferenceCollection References { get; }

        string OperationName { get; }

        Baggage Baggage { get; }
    }
}