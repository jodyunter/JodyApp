using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSortingRule:DomainObject, BaseConfigItem
    {
        public string Name { get; set; }
        public int GroupNumber { get; set; }
        public ConfigDivision Division { get; set; }
        public ConfigDivision DivisionToGetTeamsFrom { get; set; }
        public string PositionsToUse { get; set; }
        public int DivisionLevel { get; set; }
        public int Type { get; set; }
        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }
    }
}
