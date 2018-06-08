using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class CompetitionView:BaseView
    {
        public CompetitionView(BaseViewModel model) : base(model) { }

        public override string[] ViewHeaders => new string[] { "Id", "Name", "League", "Type", "RefComp", "Order", "First Year", "Last Year"};
        public override object[] ViewObjects
        {
            get
            {
                var m = (ConfigCompetitionViewModel)Model;
                return new object[] { m.Id, m.Name,
                    m.League == null ? "None" : m.League.Name,
                    m.CompetitionType,
                    m.ReferenceCompetition == null ? "None" : m.ReferenceCompetition.Name,
                    m.Order,
                    m.FirstYear,
                    m.LastYear};
            }
        }

    }
}
