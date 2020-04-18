using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MetaHealth.Models
{
    [Table("CustomList")]
    public partial class CustomList
    {
        [Key]
        public int PK { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [Required]
        [StringLength(1000)]
        public string TaskTitle { get; set; }
    }
}