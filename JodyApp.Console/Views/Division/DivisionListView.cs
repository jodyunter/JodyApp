using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views.Division
{
    public class DivisionListView : BaseListView
    {

        public DivisionListView(ConfigDivisionListViewModel model)
        {
            Model = model;
        }

        public override string Formatter => "{0,5} {1,20} {2,10} {3,15} {4,20} {5,5} {6,5}";
        public override string[] HeaderStrings => new string[] { "Id", "Name", "Short", "League", "Parent", "Level", "Order" };

        public override ListViewModel Model { get; set; }

        public override List<object> GetRowFromModel(BaseViewModel model)
        {
            var d = (ConfigDivisionViewModel)model;

            return new List<object> {
                d.Id, d.Name, d.ShortName, d.League, d.Parent, d.Level, d.Order };

        }
    }
}
