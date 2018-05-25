using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views.Division
{
    public class DivisionView : BaseView
    {
        public ConfigDivisionViewModel Model { get; set; }
        public DivisionView(ConfigDivisionViewModel model)
        {
            Model = model;
        }
        public override string GetView()
        {
            var results = 
                GetView("",
                new string[] { "Id", "Name", "Short", "League", "Parent", "Level", "Order" },
                    new object[] { Model.Id, Model.Name, Model.ShortName, Model.League, Model.Parent, Model.Level, Model.Order });

            var teamView = new TeamListView(new ConfigTeamListViewModel(Model.Teams.ToList<BaseViewModel>()));
            var teamViewString = teamView.GetView();

            results += "\n" + "Teams:";
            results += "\n" + teamViewString;

            return results;

        }
    }
}
