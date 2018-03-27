using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class StandingsSorter
    {
        public static int SORT_BY_LEAGUE = 0;
        public static int SORT_BY_DIVISION = 1;

        private int DivisionLevel { get; set; }     

        public StandingsSorter() { }


        
        public Dictionary<Division, Division> SortDivisions(RecordTable table)
        {
            return null;
        }
        public int SortByDivisionLevel(RecordTableTeam a, RecordTableTeam b, int divisionLevel)
        {
            DivisionLevel = divisionLevel;

            return SortByDivisionLevel(a, b);
        }

        public int SortByDivisionLevel(RecordTableTeam a, RecordTableTeam b)
        {

            Division a0 = a.Division;
            Division b0 = b.Division;

            if (a0 == null || b0 == null) {
                throw new ApplicationException("Can't compare divisions when they don't have a division!");
            }

            while (a0.Level != DivisionLevel)
            {
                a0 = a0.Parent;
            }

            while (b0.Level != DivisionLevel)
            {
                b0 = b0.Parent;
            }

            if (a0.Order == b0.Order)
            {
                return a.CompareTo(b);
            } else
            {
                return a0.Order.CompareTo(b0.Order);
            }
            
        }
    }
}
