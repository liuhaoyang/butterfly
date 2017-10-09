using System;
using System.Collections.Generic;

namespace Butterfly.APM.Core.OpenTracing
{
    public class SpanBuilder : ISpanBuilder
    {
        private readonly List<SpanReference> _references;
        private readonly string _operationName;
        private readonly Baggage _baggage;

        public string OperationName => _operationName;

        public IReadOnlyCollection<SpanReference> References => _references;

        public Baggage Baggage => throw new NotImplementedException();

        public SpanBuilder(string operationName)
        {
            _operationName = operationName ?? throw new ArgumentNullException(nameof(operationName));
            _references = new List<SpanReference>();
            _baggage = new Baggage();
        }

        public ISpanBuilder Reference(SpanReference reference)
        {
            if (reference.SpanContext == null)
            {
                throw new ArgumentNullException(nameof(reference.SpanContext));
            }
            _references.Add(reference);
            _baggage.Merge(reference.SpanContext.GetBaggage());
            return this;
        }
    }
}