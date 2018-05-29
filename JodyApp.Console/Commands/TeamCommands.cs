using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class TeamCommands:BaseViewCommands
    {
        public TeamCommands():base() { Service = new ConfigTeamService(); }
        //probably want to switch these out to config commands            
        public static BaseView View(ApplicationContext context, int id)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var model = service.GetTeamModelById(id);

            var view = new TeamView(model);

            return view;
        }

        /*
        public BaseView Edit(ApplicationContext context, int id)
        {
            var view = View(context, id);
            view.EditMode = true;

            return view;
        }

        public BaseView UpdateAttribute(ApplicationContext context, int selection, string newData = "None")
        {
            var view = context.GetLastView();
            
            if (selection == 0 || newData.Equals("None"))
            {
                return new MessageView("Nothing chosen to edit");
            }
            else 
                view.UpdateAttribute(view.EditHeaders[selection-2], newData);

            return view;
        }

        public BaseView List(ApplicationContext context)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new TeamListView(service.GetAllTeams());

            return view;
        }
        */


        public BaseView ListByDivision(ApplicationContext context, string divisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);
            var view = new TeamListView(service.GetTeamsByDivisionName(divisionName));

            view.Header = divisionName;

            return view;
        }
        
        public BaseView ChangeDivision(ApplicationContext context, int teamId, string newDivisionName)
        {
            var service = new ConfigService(JodyAppContext.Instance);

            service.ChangeDivision(service.GetTeamById(teamId), newDivisionName);

            service.Save();

            var model = service.GetTeamModelById(teamId);

            var view = new TeamView(model);

            return view;
        }

        public BaseView ListByLeague(ApplicationContext context, int leagueId = -55)
        {
            var searchId = -1;

            if (leagueId == -55)
            {
                searchId = LeagueCommands.SelectLeague(context);                        
            }
            else
                searchId = (int)leagueId;

            return new TeamListView(((ConfigTeamService)Service).GetByLeague(searchId));
        
        }
        public override BaseView GetView(BaseViewModel model)
        {
            return new TeamView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new TeamListView(model);
        }

        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }
}
