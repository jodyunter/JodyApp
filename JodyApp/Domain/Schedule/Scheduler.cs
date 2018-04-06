using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Schedule
{
    public class Scheduler
    {
        //need arrays to do this correctly, may need to sort prior to this method
        public static List<ScheduleGame> ScheduleGames(Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway)
        {
            var games = new List<ScheduleGame>();

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

        public static List<ScheduleGame> ScheduleGames(Team[] HomeTeams, bool playHomeAndAway)
        {
            var games = new List<ScheduleGame>();

            for (int i = 0; i < HomeTeams.Length - 1; i++)
            {
                for (int j = i + 1; j < HomeTeams.Length; j++)
                {
                    AddGames(games, HomeTeams[i], HomeTeams[j], playHomeAndAway);
                }
            }

            return games;
        }

        public static void AddGames(List<ScheduleGame> games, Team a, Team b, bool homeAndAway)
        {
            if (!a.Equals(b))
            {
                games.Add(SetupGame(a, b));
                if (homeAndAway) games.Add(SetupGame(b, a));
            }
        }
        public static ScheduleGame SetupGame(Team home, Team away)
        {
            return new ScheduleGame
            {
                Home = home,
                Away = away,
                HomeScore = 0,
                AwayScore = 0,
                Complete = false
            };

        }
    }
}
