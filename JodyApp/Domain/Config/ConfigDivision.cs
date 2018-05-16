using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigDivision:BaseConfigItem
    {
        private string _shortName;

        virtual public League League { get; set; }
        public string Name { get; set; }
        public string ShortName { get { if (_shortName == null) return Name; else return _shortName; } set { _shortName = value; } }
        virtual public ConfigDivision Parent { get; set; }
        public int Level { get; set; }
        public int Order { get; set; }
        virtual public List<ConfigTeam> Teams { get; set; }
        virtual public List<ConfigScheduleRule> ScheduleRules { get; set; } 
        virtual public List<ConfigSortingRule> SortingRules { get; set; }
        virtual public ConfigCompetition Competition { get; set; }

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

        public ConfigDivision() { }

        public override bool Equals(object obj)
        {
            var division = obj as ConfigDivision;
            return division != null &&
                   _shortName == division._shortName &&
                   EqualityComparer<League>.Default.Equals(League, division.League) &&
                   Name == division.Name &&
                   ShortName == division.ShortName &&
                   EqualityComparer<ConfigDivision>.Default.Equals(Parent, division.Parent) &&
                   Level == division.Level &&
                   Order == division.Order &&
                   EqualityComparer<List<ConfigTeam>>.Default.Equals(Teams, division.Teams) &&
                   EqualityComparer<ConfigCompetition>.Default.Equals(Competition, division.Competition) &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, division.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, division.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = 1450725381;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(_shortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<League>.Default.GetHashCode(League);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ShortName);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(Parent);
            hashCode = hashCode * -1521134295 + Level.GetHashCode();
            hashCode = hashCode * -1521134295 + Order.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<List<ConfigTeam>>.Default.GetHashCode(Teams);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigCompetition>.Default.GetHashCode(Competition);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
