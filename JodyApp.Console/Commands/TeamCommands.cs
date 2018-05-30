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
        public TeamCommands() : base()
        {
            Service = new ConfigTeamService();
            InputDictionary.Add("League", LeagueCommands.SelectLeague);
            InputDictionary.Add("Division", DivisionCommands.SelectDivisionNoId);
        }

        public ReferenceObject SelectTeam(ApplicationContext context)
        {
            var league = LeagueCommands.SelectLeague(context);

            var teamListView = (TeamListView)ListByLeague(context, (int)league.Id);
            


            
        }
        public BaseListView ListByDivision(ApplicationContext context, int divisionId = -1)
        {
            var searchId = -1;
            var divisionRef = new ReferenceObject(null, null);

            if (divisionId == -1)
            {
                divisionRef = DivisionCommands.SelectDivisionNoId(context);

            }

            searchId = (int)divisionRef.Id;

            var service = new ConfigTeamService();
            var view = new TeamListView(service.GetByDivisionId(searchId))
            {
                Header = divisionRef.Name
            };
            return view;
        }

        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -55)
        {
            var searchId = -1;

            if (leagueId == -55)
            {
                searchId = (int)LeagueCommands.SelectLeague(context).Id;
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
            var basicInput = IOMethods.GatherData(context, "New Team", new List<string> { "Name", "Skill", "First Year", "Last Year" });

            ReferenceObject league = LeagueCommands.SelectLeague(context);
            ReferenceObject division = DivisionCommands.SelectDivision(context, (int)league.Id);

            basicInput.Add("LeagueId", league.Id.ToString());
            basicInput.Add("LeagueName", league.Name);
            basicInput.Add("DivisionId", division.Id.ToString());
            basicInput.Add("DivisionName", division.Name);

            return basicInput;
        }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            int? id = null;
            string name = data["Name"];
            int skill = int.Parse(data["Skill"]);
            int leagueId = int.Parse(data["LeagueId"]);
            string leagueName = data["LeagueName"];
            int divisionId = int.Parse(data["DivisionId"]);
            string divisionName = data["DivisionName"];
            int? firstYear = GetNullableIntFromString(data["First Year"]);
            int? lastYear = GetNullableIntFromString(data["Last Year"]);

            var model = new ConfigTeamViewModel(id, name, skill, leagueId, leagueName, divisionId, divisionName, firstYear, lastYear);

            return model;
        }
        
    }
}
