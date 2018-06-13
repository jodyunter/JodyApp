using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.ViewModel
{
    public class SeriesViewModel:BaseViewModel
    {
        public string PlayoffName { get; set; }
        public int Year { get; set; }
        public String HomeTeamName { get; set; }
        public int HomeWins { get; set; }
        public String AwayTeamName { get; set; }
        public int AwayWins { get; set; }
        public int Round { get; set; }
        public List<GameViewModel> Games { get; set; }

        public SeriesViewModel(int id, string name, string playoffName, int year, string homeTeamName, int homeWins, string awayTeamName, int awayWins, int round, List<GameViewModel> games)
        {
            Id = id;
            Name = name;
            PlayoffName = playoffName;
            Year = year;
            HomeTeamName = homeTeamName;
            AwayTeamName = awayTeamName;
            Round = round;
            Games = games;
            HomeWins = homeWins;
            AwayWins = awayWins;
        }
    }
}
