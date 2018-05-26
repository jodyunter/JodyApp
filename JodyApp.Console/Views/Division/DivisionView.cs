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
        public DivisionView(ConfigDivisionViewModel model):base(model)
        {            
        }
        public override string GetView()
        {
            var m = (ConfigDivisionViewModel)Model;

            var results = 
                GetView("",
                new string[] { "Id", "Name", "Short", "League", "Parent", "Level", "Order" },
                    new object[] { m.Id, m.Name, m.ShortName, m.League, m.Parent, m.Level, m.Order });

            var teamView = new TeamListView(new ListViewModel(m.Teams.ToList<BaseViewModel>()));
            var teamViewString = teamView.GetView();

            results += "\n" + "Teams:";
            results += "\n" + teamViewString;

            return results;

        }
    }
}
