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
        public List<ConfigScheduleRule> ScheduleRules { get; set; } 
        public List<ConfigSortingRule> SortingRules { get; set; }
        public ConfigSeason Season { get; set; }

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }
        

    }
}
