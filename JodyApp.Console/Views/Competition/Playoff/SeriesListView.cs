using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class SeriesListView:BaseListView
    {
        public SeriesListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{5,10}{0,5}.{1,20} {2,2}:{4,-2} {3,-20}";

        public override string[] HeaderStrings => new string[] { "R", "Team", "W", "Team", "W","Series" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (SeriesViewModel)model;
            var seriesName = m.Name;
            if (seriesName.Length > 10) seriesName = seriesName.Substring(0, 10);
            return new List<object>()
            {
                m.Round, m.HomeTeamName, m.HomeWins, m.AwayTeamName, m.AwayWins, seriesName
            };
        }
    }
}
