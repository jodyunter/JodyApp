using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigDivisionViewModel:BaseViewModel
    {        
        public ReferenceObject League { get; set; }        
        public string ShortName { get; set; }
        public ReferenceObject Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public List<ConfigTeamViewModel> Teams { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigDivisionViewModel(int? id, int? leagueId, string league, string name, string shortName, int? parentId, string parent, int level, int order, List<ConfigTeamViewModel> teams, int? firstYear, int? lastYear)
        {
            Id = id;
            League = new ReferenceObject(leagueId, league);
            Name = name;
            ShortName = shortName;
            Parent = new ReferenceObject(parentId, parent);
            Level = level;
            Order = order;
            Teams = teams;
            FirstYear = firstYear;
            LastYear = lastYear;
        }
        //TODO add these in
        //virtual public List<ConfigScheduleRule> ScheduleRules { get; set; }
        //virtual public List<ConfigSortingRule> SortingRules { get; set; }
        //virtual public ConfigCompetition Competition { get; set; }


    }
}
