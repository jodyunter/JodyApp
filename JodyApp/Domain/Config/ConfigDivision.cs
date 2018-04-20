using JodyApp.Domain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigDivision:RecordTableDivision
    {
        public ConfigDivision() { }
        public ConfigDivision(League league, string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules) : base(league, name, shortName, level, order, parent, sortingRules) { }

        
    }
}
