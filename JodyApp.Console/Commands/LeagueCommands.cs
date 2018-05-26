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
    public class LeagueCommands
    {

        public static BaseView View(List<BaseView> lastViews, int id)
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueViewModel model = service.GetById(id);

            LeagueView view = new LeagueView(model);

            return view;
        }
        public static BaseView Create(List<BaseView> lastViews, string name)
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueViewModel model = service.DomainToDTO(service.CreateLeague(name));
            service.Save();

            LeagueView view = new LeagueView(model);

            return view;
            
        }
        public static BaseView List(List<BaseView> lastViews)
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueListView view = new LeagueListView(service.GetAll());

            return view;
        }
    }
}
