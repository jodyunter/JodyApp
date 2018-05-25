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
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            ConfigTeamViewModel model = service.GetTeamModelById(id);

            TeamView view = new TeamView(model);

            return view.GetView();
        }

        public static string List()
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetAllTeams());

            return view.GetView();
        }

        public static string ListByDivision(string divisionName)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetTeamsByDivisionName(divisionName));

            view.Header = divisionName;

            return view.GetView();
        }
        
        public static string ChangeDivision(int teamId, string newDivisionName)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);

            service.ChangeDivision(service.GetTeamById(teamId), newDivisionName);

            service.Save();

            var model = service.GetTeamModelById(teamId);

            TeamView view = new TeamView(model);

            return view.GetView();
        }

    }
}
