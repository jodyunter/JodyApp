using JodyApp.ConsoleApp.App;
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
    public class DivisionCommands : BaseViewCommands
    {
        public static string SELECT_DIVISION = "Select Division>";
        public static string SELECT_PARENT_DIVISION = "Select Parent>";

        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectDivision;

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public DivisionCommands() : base() { Service = new ConfigDivisionService(); }



        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -1)
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
                searchId = (int)selectedLeague.Id;
            }

            var listView = new DivisionListView(((ConfigDivisionService)Service).GetByLeagueId(searchId));

            return listView;

        }
        
        public static ReferenceObject SelectDivision(ApplicationContext context, string prompt)
        {
            return SelectDivisionWithLeagueId(context, -1, prompt);
        }
        public static ReferenceObject SelectDivisionWithLeagueId(ApplicationContext context, int leagueId = -1, string prompt = "Choose Division>")
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            var commands = new DivisionCommands();

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);                
                searchId = (int)selectedLeague.Id;
            }

            var view = commands.ListByLeague(context, searchId);

            return GetSelectedObject(context, prompt, view);
        }


        public override BaseView GetView(BaseViewModel model)
        {
            return new DivisionView(model);
        }

        public override BaseListView GetList(ListViewModel model)
        {
            return new DivisionListView(model);
        }

        public static string INPUT_NAME = "Name";
        public static string INPUT_SHORTNAME = "Short Name";
        public static string INPUT_LEVEL = "Level";
        public static string INPUT_ORDER = "Order";
        public static string INPUT_FIRSTYEAR = "First Year";
        public static string INPUT_LASTYEAR = "Last Year";
        public static string INPUT_TEAMNAME = "TEAMNAME_";
        public static string INPUT_TEAMID = "TEAMID_";
        public static string INPUT_LEAGUEID = "LEAGUEID";
        public static string INPUT_LEAGUENAME = "LEAGUENAME";
        public static string INPUT_PARENTID = "PARENTID";
        public static string INPUT_PARENTNAME = "PARENTNAME";

        //we can add teams to a division later by team if need be
        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            //league -- will return the current context
            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
            //parent division
            var parentDivision = SelectDivision(context, SELECT_PARENT_DIVISION);

            //name, shortname, level, order, first year, last year
            var basicInput = Application.GatherData(context, "New Division", new List<string> { INPUT_NAME, INPUT_SHORTNAME, INPUT_LEVEL, INPUT_ORDER, INPUT_FIRSTYEAR, INPUT_LASTYEAR});

            basicInput.Add(INPUT_LEAGUEID, league.Id.ToString());
            basicInput.Add(INPUT_LEAGUENAME, league.Name);
            basicInput.Add(INPUT_PARENTID, parentDivision == null ? null :parentDivision.Id.ToString());
            basicInput.Add(INPUT_PARENTNAME, parentDivision == null ? null : parentDivision.Name.ToString());

            //teams
            var done = false;
            int teamCount = 0;

            while (!done)
            {
                var nextTeam = TeamCommands.SelectTeam(context, "Add Team>");
                if (nextTeam == null)
                {
                    done = true;
                }
                else
                {
                    teamCount++;
                    basicInput.Add(INPUT_TEAMID + teamCount, nextTeam.Id.ToString());
                    basicInput.Add(INPUT_TEAMNAME + teamCount, nextTeam.Name);

                }
            }

            return basicInput;
        }

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }
    }
}
