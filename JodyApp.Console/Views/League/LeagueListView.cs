using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class LeagueListView:BaseView
    {        
        public LeagueListViewModel Model { get; set; }

        public LeagueListView(LeagueListViewModel model)
        {
            Model = model;
        }

        public override string GetView()
        {
            var header = "{0,-5} {1,-15} {2,5}";

            var result = string.Format(header, "Id", "Name", "Year");

            Model.Items.ForEach(item =>
            {
                result += "\n" + string.Format(header, item.Id, item.Name, item.CurrentYear);
            });

            return result;
        }        


    }
}
