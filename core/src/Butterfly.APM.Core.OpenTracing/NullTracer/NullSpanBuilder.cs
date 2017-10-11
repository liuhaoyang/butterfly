using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.APM.Core.OpenTracing.NullTracer
{
    public class NullSpanBuilder : ISpanBuilder
    {
        public static readonly ISpanBuilder Instance = new NullSpanBuilder();

        public SpanReferenceCollection References { get; } = new SpanReferenceCollection();

        public string OperationName => string.Empty;

        public Baggage Baggage { get; } = new Baggage();
    }
}
