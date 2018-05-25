using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views.Season
{
    public class StandingsView : BaseListView
    {
        public override string Formatter => "{0,5}. {1,-15} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5}";

        public override string[] HeaderStrings => new string[] {"R", "Name", "W", "L", "T", "Pts", "GP", "GF", "GA", "GD"};

        public StandingsView(StandingsViewModel model) : base(model) { }

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (StandingsRecordViewModel)model;

            return new List<object>
            {
                m.Rank, m.TeamName, m.Wins, m.Loses, m.Ties, m.Points, m.GamesPlayed, m.GoalsFor, m.GoalsAgainst, m.GoalDifference
            };
        }
    }
}
