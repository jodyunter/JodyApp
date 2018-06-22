using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class ScheduleDayView:BaseView
    {
        public ScheduleDayView(BaseViewModel model) : base(model) { }

        public override string GetView()
        {
            var dv = (ScheduleDayViewModel)Model;

            var result = "Day - " + dv.Day;

            var gameListView = new GameCompactListView(dv.GameViewModels);

            result += "\n" + gameListView.GetView();

            return result;

        }
    }
}
