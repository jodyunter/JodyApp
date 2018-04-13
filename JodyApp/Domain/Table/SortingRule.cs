using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class SortingRule:DomainObject
    {
        public RecordTableDivision DivisionToSort { get; set; }
        public int GroupNumber { get; set; }                
        public RecordTableDivision DivisionToGetTeamsFrom { get; set; }
        public string PositionsToUse { get; set; }
    }
}
