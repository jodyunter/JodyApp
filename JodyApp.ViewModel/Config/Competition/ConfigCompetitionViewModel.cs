using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigCompetitionViewModel:BaseViewModel
    {
        public const string PLAYOFF = "Playoff";
        public const string SEASON = "Season";

        public ReferenceObject League { get; set; }
        public string CompetitionType { get; set; }
        public ReferenceObject ReferenceCompetition { get; set; }
        public int  Order { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigCompetitionViewModel(int? id, string name, ReferenceObject league, string competitionType, ReferenceObject refComp, int order, int? firstYear, int? lastYear)
        {
            Id = id;
            Name = name;
            League = league;
            CompetitionType = competitionType;
            ReferenceCompetition = refComp;
            Order = order;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
