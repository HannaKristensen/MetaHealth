using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MetaHealth.Models
{
    public class CreateEvent
    {
        [Required]
        public string Summary { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime Start { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true)]
        public DateTime End { get; set; }
    }
}