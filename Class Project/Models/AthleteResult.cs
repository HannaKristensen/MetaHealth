namespace Class_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class AthleteResult
    {
        [Key]
        public int AthleteResultsID { get; set; }

        public float RaceTime { get; set; }

        public int LocationID { get; set; }

        public int AthleteID { get; set; }

        public int ResultID { get; set; }

        public virtual Result Result { get; set; }

        public virtual Athlete Athlete { get; set; }

        public virtual Location Location { get; set; }
    }
}
