using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.DataContract.Tracing
{
    public class Service
    {
        public string Name { get; set; }

        public string SpanId { get; set; }

        public DateTimeOffset StartTimestamp { get; set; }
    }
}
