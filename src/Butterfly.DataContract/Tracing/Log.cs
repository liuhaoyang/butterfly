using System;
using System.Collections.Generic;

namespace Butterfly.DataContract.Tracing
{
    public class Log
    {
        public DateTimeOffset Timestamp { get; set; }

        public ICollection<LogField> Fields { get; set; } = new List<LogField>();
    }
}