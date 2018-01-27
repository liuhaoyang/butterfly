using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Storage.Query
{
    public class TimeRangeQuery
    {
        public DateTimeOffset? StartTimestamp { get; set; }

        public DateTimeOffset? FinishTimestamp { get; set; }
    }
}
