using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Butterfly.Server.Models
{
    public class SpanResponse
    {
        public string SpanId { get; set; }

        public string TraceId { get; set; }

        public string OperationName { get; set; }

        /// <summary>
        /// duration(microsecond)
        /// </summary>
        public long Duration { get; set; }

        public DateTimeOffset StartTimestamp { get; set; }

        public DateTimeOffset FinishTimestamp { get; set; }
    }
}
