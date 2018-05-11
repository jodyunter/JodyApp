using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Console.Views
{
    public class StandingsView
    {
        public StandingsViewModel viewModel { get; set; }

        public StandingsView() { }

        private const string FORMAT_STANDINGS_HEADER = "{0} standings, {1}, Year: {2}";
        private const string FORMAT_STANDINGS_RECORD = "{0,-5}{1,-15}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,5}{9,5}";

        public string GetStandingsRecordHeader()
        {
            string result = String.Format(
                FORMAT_STANDINGS_RECORD,
                "R",
                "Name",
                "W",
                "L",
                "T",
                "Pts",
                "GP",
                "GF",
                "GA",
                "GD",
                "Div"
                );

            return result;
        }

        public string GetStandingsRecordRow(StandingsRecordViewModel team)
        {
            string result = String.Format(
                FORMAT_STANDINGS_RECORD,
                team.Rank,
                team.Name,
                team.Wins,
                team.Loses,
                team.Ties,
                team.Points,
                team.Games,
                team.GoalsFor,
                team.GoalsAgainst,
                team.GoalDifference);


            return result;
        }

        public string GetDisplayString()
        {
            string result = String.Format(FORMAT_STANDINGS_HEADER, viewModel.StandingsName, viewModel.SeasonName, viewModel.Year);


            viewModel.Records.ToList().ForEach(
                pair =>
                {
                    string divisionName = pair.Key;
                    result += "\n" + divisionName;
                    result += "\n" + GetStandingsRecordHeader();
                    var teamList = pair.Value;

                    teamList.ForEach(rec =>
                    {
                        result += "\n" + GetStandingsRecordRow(rec);
                    });
                }
             );
            return result;
        }            

         
        
    }
}
