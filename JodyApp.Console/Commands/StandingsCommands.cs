using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Domain;
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
        public StandingsCommands(ApplicationContext context) : base(context) { }
        public StandingsCommands() : base() { }
        public BaseView View(ApplicationContext context, int seasonId, int divisionLevel)
        {
            var seasonService = new SeasonService(JodyAppContext.Instance);
            var standingsService = new StandingsService(JodyAppContext.Instance);

            var model = standingsService.GetBySeasonAndDivisionLevel((Season)seasonService.GetById(seasonId), divisionLevel);
            var view = new StandingsView(model);

            return view;
                        
        }
    }
}
