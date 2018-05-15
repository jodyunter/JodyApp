using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigGroup:DomainObject, BaseConfigItem
    {
        public string Name { get; set; }
        virtual public ConfigCompetition Playoff { get; set; }
        virtual public List<ConfigGroupRule> GroupRules { get; set; }
        virtual public ConfigDivision SortByDivision { get; set; }

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigGroup() { }

        public ConfigGroup(string name, ConfigCompetition playoff, List<ConfigGroupRule> groupRules, ConfigDivision sortByDivision, int? firstYear, int? lastYear)
        {
            Name = name;
            Playoff = playoff;
            GroupRules = groupRules;
            SortByDivision = sortByDivision;
            FirstYear = firstYear;
            LastYear = lastYear;
        }

        public override bool Equals(object obj)
        {
            var group = obj as ConfigGroup;
            return group != null &&
                   Name == group.Name &&
                   EqualityComparer<ConfigCompetition>.Default.Equals(Playoff, group.Playoff) &&
                   EqualityComparer<ConfigDivision>.Default.Equals(SortByDivision, group.SortByDivision) &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, group.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, group.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = 765020825;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigCompetition>.Default.GetHashCode(Playoff);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(SortByDivision);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
