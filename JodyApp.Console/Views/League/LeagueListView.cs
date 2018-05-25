using JodyApp.Service;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class LeagueListView:BaseListView
    {                
        public override string Formatter { get { return "{0,-5} {1,-15} {2,5}"; } }
        public override string[] HeaderStrings { get { return new string[] { "Id", "Name", "Year" }; } }        

        public LeagueListView(LeagueListViewModel model):base(model)
        {            
        }

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (LeagueViewModel)model;

            return new List<object> { m.Id, m.Name, m.CurrentYear };
        }
    }
}
