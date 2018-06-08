using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views.Division
{
    public class CompetitionListView : BaseListView
    {

        public CompetitionListView(ListViewModel model):base(model)
        {            
        }

        public override string Formatter => "{0,5} {1,20} {2,15} {3,10} {4, 15} {5,5} {6,5} {7,5}";
        public override string[] HeaderStrings => new string[] { "Id", "Name", "League", "Type", "RefComp", "Order", "First Year", "Last Year" };
        

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (ConfigCompetitionViewModel)model;            
            return new List<object> { m.Id, m.Name,
                    m.League == null ? "None" : m.League.Name,
                    m.CompetitionType,
                    m.ReferenceCompetition == null ? "None" : m.ReferenceCompetition.Name,
                    m.Order,
                    m.FirstYear,
                    m.LastYear};

        }
    }
}
