using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class RecordTableDivision:Division
    {
        public RecordTableDivision() { }
        public RecordTableDivision(string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules) : base(name, shortName, level, order, parent)
        {
            if (sortingRules == null) sortingRules = new List<SortingRule>();
            SortingRules = sortingRules;
        }
         

        public List<SortingRule> SortingRules { get; set; }
        public List<DivisionRank> Rankings { get; set; }

    }
}
