using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ConsoleApp.Views.Game
{
    public class GameCompactListView : BaseListView
    {
        public GameCompactListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{0,5}. {1,20} {2,5}:{3,5} {4,20} {5, 5}";

        public override string[] HeaderStrings => new string[] { "No.", "Home", "Sc", "Sc", "Away", "Complete" };

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (GameViewModel)model;

            return new List<object>
            {
                m.GameNumber,
                m.HomeTeam.Name,
                m.HomeScore,
                m.AwayTeam.Name,
                m.AwayScore,
                m.Complete
            };
        }
    }
}
