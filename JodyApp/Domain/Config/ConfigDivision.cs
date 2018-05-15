using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigDivision:DomainObject, BaseConfigItem
    {
        private string _shortName;

        public League League { get; set; }
        public string Name { get; set; }
        public string ShortName { get { if (_shortName == null) return Name; else return _shortName; } set { _shortName = value; } }
        virtual public ConfigDivision Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        public List<ConfigTeam> Teams { get; set; }
        public List<ConfigScheduleRule> ScheduleRules { get; set; } 
        public List<ConfigSortingRule> SortingRules { get; set; }
        public ConfigCompetition Competition { get; set; }

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigDivision(League league, ConfigCompetition competition, string name, string shortName, int level, int order, ConfigDivision parent, int? firstYear, int? lastYear)
        {
            this.Name = name;
            this.ShortName = shortName;
            this.Level = level;
            this.Order = order;
            this.Parent = parent;
            this.League = league;
            this.Competition = competition;
            Teams = new List<ConfigTeam>();            
            SortingRules = new List<ConfigSortingRule>();
            FirstYear = firstYear;
            LastYear = lastYear;
        }

        public ConfigDivision(League league, ConfigCompetition competition, string name, string shortName, int level, int order, ConfigDivision parent, List<ConfigSortingRule> sortingrules, int? firstYear, int? lastYear)
        {
            this.Name = name;
            this.ShortName = shortName;
            this.Level = level;
            this.Order = order;
            this.Parent = parent;
            this.League = league;
            this.Competition = competition;
            Teams = new List<ConfigTeam>();
            SortingRules = sortingrules;
            FirstYear = firstYear;
            LastYear = lastYear;
        }

        public ConfigDivision()
        {
        }
    }
}
