using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class Scheduler
    {

        public static List<Game> ScheduleGames(Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway, int rounds)
        {
            var games = new List<Game>();

            for (int i = 0; i < rounds; i++)
            {
                games.AddRange(ScheduleGames(HomeTeams, AwayTeams, playHomeAndAway));
            }

            return games;
        }
        //need arrays to do this correctly, may need to sort prior to this method
        public static List<Game> ScheduleGames(Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway)
        {
            var games = new List<Game>();

            if (AwayTeams == null || AwayTeams.Length == 0)
            {
                return ScheduleGames(HomeTeams, playHomeAndAway);
            }

            for (int i = 0; i < HomeTeams.Length; i++)
            {
                for (int j = 0; j < AwayTeams.Length; j++)
                {
                    AddGames(games, HomeTeams[i], AwayTeams[j], playHomeAndAway);                    
                }
            }

            return games;
        }

        public static List<Game> ScheduleGames(Team[] HomeTeams, bool playHomeAndAway)
        {
            var games = new List<Game>();

            for (int i = 0; i < HomeTeams.Length - 1; i++)
            {
                for (int j = i + 1; j < HomeTeams.Length; j++)
                {
                    AddGames(games, HomeTeams[i], HomeTeams[j], playHomeAndAway);
                }
            }

            return games;
        }

        public static void AddGames(List<Game> games, Team a, Team b, bool homeAndAway)
        {
            if (!a.Name.Equals(b.Name))
            {
                games.Add(SetupGame(a, b));
                if (homeAndAway) games.Add(SetupGame(b, a));
            }
        }
        public static Game SetupGame(Team home, Team away)
        {
            return new Game
            {
                HomeTeam = home,
                AwayTeam = away,
                HomeScore = 0,
                AwayScore = 0,
                CanTie = true,
                Complete = false
            };

        }


        
    }
}
