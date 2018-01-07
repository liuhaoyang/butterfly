using System.Collections.Generic;

namespace Butterfly.DataContract.Tracing
{
    public class Trace
    {
        public string TraceId { get; set; }

        public List<Span> Spans { get; set; }
    }
}