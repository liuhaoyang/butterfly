using System;

namespace Butterfly.Storage.Query
{
    public class DependencyQuery
    {
        public DateTimeOffset? StartTimestamp { get; set; }

        public DateTimeOffset? FinishTimestamp { get; set; }
    }
}