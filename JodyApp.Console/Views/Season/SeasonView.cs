using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class SeasonView : BaseView
    {
        public SeasonView(SeasonViewModel model) : base(model) { }

        public override string GetView()
        {
            var m = (SeasonViewModel)Model;

            return GetView("",
                new string[] { "Id", "League", "Name", "Year", "Started" },
                new object[] { m.Id, m.League, m.Name, m.Year, m.Started });
        }
    }
}
