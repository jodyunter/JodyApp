﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Config
{
    public class Scheduler
    {

        public static int ScheduleGames(List<Game> games, int lastGameNumber, Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway, int rounds)
        {
            for (int i = 0; i < rounds; i++)
            {
                lastGameNumber = ScheduleGames(games, lastGameNumber, HomeTeams, AwayTeams, playHomeAndAway);
            }

            return lastGameNumber;
        }


        //need arrays to do this correctly, may need to sort prior to this method
        public static int ScheduleGames(List<Game> games, int lastGameNumber, Team[] HomeTeams, Team[] AwayTeams, bool playHomeAndAway)
        {
            if (AwayTeams == null || AwayTeams.Length == 0)
            {
                return ScheduleGames(games, lastGameNumber, HomeTeams, playHomeAndAway);
            }

            for (int i = 0; i < HomeTeams.Length; i++)
            {
                for (int j = 0; j < AwayTeams.Length; j++)
                {
                    lastGameNumber = AddGames(lastGameNumber, games, HomeTeams[i], AwayTeams[j], playHomeAndAway);
                }
            }

            return lastGameNumber;
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
                GameNumber = ++lastGameNumber
                };


            }
    }
}
