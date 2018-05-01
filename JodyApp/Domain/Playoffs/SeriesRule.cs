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

        public const int TYPE_TOTAL_GOALS = 0;
        public const int TYPE_BEST_OF = 1;

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

        public bool HomeTeam { get; set; }

        public int SeriesType { get; set; } //total goal series or best of
        public int GamesNeeded { get; set; } //games needed to win, or total games to play in total goals

        public bool CanTie { get; set; } //can games be tied?  
        public String HomeGames { get; set; } //sets which games are home, if there is not, then alternate 1,1,0,0,1,0,1 is standard exmaple.  1 = "Home Team" gets first home game

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
