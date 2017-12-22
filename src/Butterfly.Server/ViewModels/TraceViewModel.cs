using System;
using System.Collections.Generic;

namespace Butterfly.Server.ViewModels
{
    public class TraceViewModel
    {
        public string TraceId { get; set; }

        public long Duration { get; set; }

        public DateTimeOffset StartTimestamp { get; set; }

        public DateTimeOffset FinishTimestamp { get; set; }

        public List<string> Services { get; set; }
    }
}