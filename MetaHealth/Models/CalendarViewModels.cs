
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Google.Apis.Tasks.v1;
using Google.Apis.Tasks.v1.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace Calendar.ASP.NET.MVC5.Models
{
    /// <summary>
    /// A view model for the UpcomingEvents view.
    /// </summary>
    public class UpcomingEventsViewModel
    {
        /// <summary>
        /// Gets or sets a sequence of event groups to display.
        /// </summary>
        [Required]
        public IEnumerable<CalendarEventGroup> EventGroups { get; set; }

        public string[] MultiTask { get; set; }
        public string[] MultiList { get; set; }
        public string[] MultiTaskID { get; set; }
    }
}