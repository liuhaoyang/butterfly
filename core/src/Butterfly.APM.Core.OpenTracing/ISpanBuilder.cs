using System.Collections.Generic;

namespace Butterfly.APM.Core.OpenTracing
{
    public interface ISpanBuilder
    {
        ISpanBuilder Reference(SpanReference reference);

        IReadOnlyCollection<SpanReference> References { get; }

        string OperationName { get; }

        Baggage Baggage { get; }
    }
}