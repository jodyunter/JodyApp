using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class ConfigSeriesRule
    {
        virtual public ConfigPlayoff Playoff { get; set; }
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
    }
}
