using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class DivisionListView : BaseListView
    {

        public DivisionListView(ListViewModel model):base(model)
        {            
        }

        public override string Formatter => "{0,5} {1,20} {2,10} {3,15} {4, 15} {5,20} {6,5} {7,5} {8,5}";
        public override string[] HeaderStrings => new string[] { "Id", "Name", "Short", "League", "Season", "Parent", "Level", "Order", "Teams" };
        

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var d = (ConfigDivisionViewModel)model;

            return new List<object> {
                d.Id, d.Name, d.ShortName,
                d.League != null ? d.League.Name: "None",
                d.Season != null ? d.Season.Name: "None",
                d.Parent != null ? d.Parent.Name: "None",
                d.Level, d.Order, d.Teams.Count };

        }
    }
}
