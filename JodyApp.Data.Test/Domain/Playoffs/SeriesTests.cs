using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Playoffs;
using JodyApp.Domain;
using System.Collections.Generic;

namespace JodyApp.Data.Test.Domain.Playoffs
{
    [TestClass]
    public class SeriesTests
    {
        [TestMethod]
        public void ShouldCountTeamWins()
        {
            Team team1 = new Team() { Id = 1, Name = "Team 1" };
            Team team2 = new Team() { Id = 2, Name = "Team 1" };
            Team team3 = new Team() { Id = 3, Name = "Team 1" };

            var games = new List<Game>
            {
                new Game(){Complete = true, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = true, HomeScore = 3, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = true, HomeScore = 4, AwayScore = 12, HomeTeam = team2, AwayTeam = team1 },
                new Game(){Complete = false, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
            };

            Series ps = new Series()
            {
                HomeTeam = team1,
                AwayTeam = team2,
                Games = games
            };

            AreEqual(1, ps.TeamWins(team2));
            AreEqual(2, ps.TeamWins(team1));
        
        }

        [TestMethod]
        public void ShouldBeCompleteBestOf()
        {
            SeriesRule rule = new SeriesRule
            {
                SeriesType = SeriesRule.TYPE_BEST_OF,
                GamesNeeded = 4
            };

            Team team1 = new Team() { Id = 1, Name = "Team 1" };
            Team team2 = new Team() { Id = 2, Name = "Team 1" };


            var games = new List<Game>
            {
                new Game(){Complete = true, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = true, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = true, HomeScore = 1, AwayScore = 2, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 2, AwayScore = 1, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 2, AwayScore = 1, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 2, AwayScore = 1, HomeTeam = team1, AwayTeam = team2 },
                new Game(){Complete = false, HomeScore = 2, AwayScore = 1, HomeTeam = team1, AwayTeam = team2 },
            };

            Series ps = new Series()
            {
                HomeTeam = team1,
                AwayTeam = team2,
                Games = games,
                Rule = rule
            };

            IsFalse(ps.Complete);
            ps.Games[3].Complete = true;
            IsFalse(ps.Complete);
            ps.Games[4].Complete = true;
            IsFalse(ps.Complete);
            ps.Games[5].Complete = true;
            IsFalse(ps.Complete);
            ps.Games[6].Complete = true;
            IsTrue(ps.Complete);
                       
        }

        [TestMethod]
        public void ShouldSetHomeTeamForGame()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldSetHomeTeamForGameNoHomeGamesString()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldSetHomeTeamForGameNotEnoughinString()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldCreateGameForSeries()
        {
            SeriesRule rule = new SeriesRule
            {
                SeriesType = SeriesRule.TYPE_BEST_OF,
                GamesNeeded = 4
            };

            Team team1 = new Team() { Id = 1, Name = "Team 1" };
            Team team2 = new Team() { Id = 2, Name = "Team 2" };

            Series ps = new Series()
            {
                HomeTeam = team1,
                AwayTeam = team2,
                Rule = rule
            };

            var games = new List<Game>();

            int lastNumber = ps.CreateGameForSeries(10, games);

            AreEqual(4, ps.Games.Count);
            //best of 7, 0 games created yet, should yeild four
            //best of 7, series 1-0, 2-0, 3-0, 4-0 should create no games

            //best of 7, series tied 1-1, should create one extra game
            //best of 7, series 2-1, should NOT create a game
            //best of 7, series 3-1, should NOT create a game (already at 5)  
            //best of 7, series 4-1, shoudl NOT create a game
            //best of 7, series 2-2, should create a game

            //best of 7, series 3-2, should not create a game
            //best of 7, series 4-2, should NOT create a game
            //best of 7, series 3-3, should create a game

            //best of 7, complete 4-3 should NOT create a game

            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldCreateNeedGames()
        {
            //make sure we do all best of 7 scenarios
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldGetHomeGamesListNull()
        {
            Series ps = new Series() { };
            ps.Rule = new SeriesRule();
            ps.Rule.HomeGames = null;

            AreEqual(0, ps.GetHomeGamesList().Length);
        }

        [TestMethod]
        public void ShouldGetHomeGamesListEmptyString()
        {
            Series ps = new Series() { };
            ps.Rule = new SeriesRule();
            ps.Rule.HomeGames = "";

            AreEqual(0, ps.GetHomeGamesList().Length);
        }
    }
}
