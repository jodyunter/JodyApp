using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class LeagueView:BaseView
    {
        public LeagueViewModel Model { get; set; }
        public LeagueView(LeagueViewModel model)
        {
            Model = model;
        }

        public override string GetView()
        {
            return GetView("", new string[] { "Id", "Name", "Year" }, new object[] { Model.Id, Model.Name, Model.CurrentYear });
        }

    }
}
