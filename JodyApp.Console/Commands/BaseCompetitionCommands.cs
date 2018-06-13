using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using JodyApp.Service;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public abstract class BaseCompetitionCommands:BaseViewCommands
    {
        public abstract string CompetitionType { get; }

        public BaseCompetitionCommands() { }
        public BaseCompetitionCommands(ApplicationContext context, string serviceType) : base(context, serviceType) { }
        public BaseCompetitionCommands(ApplicationContext context) : base(context, SERVICE_COMPETITION) { }

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
            return new BaseCompetitionListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new BaseCompetitionView(model);
        }

        public override BaseListView List(ApplicationContext context)
        {
            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);

            var service = (CompetitionService)context.ServiceLibraries[AppConstants.SERVICE_COMPETITION];

            return GetList(service.GetAllByLeagueId((int)league.Id, CompetitionType));
        }

        //move this to games commands
        [Command]
        public BaseListView ListNextGames(ApplicationContext context, int? seasonId = null)
        {
            seasonId = SelectMethod(context, CompetitionType).Id;

            var commands = new GameCommands();

            return commands.ListNextGames(context, seasonId, CompetitionType);
        }

        //move this to games commands
        [Command]
        public BaseListView ListGames(ApplicationContext context, int? seasonId = null)
        {
            seasonId = SelectMethod(context, CompetitionType).Id;

            var commands = new GameCommands();

            return commands.ListGames(context, seasonId, CompetitionType);
        }

        //move this to games commands
        [Command]
        public BaseListView ListGamesByTeam(ApplicationContext context, int? seasonId = null, string teamName = null)
        {
            seasonId = SelectMethod(context, CompetitionType).Id;

            var commands = new GameCommands();

            return commands.ListGamesByTeam(context, seasonId, teamName, CompetitionType);



        }
    }
}
