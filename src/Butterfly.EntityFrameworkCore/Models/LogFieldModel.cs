using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Butterfly.EntityFrameworkCore.Models
{
    [Table("LogFields")]
    public class LogFieldModel
    {
        [Key]
        public long LogFieldId { get; set; }
        
        public string LogId { get; set; }
        
        public string Key { get; set; }

        public string Value { get; set; }
    }
}