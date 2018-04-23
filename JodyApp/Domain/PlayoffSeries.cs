using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain
{
    public class PlayoffSeries
    {
        public Team HomeTeam { get; set; }
        public Team AwayTeam { get; set; }
        public List<Game> Games { get; set; }

        //rule that determines number of games, wins home and away and game specific rules
        
    }
}
