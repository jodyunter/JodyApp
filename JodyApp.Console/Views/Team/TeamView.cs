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
        public ConfigTeamViewModel Model { get; set; }

        public TeamView(ConfigTeamViewModel model)
        {
            Model = model;
        }

        public override string GetView()
        {
            return GetView("",
                new string[] { "Id", "Name", "Skill", "League", "Division" },
                new object[] { Model.Id, Model.Name, Model.Skill, Model.League, Model.Division });
        }
    }
}
