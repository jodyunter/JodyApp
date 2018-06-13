﻿using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class SeriesView:BaseView
    {
        public SeriesView(BaseViewModel model) : base(model) { }

        public override string GetView()
        {
            var m = (SeriesViewModel)Model;

            var gameList = new GameCompactListView(new ListViewModel(m.Games.ToList<BaseViewModel>()));

            var line1Format = "Round: {0,-5} {1,-15} {2,15} Year {3,5}";
            var result = string.Format(line1Format, m.Round,m.PlayoffName, m.Name, m.Year);
            return result += "\n" + gameList.GetView();

        }
    }
}
