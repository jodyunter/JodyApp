using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Table;

namespace JodyApp.Domain.Table.Display
{
    public class RecordTableDisplay
    {

        public static String TeamFormat = "{0,-5}{1,-15}{2,5}{3,5}{4,5}{5,5}{6,5}{7,5}{8,5}{9,5}{10,15}";

        public static String PrintDivisionStandings(String divisionName, List<Team> teams)
        {
            string result = divisionName;

            result += "\r\n" + GetRecordTableRowHeader();

            teams.ForEach(t => {
                result += "\r\n" + GetRecordTableRow(t);
            });

            return result;
        }
        public static string GetRecordTableRowHeader()
        {
            string result = String.Format(
                TeamFormat,
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
        public static string GetRecordTableRow(Team team )
        {
            string result = String.Format(
                TeamFormat,
                team.Stats.Rank,
                team.Name,
                team.Stats.Wins,
                team.Stats.Loses,
                team.Stats.Ties,
                team.Stats.Points,
                team.Stats.GamesPlayed,
                team.Stats.GoalsFor,
                team.Stats.GoalsAgainst,
                team.Stats.GoalDifference,
                team.Division == null ? "null" : team.Division.ShortName);


            return result;
        }
    }
}
