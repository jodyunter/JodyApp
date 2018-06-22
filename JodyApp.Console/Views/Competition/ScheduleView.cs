using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleView:BaseView
    {
        public ScheduleView(BaseViewModel model) : base(model) { }

        public override string GetView()
        {
            var m = (ScheduleViewModel)Model;

            var result = m.Competition.Name + " Year " + m.Competition.Year;

            m.ScheduleDayViewModels.Items.ForEach(dm =>
            {
                result += "\n" + (new ScheduleDayView(dm)).GetView();
            });

            return result;
        }
    }
}
