using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSeriesRule:BaseConfigItem
    {
        //Series rules determine which Playoff Groupings to get the teams from
        //and the rules for creating the games, how many are needed etc
        public const int TYPE_TOTAL_GOALS = 0;
        public const int TYPE_BEST_OF = 1;

        //these set the rank if it is setup by series
        public const int SERIES_WINNER = 1;
        public const int SERIES_LOSER = 2;

        public const string SEVEN_GAME_SERIES_HOME_GAMES = "1,1,0,0,1,0,1";

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

        public override bool Equals(object obj)
        {
            var rule = obj as ConfigSeriesRule;
            return rule != null &&
                   EqualityComparer<ConfigCompetition>.Default.Equals(Playoff, rule.Playoff) &&
                   Name == rule.Name &&
                   Round == rule.Round &&
                   EqualityComparer<ConfigGroup>.Default.Equals(HomeTeamFromGroup, rule.HomeTeamFromGroup) &&
                   HomeTeamFromRank == rule.HomeTeamFromRank &&
                   EqualityComparer<ConfigGroup>.Default.Equals(AwayTeamFromGroup, rule.AwayTeamFromGroup) &&
                   AwayTeamFromRank == rule.AwayTeamFromRank &&
                   SeriesType == rule.SeriesType &&
                   GamesNeeded == rule.GamesNeeded &&
                   CanTie == rule.CanTie &&
                   HomeGames == rule.HomeGames &&
                   EqualityComparer<int?>.Default.Equals(FirstYear, rule.FirstYear) &&
                   EqualityComparer<int?>.Default.Equals(LastYear, rule.LastYear);
        }

        public override int GetHashCode()
        {
            var hashCode = -1446039340;
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigCompetition>.Default.GetHashCode(Playoff);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Round.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigGroup>.Default.GetHashCode(HomeTeamFromGroup);
            hashCode = hashCode * -1521134295 + HomeTeamFromRank.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<ConfigGroup>.Default.GetHashCode(AwayTeamFromGroup);
            hashCode = hashCode * -1521134295 + AwayTeamFromRank.GetHashCode();
            hashCode = hashCode * -1521134295 + SeriesType.GetHashCode();
            hashCode = hashCode * -1521134295 + GamesNeeded.GetHashCode();
            hashCode = hashCode * -1521134295 + CanTie.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(HomeGames);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(FirstYear);
            hashCode = hashCode * -1521134295 + EqualityComparer<int?>.Default.GetHashCode(LastYear);
            return hashCode;
        }
    }
}
