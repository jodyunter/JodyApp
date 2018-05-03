using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class RoundGroupRule:DomainObject
    {
        public static int SERIES_WINNER = 0;
        public static int SERIES_LOSER = 1;

        public static int SORT_BY_DIVISION = 0;
        public static int SORT_BY_NONE = 1;

        public RoundRule ParentRule { get; set; }        
        public int SortType { get; set; } //sort by division rank? League rank? random? no sort?
        public int SortyTypeValue { get; set; } //this is either division level or not applicable.  Teams should only share one division at the assigned level or it will not work
        public Division FromDivision { get; set; }        
        public Series FromSeries { get; set; } //this means that all series need to be created at playoff creation time
        public int FromValue { get; set; } //ranking or WINNER/LOSER
        public int GroupIdentifier { get; set; }  //this is how we know which rules go with which group

        public static RoundGroupRule CreateFromDivision()
        {
            return null;
        }

        public static RoundGroupRule CreateFromSeriesWinner()
        {
            return null;
        }

        public static RoundGroupRule CreateFromSeriesLoser()
        {
            return null;
        }
    }
}
