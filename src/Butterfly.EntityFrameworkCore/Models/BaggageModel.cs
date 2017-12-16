using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Butterfly.EntityFrameworkCore.Models
{
    [Table("Baggages")]
    public class BaggageModel
    {
        [Key]
        public long BaggageId { get; set; }
        
        
        
        public string SpanId { get; set; }
        
        public string Key { get; set; }

        public string Value { get; set; }
    }
}