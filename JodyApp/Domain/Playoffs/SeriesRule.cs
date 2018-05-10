using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class SeriesRule : DomainObject
    {
        //Series rules determine which Playoff Groupings to get the teams from
        //and the rules for creating the games, how many are needed etc
        public const int TYPE_TOTAL_GOALS = 0;
        public const int TYPE_BEST_OF = 1;

        //these set the rank if it is setup by series
        public const int SERIES_WINNER = 1;
        public const int SERIES_LOSER = 2;

        virtual public Playoff Playoff { get; set; }              
        public string Name { get; set; }
        public int Round { get; set; }
        //teams are picked from Groupings of teams.  Sometimes the groupings are only 2 teams sometimse more
        public Group HomeTeamFromGroup { get; set; }
        public int HomeTeamFromRank { get; set; }
        public Group AwayTeamFromGroup { get; set; }
        public int AwayTeamFromRank { get; set; }        

        public int SeriesType { get; set; } //total goal series or best of
        public int GamesNeeded { get; set; } //games needed to win, or total games to play in total goals

        public bool CanTie { get; set; } //can games be tied?  
        public string HomeGames { get; set; } //sets which games are home, if there is not, then alternate 1,1,0,0,1,0,1 is standard exmaple.  1 = "Home Team" gets first home game        
        public SeriesRule() { }

        public SeriesRule(Playoff playoff, string name, int round, Group homeTeamFromGroup,
                            int homeTeamFromRank, Group awayTeamFromGroup, int awayTeamFromRank, 
                            int seriesType, int gamesNeeded, bool canTie, string homeGames)
        {            
            Playoff = playoff;
            Name = name;
            Round = round;
            HomeTeamFromGroup = homeTeamFromGroup;
            HomeTeamFromRank = homeTeamFromRank;
            AwayTeamFromGroup = awayTeamFromGroup;
            AwayTeamFromRank = awayTeamFromRank;
            SeriesType = seriesType;
            GamesNeeded = gamesNeeded;
            CanTie = canTie;
            HomeGames = homeGames;
        }

            

    }
}
