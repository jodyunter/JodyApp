using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSeriesRule:DomainObject, BaseConfigItem
    {
        virtual public ConfigCompetition Playoff { get; set; }
        public string Name { get; set; }
        public int Round { get; set; }
        //teams are picked from Groupings of teams.  Sometimes the groupings are only 2 teams sometimse more
        public ConfigGroup HomeTeamFromGroup { get; set; }
        public int HomeTeamFromRank { get; set; }
        public ConfigGroup AwayTeamFromGroup { get; set; }
        public int AwayTeamFromRank { get; set; }

        public int SeriesType { get; set; } //total goal series or best of
        public int GamesNeeded { get; set; } //games needed to win, or total games to play in total goals

        public bool CanTie { get; set; } //can games be tied?  
        public string HomeGames { get; set; } //sets which games are home, if there is not, then alternate 1,1,0,0,1,0,1 is standard exmaple.  1 = "Home Team" gets first home game        

        public int? FirstYear { get; set; }
        public int? LastYear { get; set; }

        public ConfigSeriesRule() { }

        public ConfigSeriesRule(ConfigCompetition playoff, string name, int round, ConfigGroup homeTeamFromGroup, int homeTeamFromRank, ConfigGroup awayTeamFromGroup, int awayTeamFromRank, int seriesType, int gamesNeeded, bool canTie, string homeGames, int? firstYear, int? lastYear)
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
            FirstYear = firstYear;
            LastYear = lastYear;
        }
    }
}
