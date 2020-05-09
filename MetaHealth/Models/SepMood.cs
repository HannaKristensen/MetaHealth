namespace MetaHealth.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SepMood
    {
        [Key]
        public int PK { get; set; }

        [StringLength(128)]
        public string UserID { get; set; }

        [Range(1, 5)]
        public int MoodNum { get; set; }

        public DateTime Date { get; set; }

        //reason for why the mood is being added
        public string Reason { get; set; }

        //Constructor made for testing
        //public SepMood(int moodVal)
        //{
        //    UserID = "";
        //    MoodNum = moodVal;
        //    Date = DateTime.MinValue;
        //    Reason = "";
        //}

        //empty constructor for viewmodel
        public SepMood()
        {
            UserID = "";
            MoodNum = 0;
            Date = DateTime.MinValue;
            Reason = "";
        }
    }
}