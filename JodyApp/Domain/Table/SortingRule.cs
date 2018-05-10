using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    [Table("SortingRules")]
    public class SortingRule:DomainObject
    {
        public static int SINGLE_DIVISION = 0;
        public static int FULL_DIVISION_LEVEL = 1;

        //need to modify so that we have types, such as Division or Division Level
        public String Name { get; set; }
        public int GroupNumber { get; set; }                  
        public Division Division { get; set; }        
        public Division DivisionToGetTeamsFrom { get; set; }
        public string PositionsToUse { get; set; }
        public int DivisionLevel { get; set; }
        public int Type { get; set; }

        public SortingRule() { }
        public SortingRule(Division owningDivision, Division divisionToGetTeamsFrom, SortingRule rule)
        {
            this.Division = owningDivision;
            this.Name = rule.Name;
            this.GroupNumber = rule.GroupNumber;
            this.DivisionLevel = rule.DivisionLevel;
            this.PositionsToUse = rule.PositionsToUse;
            this.Type = rule.Type;
            this.DivisionToGetTeamsFrom = divisionToGetTeamsFrom;
        }
    }
}
