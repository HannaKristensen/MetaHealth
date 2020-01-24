namespace Class_Project.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Location
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Location()
        {
            AthleteResults = new HashSet<AthleteResult>();
        }

        public int LocationID { get; set; }

        [Column("Location")]
        [Required]
        [StringLength(50)]
        public string Location1 { get; set; }

        public DateTime MeetDate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AthleteResult> AthleteResults { get; set; }
    }
}
