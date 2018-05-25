using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class ConfigDivisionViewModel:BaseViewModel
    {
        public int? Id { get; set; }
        public string League { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public List<ConfigTeamViewModel> Teams { get; set; }

        public ConfigDivisionViewModel(int? id, string league, string name, string shortName, string parent, int level, int order, List<ConfigTeamViewModel> teams)
        {
            Id = id;
            League = league;
            Name = name;
            ShortName = shortName;
            Parent = parent;
            Level = level;
            Order = order;
            Teams = teams;
        }
        //TODO add these in
        //virtual public List<ConfigScheduleRule> ScheduleRules { get; set; }
        //virtual public List<ConfigSortingRule> SortingRules { get; set; }
        //virtual public ConfigCompetition Competition { get; set; }


    }
}
