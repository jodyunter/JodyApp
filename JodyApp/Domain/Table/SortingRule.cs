using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class SortingRule:DomainObject
    {        
        //need to modify so that we have types, such as Division or Division Level
        public String Name { get; set; }
        public int GroupNumber { get; set; }                
        public RecordTableDivision DivisionToGetTeamsFrom { get; set; }
        public string PositionsToUse { get; set; }
    }
}
