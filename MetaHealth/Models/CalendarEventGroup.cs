using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Google.Apis.Calendar.v3.Data;

namespace Calendar.ASP.NET.MVC5.Models
{
    /// <summary>
    /// A labeled group of calendar events.
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Google.Apis.Calendar.v3.Data;

    namespace Calendar.ASP.NET.MVC5.Models
    {
        public class CalendarEventGroup
        {
            [Required]
            public string GroupTitle { get; set; }

            /// <summary>
            /// Gets or sets a sequence of calendar events to show under the group title.
            /// </summary>
            [Required]
            public IEnumerable<Event> Events { get; set; }
        }
    }