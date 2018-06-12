using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Commands
{
    public class GameCommands:BaseViewCommands
    {
        public GameCommands() { }
        public GameCommands(ApplicationContext context):base(context, "Competition") { }

        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => throw new NotImplementedException();

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public override BaseViewModel ConstructViewModelFromData(Dictionary<string, string> data)
        {
            throw new NotImplementedException();
        }

        public override Dictionary<string, string> GatherCreateData(ApplicationContext context)
        {
            throw new NotImplementedException();
        }

        public override BaseListView GetList(ListViewModel model)
        {
            throw new NotImplementedException();
        }

        public override BaseView GetView(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        [Command]
        public BaseListView ListNextGames(ApplicationContext context, int? competitionId, string competitionType)
        {

            var competitionService = (CompetitionService)context.ServiceLibraries["Competition"];

            return new GameListView(competitionService.GetModelForNextGames((int)competitionId, ConfigCompetitionViewModel.SEASON));
        }

        //move this to games commands
        [Command]
        public BaseListView ListGames(ApplicationContext context, int? competitionId, string competitionType)
        {

            var competitionService = (CompetitionService)context.ServiceLibraries["Competition"];

            return new GameListView(competitionService.GetModelForGames((int)competitionId, competitionType));
        }

        //move this to games commands
        [Command]
        public BaseListView ListGamesByTeam(ApplicationContext context, int? competitionid, string teamName, string competitionType)
        {

            if (teamName == null)
                teamName = TeamCommands.SelectTeam(context).Name;

            var competitionService = (CompetitionService)context.ServiceLibraries["Competition"];

            return new GameListView(competitionService.GetModelForGames((int)competitionid, teamName, competitionType));



        }

    }
}
