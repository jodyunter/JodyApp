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
        public static BaseView View(List<BaseView> lastViews, int id)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            ConfigTeamViewModel model = service.GetTeamModelById(id);

            TeamView view = new TeamView(model);

            return view;
        }

        public static BaseView List(List<BaseView> lastViews)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetAllTeams());

            return view;
        }

        public static BaseView ListByDivision(List<BaseView> lastViews, string divisionName)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);
            TeamListView view = new TeamListView(service.GetTeamsByDivisionName(divisionName));

            view.Header = divisionName;

            return view;
        }
        
        public static BaseView ChangeDivision(List<BaseView> lastViews, int teamId, string newDivisionName)
        {
            ConfigService service = new ConfigService(JodyAppContext.Instance);

            service.ChangeDivision(service.GetTeamById(teamId), newDivisionName);

            service.Save();

            var model = service.GetTeamModelById(teamId);

            TeamView view = new TeamView(model);

            return view;
        }

    }
}
