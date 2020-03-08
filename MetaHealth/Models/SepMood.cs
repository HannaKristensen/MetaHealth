namespace MetaHealth.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class SepMood
    {
        [Key]
        public int PK { get; set; }

        [Required]
        [StringLength(128)]
        public string UserID { get; set; }

        public int MoodNum { get; set; }

        public DateTime Date { get; set; }
    }
}
