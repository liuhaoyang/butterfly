using System;
using System.Collections.Generic;

namespace Butterfly.Server.ViewModels
{
    public class TraceViewModel
    {
        public string TraceId { get; set; }

        public long Duration { get; set; }

        public DateTime StartTimestamp { get; set; }

        public DateTime FinishTimestamp { get; set; }

        public List<TraceService> Services { get; set; }
    }

    public class TraceService
    {
        public string Name { get; }

        public TraceService(string name = null)
        {
            Name = name ?? "unknown";
        }
    }
}