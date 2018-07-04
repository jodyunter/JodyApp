using System;
using System.Collections.Generic;
using JodyApp.ConsoleApp.Views;
using JodyApp.ViewModel;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public class ScheduleRuleCommands : BaseViewCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectScheduleRule;

        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public ScheduleRuleCommands() { }
        public ScheduleRuleCommands(ApplicationContext context):base(context, SERVICE_CONFIGSCHEDULERULE)  { }

        public static ReferenceObject SelectScheduleRule(ApplicationContext context, string prompt = "Select Rule>")
        {
            ReferenceObject selectedLeague = null;            

            var commands = new ScheduleRuleCommands(context);

            selectedLeague = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);

            var view = commands.List(context);

            return GetSelectedObject(context, prompt, view);
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
