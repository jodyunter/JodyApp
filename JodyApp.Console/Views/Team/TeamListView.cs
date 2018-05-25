using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class TeamListView:BaseListView
    {       

        public override string Formatter { get { return "{0,-5} {1,-15} {2,5} {3, 15} {4,15}"; } }

        public override string[] HeaderStrings { get { return new string[] { "Id", "Name", "Skill", "League", "Division" }; } }
        

        public TeamListView(ConfigTeamListViewModel model):base(model)
        {            
        }

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var t = (ConfigTeamViewModel)model;

            return new List<object> { t.Id, t.Name, t.Skill, t.League, t.Division };
        }
    }
}
