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
        public static BaseView View(ApplicationContext context, List<BaseView> lastViews, int id)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var model = service.GetTeamModelById(id);

            TeamView view = new TeamView(model);

            return view;
        }

        public static BaseView List(ApplicationContext context, List<BaseView> lastViews)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new TeamListView(service.GetAllTeams());

            return view;
        }

        public static BaseView ListByDivision(ApplicationContext context, List<BaseView> lastViews, string divisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new TeamListView(service.GetTeamsByDivisionName(divisionName));

            view.Header = divisionName;

            return view;
        }
        
        public static BaseView ChangeDivision(ApplicationContext context, List<BaseView> lastViews, int teamId, string newDivisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);

            service.ChangeDivision(service.GetTeamById(teamId), newDivisionName);

            service.Save();

            var model = service.GetTeamModelById(teamId);

            var view = new TeamView(model);

            return view;
        }

    }
}
