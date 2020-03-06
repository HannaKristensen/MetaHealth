namespace MetaHealth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomLevel")]
    public partial class CustomLevel
    {
        [Key]
        public int PK { get; set; }

        public int FK_List { get; set; }

        [Required]
        [StringLength(256)]
        public string Task { get; set; }

        public virtual List List { get; set; }
    }
}