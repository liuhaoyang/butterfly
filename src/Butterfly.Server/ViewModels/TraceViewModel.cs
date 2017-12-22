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

        public List<TraceApplication> Applications { get; set; }
    }

    public class TraceApplication
    {
        public string Name { get; }

        public TraceApplication(string name = null)
        {
            Name = name ?? "unknown";
        }
    }
}