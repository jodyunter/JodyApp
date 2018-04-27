using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class PlayoffSeriesRule : DomainObject
    {
        public League League { get; set; }
        public Playoff Playoff { get; set; }
        int Round { get; set; }
        /*         
         * HomeTeamFromType (Division, series, Pool of teams)
         * HomeTeamFrom  (Division 1, Semi Final A, Pool name)         
         * HomeTeamValue (Winner/Loser, Position)         
         * AwayTeamFromType
         * AwayTeamFrom
         * AwayTeamValue
         * 
         */
    }
}
