using System.Collections.Generic;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class StandingsView : BaseListView
    {
        public override string Formatter => "{0,5}. {1,-15} {2,5} {3,5} {4,5} {5,5} {6,5} {7,5} {8,5}";

        public override string[] HeaderStrings => new string[] {"R", "Name", "W", "L", "T", "Pts", "GP", "GF", "GA", "GD"};

        public StandingsView(ListViewModel model) : base(model) { }

        public override List<object> GetDataObjectFromModel(BaseViewModel model)
        {
            var m = (StandingsRecordViewModel)model;

            return new List<object>
            {
                m.Rank, m.TeamName, m.Wins, m.Loses, m.Ties, m.Points, m.GamesPlayed, m.GoalsFor, m.GoalsAgainst, m.GoalDifference
            };
        }

        public override string GetView()
        {
            var listModel = Model;

            var result = "";
            if (!(string.IsNullOrEmpty(Header))) result += Header + "\n";

            listModel.Items.ForEach(model =>
           {
               var sm = (StandingsRecordViewModel)model;

               if (sm.Rank == 1)
               {
                   result += "\n" + sm.DivisionName;
                   result += "\n" + string.Format(Formatter, HeaderStrings);
               }

               result += "\n" + string.Format(Formatter, GetDataObjectFromModel(sm).ToArray());
           });

            return result;
        }
    }
}
