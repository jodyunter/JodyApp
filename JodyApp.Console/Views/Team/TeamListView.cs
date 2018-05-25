using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class TeamListView:BaseView
    {
        public string Header { get; set; }
        public TeamListViewModel Model { get; set; }

        public TeamListView(TeamListViewModel model)
        {
            this.Model = model;
        }
        public override string GetView()
        {
            var formatter = "{0,-5} {1,-15} {2,5} {3, 15} {4,15}";

            var result = "";
            if (!string.IsNullOrEmpty(Header)) result += Header + "\n";

            result += string.Format(formatter, "Id", "Name", "Skill", "League", "Division");

            Model.Items.ForEach(item =>
            {
                result += "\n" + string.Format(formatter, item.Id, item.Name, item.Skill, item.League, item.Division);
            });

            return result;
        }
    }
}
