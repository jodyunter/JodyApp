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

        public override string Formatter => "{0,5}.{1,10}{2,5}.{3,20} {4,2}:{5,-2} {6,-20}";

        public override string[] HeaderStrings => new string[] { "Yr", "Series", "R", "Team", "W", "W", "Team" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (SeriesViewModel)model;
            var seriesName = m.Name;
            if (seriesName.Length > 10) seriesName = seriesName.Substring(0, 10);
            return new List<object>()
            {
               m.Year, seriesName, m.Round, m.HomeTeamName, m.HomeWins, m.AwayWins, m.AwayTeamName
            };
        }
    }
}
