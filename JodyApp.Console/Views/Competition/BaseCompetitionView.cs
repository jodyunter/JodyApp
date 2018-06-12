using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views
{
    public class BaseCompetitionView:BaseView
    {
        public BaseCompetitionView(BaseViewModel model) : base(model) { }

        public override string[] ViewHeaders => new string[] { "Id", "League", "Name", "Year", "Started" };
        public override object[] ViewObjects
        {
            get
            {
                var m = (CompetitionViewModel)Model;
                return new object[] { m.Id, m.League, m.Name, m.Year, m.Started };
            }
        }
    }
}
