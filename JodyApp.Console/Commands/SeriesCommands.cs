using System;
using System.Collections.Generic;
using static JodyApp.ConsoleApp.App.AppConstants;
using JodyApp.ConsoleApp.Views;
using JodyApp.ViewModel;
using JodyApp.ConsoleApp.App;
using JodyApp.Service;

namespace JodyApp.ConsoleApp.Commands
{
    public class SeriesCommands : BaseViewCommands
    {
        public SeriesCommands() : base() { }
        public SeriesCommands(ApplicationContext context) : base(context, SERVICE_SERIES) { }
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectSeries;

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
            return new SeriesListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new SeriesView(model);
        }

        public static ReferenceObject SelectSeries(ApplicationContext context, string prompt = "Select Series>")
        {
            int? leagueId = LeagueCommands.SelectLeague(context, "Select League>").Id;
            int? playoffId = PlayoffCommands.SelectPlayoff(context, "Select Playoff>").Id;

            var commands = new SeriesCommands(context);

            var view = (SeriesListView)commands.List(context);

            var seriesRefObject = GetSelectedObject(context, prompt, view);

            return seriesRefObject;
        }


        [Command]
        public override BaseListView List(ApplicationContext context)
        {
            int? playoffId = PlayoffCommands.SelectPlayoff(context).Id;

            var view = GetList(((SeriesService)Service).GetByPlayoffId((int)playoffId));

            return view;
        }

        [Command]
        public BaseListView ListByName(ApplicationContext context)
        {            
            var view = new ReferenceObjectListView(((SeriesService)Service).GetSeriesNames());

            var selectedObject = GetSelectedObject(context, "Series Name>", view);

            return new SeriesListView(((SeriesService)Service).GetBySeriesName(selectedObject.Name));
        }

        [Command]
        public BaseListView ListByTeam(ApplicationContext context)
        {
            var teamRef = TeamCommands.SelectTeam(context);

            return new SeriesListView(((SeriesService)Service).GetByTeam((int)teamRef.Id));
            
        }
    }
}
