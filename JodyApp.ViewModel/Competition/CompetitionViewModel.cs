using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class CompetitionViewModel : BaseViewModel
    {
        public ReferenceObject League { get; set; }
        public int Year { get; set; }
        public bool Started { get; set; }
        public string CompetitionType { get; set; }
        public bool Complete { get; set; }
        public int StartingDay { get; set; }

        public CompetitionViewModel(int? id, ReferenceObject league, string name, int year, string competitionType, bool started, bool complete, int startingDay)
        {
            Id = id;
            League = league;
            Name = name;
            Year = year;
            Started = started;
            Complete = complete;
            StartingDay = startingDay;
            CompetitionType = competitionType;
        }

    }
}
