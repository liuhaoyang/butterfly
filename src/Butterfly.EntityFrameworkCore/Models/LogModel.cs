using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Butterfly.DataContract.Tracing;

namespace Butterfly.EntityFrameworkCore.Models
{
    [Table("Logs")]
    public class LogModel
    {
        [Key]
        public string LogId { get; set; }
        
        public string SpanId { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        public List<LogFieldModel> Fields { get; set; }
    }
}