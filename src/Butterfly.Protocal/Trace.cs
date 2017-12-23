using System.Collections.Generic;

namespace Butterfly.Protocol
{
    public class Trace
    {
        public string TraceId { get; set; }

        public List<Span> Spans { get; set; }
    }
}