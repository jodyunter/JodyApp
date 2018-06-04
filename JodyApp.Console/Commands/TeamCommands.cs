using JodyApp.ConsoleApp.App;
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
        public static string SELECT_TEAM = "Select Team>";
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectTeam;

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public TeamCommands() : base()
        {
            Service = new ConfigTeamService();
            InputDictionary.Add("League", LeagueCommands.SelectLeague);
            InputDictionary.Add("Division", DivisionCommands.SelectDivision);
        }

        public static ReferenceObject SelectTeam(ApplicationContext context, string prompt = "Select Team>")
        {
            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
            var commands = new TeamCommands();

            var view = (TeamListView)commands.ListByLeague(context, (int)league.Id);

            return GetSelectedObject(context, prompt, view);            
            
        }
        [Command]
        public BaseListView ListByDivision(ApplicationContext context, int divisionId = -1, string prompt = "Select Division>")
        {
            var searchId = -1;
            var divisionRef = new ReferenceObject(null, null);

            if (divisionId == -1)
            {
                divisionRef = DivisionCommands.SelectDivision(context, prompt);

            }

            searchId = (int)divisionRef.Id;

            var service = new ConfigTeamService();
            var view = new TeamListView(service.GetModelByDivision(searchId))
            {
                Header = divisionRef.Name
            };
            return view;
        }

        [Command]
        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -55)
        {
            var searchId = -1;

            if (leagueId == -55)
            {
                searchId = (int)LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE).Id;
            }
            else
                searchId = (int)leagueId;

            return new TeamListView(((ConfigTeamService)Service).GetModelByLeague(searchId));
        
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
            var basicInput = Application.GatherData(context, "New Team", new List<string> { "Name", "Skill", "First Year", "Last Year" });

            ReferenceObject league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
            ReferenceObject division = DivisionCommands.SelectDivisionWithLeagueId(context, (int)league.Id);

            basicInput.Add("LeagueId", league.Id.ToString());
            basicInput.Add("LeagueName", league.Name);
            basicInput.Add("DivisionId", division == null ? null : division.Id.ToString());
            basicInput.Add("DivisionName", division == null ? null : division.Name);

            return basicInput;
        }
        
        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            int? id = null;
            string name = data["Name"];
            int skill = int.Parse(data["Skill"]);
            int leagueId = int.Parse(data["LeagueId"]);
            string leagueName = data["LeagueName"];
            var divisionIdString = data["DivisionId"];
            int? divisionId = divisionIdString == null ? (int?)null : int.Parse(divisionIdString);
            string divisionName = data["DivisionName"];
            int? firstYear = GetNullableIntFromString(data["First Year"]);
            int? lastYear = GetNullableIntFromString(data["Last Year"]);

            var model = new ConfigTeamViewModel(id, name, skill, leagueId, leagueName, divisionId, divisionName, firstYear, lastYear);

            return model;
        }
        
    }
}
