using System;
using System.Collections.Generic;
using System.Text;

namespace Butterfly.Storage.Query
{
    public class TimeRangeQuery
    {
        public DateTimeOffset? StartTimestamp { get; set; }

        public DateTimeOffset? FinishTimestamp { get; set; }

        public virtual void Ensure()
        {
            if (FinishTimestamp == null)
            {
                FinishTimestamp = DateTimeOffset.UtcNow;
            }
            if (StartTimestamp == null)
            {
                StartTimestamp = FinishTimestamp.Value.AddHours(-1);
            }
        }
    }
}
