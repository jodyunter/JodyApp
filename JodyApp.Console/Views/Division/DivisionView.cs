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
        public override string[] ViewHeaders => new string[] { "Id", "Name", "Short", "League", "Season", "Parent", "Level", "Order" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (ConfigDivisionViewModel)Model;
                return new object[] { m.Id, m.Name, m.ShortName, m.League,
                    m.Parent != null ? m.Parent.Name :"None",
                    m.Level, m.Order };
            }
        }

        public override string[] EditHeaders => new string[] { "Name", "Short", "League", "Season", "Parent", "Level", "Order" };
        public override object[] EditObjects
        {
            get
            {
                var m = (ConfigDivisionViewModel)Model;
                return new object[] { m.Name, m.ShortName, m.League,
                    m.Season != null ? m.Season.Name : "None",
                    m.Parent != null ? m.Parent.Name : "None", m.Level, m.Order};
            }
        }

        public DivisionView(BaseViewModel model) : base(model)
        {
        }

        public string GetViewWithTeams()
        {
            var result = base.GetView();

            var m = (ConfigDivisionViewModel)Model;

            result += "\n" + "Teams:";
            m.Teams.ForEach(ro =>
            {
                result += "\n" + ro.Name;
            });

            return result;
        }
        public override string GetView()
        {
            var m = (ConfigDivisionViewModel)Model;

            var results = GetViewWithTeams();

            return results;

        }
    }
}
