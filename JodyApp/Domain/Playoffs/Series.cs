using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Series:DomainObject
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Game> Games { get; set; }

        public Playoff Playoff { get; set; }
        public int Round { get; set; }
        //rule that determines number of games, wins home and away and game specific rules
        //we want total goal series, and best-of series
        public SeriesRule Rule { get; set; }

        public int TeamWins(Team team)
        {
            return Games.Where(g => g.GetWinner() != null && g.GetWinner().Id == team.Id).ToList().Count;
            
        }
        


    }
}
