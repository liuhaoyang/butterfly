using System;
using System.Linq;

namespace Butterfly.OpenTracing
{
    public class SpanBuilder : ISpanBuilder
    {
        public string OperationName { get; }

        public Baggage Baggage { get; }

        public SpanReferenceCollection References { get; }

        public bool? Sampled
        {
            get { return References.FirstOrDefault()?.SpanContext?.Sampled; }
        }

        public SpanBuilder(string operationName)
        {
            OperationName = operationName ?? throw new ArgumentNullException(nameof(operationName));
            Baggage = new Baggage();
            References = new SpanReferenceCollection();
        }
    }
}