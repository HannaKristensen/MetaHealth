namespace MetaHealth
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mood
    {
        [Key]
        public int PK { get; set; }

        public int FK_MoodsInBetween { get; set; }

        public int MoodNum { get; set; }

        public DateTime Date { get; set; }

        public virtual MoodsInBetween MoodsInBetween { get; set; }
    }
}
