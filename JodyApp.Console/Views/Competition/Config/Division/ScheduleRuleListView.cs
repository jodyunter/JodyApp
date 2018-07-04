using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleRuleListView : BaseListView
    {
        public override string Formatter => "{0,5}{1,15}{2,15}{3,15}{4,5}{5,15}";

        public override string[] HeaderStrings => new string[] { "Id", "Name", "HomeType", "AwayType", "Order", "Competiton" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (ScheduleRuleViewModel)model;

            return new List<object>()
            {
                m.Id, m.Name, m.HomeType, m.AwayType, m.Order, m.Competition != null ? m.Competition.Name :"None"
            };
        }

        public ScheduleRuleListView(ListViewModel model) : base(model) { }
    }
}
