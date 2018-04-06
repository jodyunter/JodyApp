using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class ScheduleValidator
    {
        public static Dictionary<string, ScheduleCounts> GetData(List<ScheduleGame> games)
        {
            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            games.ForEach(game =>
            {
                ProcessGame(data, game);
            });

            return data;
        }

        public static void ProcessGame(Dictionary<string, ScheduleCounts> TeamData, ScheduleGame game)
        {
            string homeName = game.Home.Name;
            string awayName = game.Away.Name;

            if (!TeamData.ContainsKey(homeName))
            {
                TeamData.Add(homeName, new ScheduleCounts(homeName));
            }

            if (!TeamData.ContainsKey(game.Away.Name))
            {
                TeamData.Add(awayName, new ScheduleCounts(awayName));
            }

            TeamData[homeName].HomeGames++;
            TeamData[awayName].AwayGames++;
            
            if (!TeamData[homeName].HomeGamesVsTeams.ContainsKey(awayName))
            {
                TeamData[homeName].HomeGamesVsTeams.Add(awayName, 0);
            }

            if (!TeamData[awayName].HomeGamesVsTeams.ContainsKey(homeName))
            {
                TeamData[awayName].HomeGamesVsTeams.Add(homeName, 0);
            }

            TeamData[homeName].HomeGamesVsTeams[awayName]++;
            TeamData[awayName].AwayGamesVsTeams[homeName]++;
        }



    }

    public class ScheduleCounts
    {
        public string TeamName { get; set; }

        public int HomeGames { get; set; }
        public int AwayGames { get; set; }

        public Dictionary<string, int> HomeGamesVsTeams { get; set; }
        public Dictionary<string, int> AwayGamesVsTeams { get; set; }

        public ScheduleCounts(string teamName)
        {
            TeamName = teamName;
            HomeGames = 0;
            AwayGames = 0;
            HomeGamesVsTeams = new Dictionary<string, int>();
            AwayGamesVsTeams = new Dictionary<string, int>();
        }
    }
      

}
