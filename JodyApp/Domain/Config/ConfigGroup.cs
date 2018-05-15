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
    }
}
