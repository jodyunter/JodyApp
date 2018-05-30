using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.IO;

namespace JodyApp.ConsoleApp.Commands
{
    public class LeagueCommands:BaseViewCommands
    {
        public LeagueCommands() : base() { Service = new LeagueService();  }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            int? id = data.ContainsKey("Id") ? GetIntFromString(data["Id"]) : null;
            int? year = data.ContainsKey("Year") ? GetIntFromString(data["Year"]) : null;
            
            return new LeagueViewModel(
                id,
                data.ContainsKey("Name") ? data["Name"] : null,
                year == null ? 0 : (int)year
                );
        }

        public static int? GetIntFromString(string input)
        {
            if (input != null) return int.Parse(input);
            else return null;
        }
        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {                    
             return IOMethods.GatherData(context, "New League", new List<string> { "Name" });
            
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new LeagueListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new LeagueView(model);
        }


        public static ReferenceObject SelectLeague(ApplicationContext context)
        {            
            var leagueCommands = new LeagueCommands();
            var view = leagueCommands.List(context);
            view.ListWithOptions = true;

            var input = IOMethods.ReadFromConsole(context, "League To Search By>", view.GetView());

            var searchSelection = (int)Program.CoerceArgument(typeof(int), input);

            var viewModel = (LeagueViewModel)view.GetBySelection(searchSelection);

            var selectedLeague = new ReferenceObject(viewModel.Id, viewModel.Name);

            return selectedLeague;
        }
    }
}
