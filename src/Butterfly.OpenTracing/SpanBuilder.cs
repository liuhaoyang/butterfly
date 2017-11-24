using System;

namespace Butterfly.OpenTracing
{
    public class SpanBuilder : ISpanBuilder
    {
        private readonly string _operationName;
        private readonly Baggage _baggage;
        private readonly SpanReferenceCollection _spanReferences;

        public string OperationName => _operationName;

        public Baggage Baggage => _baggage;

        public SpanReferenceCollection References => _spanReferences;

        public SpanBuilder(string operationName)
        {
            _operationName = operationName ?? throw new ArgumentNullException(nameof(operationName));
            _baggage = new Baggage();
            _spanReferences = new SpanReferenceCollection();
        }
    }
}