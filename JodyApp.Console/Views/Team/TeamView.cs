using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class TeamView:BaseView
    {        

        public TeamView(ConfigTeamViewModel model):base(model)
        {         
        }

        public override string GetView()
        {
            var m = (ConfigTeamViewModel)Model;
            return GetView("",
                new string[] { "Id", "Name", "Skill", "League", "Division" },
                new object[] { m.Id, m.Name, m.Skill, m.League, m.Division });
        }
    }
}
