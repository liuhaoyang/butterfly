using System.Collections.Generic;

namespace Butterfly.Core.Protocol
{
    public class SpanDescriptor
    {
        public string OperationName { get; }

        public string TraceId { get; set; }

        public string SpanId { get; set; }

        public bool Sampled { get; set; }

        public long StartTimestamp { get; set; }

        public long FinishTimestamp { get; set; }

        public long Duration { get; set; }

        public IEnumerable<TagDescriptor> Tags { get; set; }

        public IEnumerable<BaggageDescriptor> Baggages { get; set; }

        public IEnumerable<ReferenceDescriptor> References { get; set; }
    }
}
