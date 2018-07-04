using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleRuleView:BaseView
    {
        public override string[] ViewHeaders => new string[] { "Id", "Name", "Short", "League", "Season", "Parent", "Level", "Order" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (ScheduleRuleViewModel)Model;
                return null;
                    /*new object[] { m.Id, m.Name, m.ShortName, m.League,
                    m.Season != null ? m.Season.Name : "None",
                    m.Parent != null ? m.Parent.Name :"None",
                    m.Level, m.Order };*/
            }
        }

        public ScheduleRuleView(BaseViewModel model) : base(model) { }
    }
}
