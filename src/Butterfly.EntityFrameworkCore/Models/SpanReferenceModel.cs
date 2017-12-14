using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore.Models
{
    [Table("SpanReferences")]
    public class SpanReferenceModel
    {     
        [Key]
        public long SpanReferenceId { get; set; }
        
        public string Reference { get; set; }
 
        public string SpanId { get; set; }

        public string ParentId { get; set; }
    }
}