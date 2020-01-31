using System;
using System.Collections.Generic;
using System.Linq;

namespace Class_Project.Models.View_Models
{
    public class AthleteDetailsViewModelcs
    {
        private DBContext db = new DBContext();

        public AthleteDetailsViewModelcs(Athlete athlete)
        {
            Name = athlete.Name;
            Gender = athlete.Gender;
            Team = athlete.Team.Name;
            Events = new string[athlete.AthleteResults.Select(x => x.Result.ToString()).ToArray().Count()];
            Events = athlete.AthleteResults.Select(x => x.Result.Event).ToArray();
            Times = new string[athlete.AthleteResults.Select(x => x.RaceTime.ToString()).ToArray().Count()];
            Times = athlete.AthleteResults.Select(x => x.RaceTime.ToString()).ToArray();
            AthelteID = athlete.AthleteID;
        }

        public int AthelteID { get; private set; }
        public string[] Events { get; private set; }
        public string[] Times { get; private set; }
        public string Name { get; private set; }
        public string Team { get; private set; }
        public string Gender { get; private set; }
    }
}