using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSortingRule:BaseConfigItem
    {
        public string Name { get; set; }
        public int GroupNumber { get; set; }
        public ConfigDivision Division { get; set; }
        public ConfigDivision DivisionToGetTeamsFrom { get; set; }
        public string PositionsToUse { get; set; }
        public int DivisionLevel { get; set; }
        public int Type { get; set; }

        public ConfigSortingRule() { }

        public ConfigSortingRule(string name, int groupNumber, ConfigDivision division, ConfigDivision divisionToGetTeamsFrom, string positionsToUse, int divisionLevel, int type, int? firstYear, int? lastYear)
        {
            Name = name;
            GroupNumber = groupNumber;
            Division = division;
            DivisionToGetTeamsFrom = divisionToGetTeamsFrom;
            PositionsToUse = positionsToUse;
            DivisionLevel = divisionLevel;
            Type = type;
            FirstYear = firstYear;
            LastYear = lastYear;
        }

        public override bool Equals(object obj)
        {
            var rule = obj as ConfigSortingRule;
            return rule != null &&
                   Name == rule.Name &&
                   GroupNumber == rule.GroupNumber &&
                   EqualityComparer<ConfigDivision>.Default.Equals(Division, rule.Division) &&
                   EqualityComparer<ConfigDivision>.Default.Equals(DivisionToGetTeamsFrom, rule.DivisionToGetTeamsFrom) &&
                   PositionsToUse == rule.PositionsToUse &&
                   DivisionLevel == rule.DivisionLevel &&
                   Type == rule.Type &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, rule.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, rule.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = 2062850672;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + GroupNumber.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(Division);
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigDivision>.Default.GetHashCode(DivisionToGetTeamsFrom);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PositionsToUse);
            hashCode = hashCode * -1521134295 + DivisionLevel.GetHashCode();
            hashCode = hashCode * -1521134295 + Type.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
