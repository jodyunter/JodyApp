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
        public static BaseView GetStandings(List<BaseView> lastViews, int seasonId, int divisionLevel)
        {
            SeasonService seasonService = new SeasonService(JodyAppContext.Instance);
            StandingsService standingsService = new StandingsService(JodyAppContext.Instance);

            var model = standingsService.GetBySeasonAndDivisionLevel(seasonService.GetById(seasonId), divisionLevel);
            var view = new StandingsView(model);

            return view;
                        
        }
    }
}
