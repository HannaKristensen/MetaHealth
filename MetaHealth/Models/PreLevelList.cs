namespace MetaHealth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PreLevelList")]
    public partial class PreLevelList
    {
        [Key]
        public int PK { get; set; }

        public int Level { get; set; }

        [Required]
        [StringLength(256)]
        public string Task { get; set; }
    }
}
