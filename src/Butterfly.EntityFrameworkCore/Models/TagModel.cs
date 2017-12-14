using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Butterfly.Protocol;

namespace Butterfly.EntityFrameworkCore.Models
{
    [Table("Tags")]
    public class TagModel
    {
        [Key]
        public long TagId { get; set; }
     
        public string SpanId { get; set; }
        
        public string Key { get; set; }

        public string Value { get; set; } 
    }
}