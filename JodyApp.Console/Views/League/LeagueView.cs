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
        public LeagueView(LeagueViewModel model):base(model)
        {            
        }

        public override string GetView()
        {
            var m = (LeagueViewModel)Model;

            return GetView("", new string[] { "Id", "Name", "Year" }, new object[] { m.Id, m.Name, m.CurrentYear });
        }

    }
}
