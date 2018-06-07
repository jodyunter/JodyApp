using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Database;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class SeasonCommands:BaseViewCommands
    {
        public SeasonCommands(ApplicationContext context) : base(context, "Season") { }
        public SeasonCommands() : base() { }
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectSeason;

        public static ReferenceObject SelectSeason(ApplicationContext context, string prompt = "Select Season>")
        {
            if (context.SelectedSeason != null)
            {
                return context.SelectedSeason;
            }

            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
            var commands = new SeasonCommands(context);

            var view = (SeasonListView)commands.List(context);

            context.SelectedSeason = GetSelectedObject(context, prompt, view);

            return context.SelectedSeason;


        }
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
            return new SeasonListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new SeasonView(model);
        }

        public override BaseListView List(ApplicationContext context)
        {
            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
                

            return GetList(((SeasonService)Service).GetAllByLeagueId((int)league.Id));
        }

    }
}
