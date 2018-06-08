using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;
using JodyApp.ViewModel.Game;

namespace JodyApp.ConsoleApp.Views
{
    public class GameListView : BaseListView
    {
        public GameListView(ListViewModel model) : base(model) { }

        public override string Formatter => "{0,5} {1,20} {2,5} {3,20} {4,5} {5,15} {6,5} {7,5} {8,5} {9,10}";

        public override string[] HeaderStrings => new string[] {"Id", "Home", "Sc", "Away", "Sc", "Season", "Year", "Day", "Game", "Done"};

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (GameViewModel)model;

            return new List<object>
            {
                m.Id,
                m.HomeTeam.Name,
                m.HomeScore,
                m.AwayTeam.Name,
                m.AwayScore,
                m.Competition,
                m.Year,
                m.DayNumber,
                m.GameNumber
            };
        }
    }
}
