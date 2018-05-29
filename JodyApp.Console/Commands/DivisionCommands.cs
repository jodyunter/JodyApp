using JodyApp.ConsoleApp.IO;
using JodyApp.ConsoleApp.Views;
using JodyApp.ConsoleApp.Views.Division;
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
    public class DivisionCommands:BaseViewCommands
    {
        public DivisionCommands() : base() { Service = new ConfigDivisionService(); }

       

        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -1)
        {            
            var selectedLeagueId = leagueId;

            if (selectedLeagueId == -1)
            {
                selectedLeagueId = LeagueCommands.SelectLeague(context);
            }

            var listView = new DivisionListView(((ConfigDivisionService)Service).GetByLeagueId(selectedLeagueId));

            return listView;
            
        }

        public static int SelectDivision(ApplicationContext context, int leagueId = -1)
        {
            var searchId = -1;
            var selectedLeagueId = leagueId;

            var commands = new DivisionCommands();
            var view = commands.ListByLeague(context);
            view.ListWithOptions = true;

            var input = IOMethods.ReadFromConsole(context, "Division To Search By>", view.GetView());

            var searchSelection = (int)Program.CoerceArgument(typeof(int), input);

            var viewModel = view.GetBySelection(searchSelection);

            if (viewModel != null) searchId = (int)((LeagueViewModel)viewModel).Id;

            return searchId;
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new DivisionView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new DivisionListView(model);
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
