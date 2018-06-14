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
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public class LeagueCommands:BaseViewCommands
    {
        public static string SELECT_LEAGUE = "Choose League>";

        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectLeague;

        public override Action<ApplicationContext> ClearSelectedItem => ClearSelectedLeague;

        public LeagueCommands(ApplicationContext context) : base(context, SERVICE_LEAGUE) { }
        public LeagueCommands() : base() { }

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


        public static ReferenceObject SelectLeague(ApplicationContext context, string prompts = "Select League>")
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
        [Command]
        public BaseView Play(ApplicationContext context, int? leagueId = null)
        {            
            LeagueService leagueService = new LeagueService(context.DbContext);
            CompetitionService competitionService = new CompetitionService(context.DbContext);
            SeasonService seasonService = new SeasonService(context.DbContext);
            DivisionService divisionService = new DivisionService(context.DbContext);            
            PlayoffService playoffService = new PlayoffService(context.DbContext);
            StandingsService standingsService = new StandingsService(context.DbContext);

            if (context.SelectedLeague == null)
            {
                context.SelectedLeague = SelectLeague(context, "Choose which league to play>");
            }
            var League = leagueService.GetByName(context.SelectedLeague.Name);
            
            Random random = new Random();
            if (leagueService.IsYearDone(League)) League.CurrentYear++;

            while (!leagueService.IsYearDone(League))
            {

                var nextCompetition = competitionService.GetNextCompetition(League);

                if (!nextCompetition.Started) nextCompetition.StartCompetition();
                competitionService.PlayGames(competitionService.GetNextGames(nextCompetition), nextCompetition, random);

                leagueService.Save();

            }

            return new MessageView("Done!");
        }

        [Command]
        public BaseView ViewChampions(ApplicationContext context)
        {
            var league = SelectLeague(context);
            return null;

        }
        
    }
}
