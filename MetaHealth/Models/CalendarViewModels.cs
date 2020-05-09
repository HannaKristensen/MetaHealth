using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MetaHealth.Models;

namespace Calendar.ASP.NET.MVC5.Models
{
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
        public List<SepMood> SepMood { get; set; }
        public List<DateTime> MoodDate { get; set; }
        public List<int> MoodNum { get; set; }
        public Dictionary<string, double> MoodDictionaryAvg { get; set; }
        public Dictionary<string, List<int>> MoodDictionaryByDay { get; set; }

        public string[] CustomTask { get; set; }
        public string[] id { get; set; }
        public int[] PK { get; set; }

        public bool EventsOrNah { get; set; }
        
        public string UserName { get; set; }

        public string DailyTask { get; set; }

        
}
}