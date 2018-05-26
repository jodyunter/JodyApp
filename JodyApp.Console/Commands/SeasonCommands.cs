using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class SeasonCommands
    {
        
        public static BaseView List(List<BaseView> lastViews, int leagueId)
        {
            SeasonService seasonService = new SeasonService(JodyAppContext.Instance);
            var model = seasonService.GetAllByLeagueId(leagueId);

            SeasonListView view = new SeasonListView(model);

            return view;
        }
        
        /*public static string View(int seasonId)
        {
            SeasonService seasonService = new SeasonService(JodyAppContext.Instance);
            
        }
        */
    }
}
