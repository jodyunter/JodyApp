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
        public void ShouldBeComplete()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldCreateNeedGames()
        {
            //make sure we do all best of 7 scenarios
            throw new NotImplementedException();
        }
    }
}
