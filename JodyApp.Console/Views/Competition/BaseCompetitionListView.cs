using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class BaseCompetitionListView : BaseListView
    {
        public BaseCompetitionListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{0,5} {1,15} {2,20} {3,5} {4,10} {5,10}, {6,10}";

        public override string[] HeaderStrings => new string[] { "Id", "League", "Name", "Year", "Type", "Started", "Complete"};

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (CompetitionViewModel)model;

            return new List<object> { m.Id, m.League, m.Name, m.Year, m.CompetitionType, m.Started, m.Complete };
        }
        
    }
}
