using System;
using System.Collections.Generic;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public class ScheduleRuleCommands : BaseViewCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectScheduleRule;

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public ScheduleRuleCommands() { }
        public ScheduleRuleCommands(ApplicationContext context) : base(context, SERVICE_CONFIGSCHEDULERULE)
        {
            InputDictionary.Add(INPUT_LEAGUE, LeagueCommands.SelectLeague);
            InputDictionary.Add(INPUT_HOMEDIVISION, DivisionCommands.SelectDivision);
            InputDictionary.Add(INPUT_AWAYDIVISION, DivisionCommands.SelectDivision);
            InputDictionary.Add(INPUT_HOMETEAM, TeamCommands.SelectTeam);
            InputDictionary.Add(INPUT_AWAYTEAM, TeamCommands.SelectTeam);
        }


        public static ReferenceObject SelectScheduleRule(ApplicationContext context, string prompt)
        {
            return SelectScheduleRuleWithLeagueId(context, -1, prompt);
        }
        public static ReferenceObject SelectScheduleRuleWithLeagueId(ApplicationContext context, int leagueId = -1, string prompt = "Choose Division>")
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            var commands = new ScheduleRuleCommands(context);

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
                searchId = (int)selectedLeague.Id;
            }

            var view = commands.ListByLeague(context, searchId);

            return GetSelectedObject(context, prompt, view);
        }

        public BaseListView ListByLeague(ApplicationContext context, int leagueId = -1)
        {
            ReferenceObject selectedLeague = null;
            int searchId = leagueId;

            if (leagueId == -1)
            {
                selectedLeague = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
                searchId = (int)selectedLeague.Id;
            }

            var listView = new ScheduleRuleListView(((ConfigScheduleRuleService)Service).GetModelsByLeagueId(searchId));

            return listView;
        }
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
            return new ScheduleRuleListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new ScheduleRuleView(model);
        }

    }
}
