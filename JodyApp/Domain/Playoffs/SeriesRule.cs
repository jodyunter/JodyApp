using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class SeriesRule : DomainObject
    {
        public static int FROM_SERIES = 0;
        public static int FROM_DIVISION = 1;
        public static int FROM_POOL = 2;

        public static int SERIES_WINNER = 0;
        public static int SERIES_LOSERS = 0;        

        public League League { get; set; }
        public Playoff Playoff { get; set; }
        public Series Series { get; set; }
        
        public int TeamFromType { get; set; }
        public Division TeamFromDivision { get; set; }
        public String TeamFromPoolName { get; set; }
        public Series TeamFromSeries { get; set; }
        public int TeamFromValue { get; set; }

        public bool HomeTeam { get; set; }

        public static SeriesRule CreateFromSeriesWinner()
        {
            return null;
        }

        public static SeriesRule CreateFromSeriesLoser()
        {
            return null;
        }

        public static SeriesRule CreateFromDivision()
        {
            return null;
        }
        public static SeriesRule CreateFromPool()
        {
            return null;
        }

    }
}
