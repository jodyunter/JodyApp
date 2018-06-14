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
        public String WinnerTeamName { get; set; }
        public int WinnerWins { get; set; }
        public String LoserTeamName { get; set; }
        public int LoserWins { get; set; }
        public int Round { get; set; }
        public List<GameViewModel> Games { get; set; }

        public SeriesViewModel(int id, string name, string playoffName, int year, string winnerTeamName, int winnerWins, string loserTeamName, int loserWins, int round, List<GameViewModel> games)
        {
            Id = id;
            Name = name;
            PlayoffName = playoffName;
            Year = year;
            WinnerTeamName = winnerTeamName;
            LoserTeamName = loserTeamName;
            Round = round;
            Games = games;
            WinnerWins = winnerWins;
            LoserWins = loserWins;
        }
    }
}
