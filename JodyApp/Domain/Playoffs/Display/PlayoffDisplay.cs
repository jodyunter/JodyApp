using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs.Display
{
    public class PlayoffDisplay
    {
        public static string PrintSeriesSummary(Series series)
        {
            string seriesSummaryFormat = "{0,-12}{1,5} : {2,-5}{3,12} - {4,15}";
            return String.Format(seriesSummaryFormat, series.HomeTeam.Name, series.GetTeamWins(series.HomeTeam), series.GetTeamWins(series.AwayTeam), series.AwayTeam.Name, series.Name);
        }
    }
}
