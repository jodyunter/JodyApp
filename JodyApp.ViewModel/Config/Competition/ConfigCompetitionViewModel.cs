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

        public ConfigCompetitionViewModel(int? id, string name, int? leagueId, string leagueName, string competitionType, int? refId, string refName, int order, int? firstYear, int? lastYear)
        {
            Id = id;
            Name = name;
            League = leagueId == null ? null : new ReferenceObject(leagueId, leagueName);
            CompetitionType = competitionType;
            ReferenceCompetition = refId == null ? null : new ReferenceObject(refId, refName);
            Order = order;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
