using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class SeasonListView : BaseListView
    {
        public SeasonListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{0,5} {1,15} {2,20} {3,5} {4,10}";

        public override string[] HeaderStrings => new string[] { "Id", "League", "Name", "Year", "Started" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (SeasonViewModel)model;

            return new List<object> { m.Id, m.League, m.Name, m.Year, m.Started };
        }
        
    }
}
