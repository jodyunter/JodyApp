﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using static JodyApp.ConsoleApp.App.AppConstants;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Commands
{
    public class PlayoffCommands : BaseCompetitionCommands
    {
        public override Func<ApplicationContext, string, ReferenceObject> SelectMethod => SelectPlayoff;

        public override string CompetitionType { get { return ConfigCompetitionViewModel.PLAYOFF; } }

        public override Action<ApplicationContext> ClearSelectedItem => ClearSelectedPlayoff;

        public PlayoffCommands() { }
        public PlayoffCommands(ApplicationContext context):base(context, SERVICE_PLAYOFF) {}

        public static ReferenceObject SelectPlayoff(ApplicationContext context, string prompt = "Select Playoff>")
        {
            if (context.SelectedPlayoff != null)
            {
                return context.SelectedPlayoff;
            }

            var league = LeagueCommands.SelectLeague(context, LeagueCommands.SELECT_LEAGUE);
            var commands = new PlayoffCommands(context);

            var view = (BaseCompetitionListView)commands.List(context);

            context.SelectedPlayoff = GetSelectedObject(context, prompt, view);

            return context.SelectedPlayoff;


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
            return new BaseCompetitionListView(model);
        }

        public override BaseView GetView(BaseViewModel model)
        {
            return new PlayoffView(model);
        }

        public static void ClearSelectedPlayoff(ApplicationContext context)
        {
            context.SelectedPlayoff = null;
        }

    }
}
