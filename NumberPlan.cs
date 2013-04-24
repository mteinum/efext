using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfExt
{
    [Table("NumberPlan")]
    public class NumberPlan
    {
        public int OperatorId { get; set; }
        [Key]
        public string LowerNumber { get; set; }
        public string UpperNumber { get; set; }
    }
}