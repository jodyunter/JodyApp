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

        public static string View(int id)
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueViewModel model = service.GetById(id);

            LeagueView view = new LeagueView(model);

            return view.GetView();
        }
        public static string Create(string name)
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueViewModel model = service.DomainToDTO(service.CreateLeague(name));
            service.Save();

            LeagueView view = new LeagueView(model);

            return view.GetView();
            
        }
        public static string List()
        {
            LeagueService service = new LeagueService(JodyAppContext.Instance);
            LeagueListView view = new LeagueListView(service.GetAll());

            return view.GetView();
        }
    }
}
