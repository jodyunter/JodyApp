using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel.Game
{
    public class GameViewModel:BaseViewModel
    {
        public ReferenceObject HomeTeam { get; set; }
        public ReferenceObject AwayTeam { get; set; }
        public int HomeScore { get; set; }
        public int AwayScore { get; set; }
        public string Competition { get; set; }
        public int Year { get; set; }
        public int DayNumber { get; set; }
        public int GameNumber { get; set; }

        public GameViewModel(int? id, int? homeTeamId, string homeTeamName, int? awayTeamId, string awayTeamName, int homeScore, int awayScore, string competition, int year, int dayNumber, int gameNumber)
        {
            Id = id;
            HomeTeam = homeTeamId == null ? null : new ReferenceObject(homeTeamId, homeTeamName);
            AwayTeam = awayTeamId == null ? null : new ReferenceObject(awayTeamId, awayTeamName);
            HomeScore = homeScore;
            AwayScore = awayScore;
            Competition = competition;
            Year = year;
            DayNumber = dayNumber;
            GameNumber = gameNumber;
        }
    }
}
