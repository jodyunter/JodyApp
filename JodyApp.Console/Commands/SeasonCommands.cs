using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Commands
{
    public class SeasonCommands:BaseCompetitionCommands
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

            var view = (BaseCompetitionListView)commands.List(context);

            context.SelectedSeason = GetSelectedObject(context, prompt, view);

            return context.SelectedSeason;


        }
        public override Action<ApplicationContext> ClearSelectedItem => throw new NotImplementedException();

        public override string CompetitionType { get { return ConfigCompetitionViewModel.SEASON; } }
    }
}
