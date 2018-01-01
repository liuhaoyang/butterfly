using System;

namespace Butterfly.Storage.Query
{
    public class TraceQuery : PageQuery
    {
        public string ServiceName { get; set; }

        public DateTimeOffset? StartTimestamp { get; set; }

        public DateTimeOffset? FinishTimestamp { get; set; }

        public int? MinDuration { get; set; }

        public int? MaxDuration { get; set; }
        
        public string Tags { get; set; }
    }
}