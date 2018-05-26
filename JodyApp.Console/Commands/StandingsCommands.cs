using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class StandingsCommands
    {
        public static string GetStandings(int seasonId, string division)
        {
            SeasonService seasonService = new SeasonService(JodyAppContext.Instance);
            StandingsService standingsService = new StandingsService(JodyAppContext.Instance);

            var model = standingsService.GetBySeasonAndDivisionName(seasonService.GetById(seasonId), division);
            var view = new StandingsView(model);

            return view.GetView();
            
            
        }
    }
}
