using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class DivisionView : BaseView
    {
        public override string[] ViewHeaders => new string[] { "Id", "Name", "Short", "League", "Parent", "Level", "Order" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (ConfigDivisionViewModel)Model;
                return new object[] { m.Id, m.Name, m.ShortName, m.League, m.Parent, m.Level, m.Order };
            }
        }
        public DivisionView(BaseViewModel model):base(model)
        {            
        }
        public override string GetView()
        {
            var m = (ConfigDivisionViewModel)Model;

            var results = base.GetView();

            var teamView = new TeamListView(new ListViewModel(m.Teams.ToList<BaseViewModel>()));
            var teamViewString = teamView.GetView();

            results += "\n" + "Teams:";
            results += "\n" + teamViewString;

            return results;

        }
    }
}
