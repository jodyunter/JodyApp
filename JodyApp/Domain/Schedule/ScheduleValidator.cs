using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class ScheduleValidator
    {
        public static void ProcessGames(Dictionary<string, ScheduleCounts> data, List<Game> games)
        {           
            games.ForEach(game =>
            {
                ProcessGame(data, game);
            });            
        }

        public static void AddTeam(Dictionary<string, ScheduleCounts> TeamData, Team team, Team opponent)
        {
            string teamName = team.Name;
            string opponentName = opponent.Name;

            if (!TeamData.ContainsKey(teamName))
            {
                TeamData.Add(teamName, new ScheduleCounts(teamName));
            }


            if (!TeamData[teamName].HomeGamesVsTeams.ContainsKey(opponentName))
            {
                TeamData[teamName].HomeGamesVsTeams.Add(opponentName, 0);
            }

            if (!TeamData[teamName].AwayGamesVsTeams.ContainsKey(opponentName))
            {
                TeamData[teamName].AwayGamesVsTeams.Add(opponentName, 0);
            }
        }
        public static void ProcessGame(Dictionary<string, ScheduleCounts> TeamData, Game game)
        {
            string homeName = game.HomeTeam.Name;
            string awayName = game.AwayTeam.Name;

            //TODO: Make this more fficient maybe by setting up the dictionary ahead of time?
            AddTeam(TeamData, game.HomeTeam, game.AwayTeam);
            AddTeam(TeamData, game.AwayTeam, game.HomeTeam);

            TeamData[homeName].HomeGames++;
            TeamData[awayName].AwayGames++;
            

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
