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
    public class TeamCommands
    {
        //probably want to switch these out to config commands            
        public static string View(int id)
        {
            TeamService service = new TeamService(JodyAppContext.Instance);
            TeamViewModel model = service.GetById(id);

            TeamView view = new TeamView(model);

            return view.GetView();
        }

        public static string List()
        {
            TeamService service = new TeamService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetAll());

            return view.GetView();
        }

        public static string ListByDivision(string divisionName)
        {
            TeamService service = new TeamService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetByDivisionName(divisionName));

            view.Header = divisionName;

            return view.GetView();
        }
    }
}
