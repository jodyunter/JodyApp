using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.ViewModel;

namespace JodyApp.ConsoleApp.Views
{
    public class StandingsRecordView : BaseView
    {
        public StandingsRecordView(StandingsRecordViewModel model):base(model)
        {

        }        

        public override string GetView()
        {
            var m = (StandingsRecordViewModel)Model;

            return GetView("",
                new string[] { "R", "League", "Division", "Name", "W", "L", "T", "Pts", "GP", "GF", "GA", "GD" },
                new object[] { m.Rank, m.LeagueName, m.DivisionName, m.TeamName, m.Wins, m.Loses, m.Ties, m.Points, m.GamesPlayed,
                                m.GoalsFor, m.GoalsAgainst, m.GoalDifference });
                
        }
    }
}
