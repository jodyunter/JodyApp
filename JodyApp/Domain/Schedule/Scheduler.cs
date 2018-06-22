using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class Scheduler
    {

        public static int ScheduleGames(List<Game> games, int lastGameNumber, Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway, int firstDay, int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                lastGameNumber = ScheduleGames(games, lastGameNumber, HomeTeams, AwayTeams, playHomeAndAway, firstDay);
            }

            return lastGameNumber;
        }


        public static bool DoesTeamPlayInDay(List<Game> games, Team a, Team b, int day)
        {
            if (games.Where(g => g.Day == day && (g.HomeTeam.Name == a.Name || g.AwayTeam.Name == a.Name || g.HomeTeam.Name == b.Name || g.AwayTeam.Name == b.Name)).FirstOrDefault() != null)
            {
                return true;
            }
            else
                return false;
        }
        public static bool DoesTeamPlayInList(List<Game> games, Team a, Team b)
        {
            bool found = false;

            for (int i = 0; i < games.Count && !found; i++)
            {
                if (games[i].HomeTeam.Equals(a) || games[i].AwayTeam.Equals(a) ||
                        games[i].HomeTeam.Equals(b) || games[i].AwayTeam.Equals(b))
                {
                    found = true;
                }
            }

            return found;
        }
        
        //need to add a starting day so we don't have to pass ALL games in ALL the time
        //what if we schedule some then add some later, will all games be in memory??
        //this way we can start a playoffs on a certian day and not have to retrieve all games.
        public static int ScheduleGames(List<Game> games, int lastGameNumber, Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway, int firstDay)
        {
            if (AwayTeams == null || AwayTeams.Length == 0)
            {
                lastGameNumber = ScheduleGames(games, lastGameNumber, HomeTeams, playHomeAndAway);
            }
            else
            {
                for (int i = 0; i < HomeTeams.Length; i++)
                {
                    for (int j = 0; j < AwayTeams.Length; j++)
                    {
                        lastGameNumber = AddGames(lastGameNumber, games, HomeTeams[i], AwayTeams[j], playHomeAndAway);
                    }
                }
            }

            

            //put games into days
            //we are only scheduling games that are -1 first
            games.Where(g => g.Day == -1).ToList().ForEach(g =>
            {
                bool scheduled = false;
                int day = firstDay;

                while (!scheduled)
                {
                    if (!DoesTeamPlayInDay(games, g.HomeTeam, g.AwayTeam, day))
                    {
                        g.Day = day;
                        scheduled = true;
                    }
                    else
                        day++;
                }

            });

            return lastGameNumber;
        }

        public static Dictionary<int, List<Game>> SortGamesIntoDays(List<Game> games)
        {
            var gameMap = new Dictionary<int, List<Game>>();

            games.ForEach(game =>
            {
                if (!(gameMap.ContainsKey(game.Day))) gameMap.Add(game.Day, new List<Game>());
                gameMap[game.Day].Add(game);
            });

            return gameMap;
        }

        public static int ScheduleGames(List<Game> games, int lastGameNumber, Team[] HomeTeams, bool playHomeAndAway)
        {
            for (int i = 0; i < HomeTeams.Length - 1; i++)
            {
                for (int j = i + 1; j < HomeTeams.Length; j++)
                {
                    lastGameNumber = AddGames(lastGameNumber, games, HomeTeams[i], HomeTeams[j], playHomeAndAway);
                }
            }

            return lastGameNumber;
        }

        //return last game number
        public static int AddGames(int lastGameNumber, List<Game> games, Team a, Team b, bool homeAndAway)
        {            
            if (!a.Name.Equals(b.Name))
            {
                Game home = SetupGame(lastGameNumber, a, b);
                games.Add(home);
                lastGameNumber = home.GameNumber;                
                if (homeAndAway)
                {
                    Game away = SetupGame(lastGameNumber, b, a);
                    games.Add(away);
                    lastGameNumber = away.GameNumber;
                }                
            }

            return lastGameNumber;
        }

        public static Game SetupGame(int lastGameNumber, Team home, Team away)
        {
            return new Game
            {
                HomeTeam = home,
                AwayTeam = away,
                HomeScore = 0,
                AwayScore = 0,
                CanTie = true,
                Complete = false,
                GameNumber = ++lastGameNumber,
                Day = -1
            };

        }

    }

    //eventually use this class to pass rules for games
    public class ScheduledGameRules
    {

    }

}
