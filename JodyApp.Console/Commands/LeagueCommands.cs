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
using JodyApp.ConsoleApp.App;

namespace JodyApp.ConsoleApp.Commands
{
    public class LeagueCommands:BaseViewCommands
    {
        public static string SELECT_LEAGUE = "Choose League>";

        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectLeague;

        public override Action<ApplicationContext> ClearSelectedItem => ClearSelectedLeague;

        public LeagueCommands(ApplicationContext context) : base(context) { Service = new LeagueService(context.DbContext);  }

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
             return Application.GatherData(context, "New League", new List<string> { "Name" });
            
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new LeagueListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new LeagueView(model);
        }


        public static ReferenceObject SelectLeague(ApplicationContext context, string prompts)
        {            
            if (context.SelectedLeague != null)
            {
                return context.SelectedLeague;
            }

            var leagueCommands = new LeagueCommands(context);
            var view = leagueCommands.List(context);

            context.SelectedLeague = GetSelectedObject(context, prompts, view);

            return context.SelectedLeague;
        }

        public void ClearSelectedLeague(ApplicationContext context)
        {
            context.SelectedLeague = null;
        }
    }
}
