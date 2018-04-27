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
        
        public int HomeTeamFromType { get; set; }
        public Division HomeTeamFromDivision { get; set; }
        public String HomeTeamFromPoolName { get; set; }
        public Series HomeTeamFromSeries { get; set; }
        public int HomeTeamFromValue { get; set; }

        public int AwayTeamFromType { get; set; }
        public Division AwayTeamFromDivision { get; set; }
        public String AwayTeamFromPoolName { get; set; }
        public Series AwayTeamFromSeries { get; set; }
        public int AwayTeamFromValue { get; set; }

    }
}
