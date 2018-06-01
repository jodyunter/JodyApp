﻿using JodyApp.ConsoleApp.App;
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
        public SeasonCommands() : base() { Service = new SeasonService(); }

        public override Func<ApplicationContext, ReferenceObject> SelectMethod => throw new NotImplementedException();

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
            if (context.SelectedLeague == null)
            {
                context.SelectedLeague = LeagueCommands.SelectLeague(context);
            }

            return GetList(((SeasonService)Service).GetAllByLeagueId((int)context.SelectedLeague.Id));
        }

    }
}
