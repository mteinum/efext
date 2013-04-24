// @mteinum

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfExt.Tests
{
    [Table("Number")]
    public class PhoneNumber
    {
        [Key]
        public string Number { get; set; }

        public string Fax { get; set; }
    }
}