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
        public void ShouldGetTeamWins()
        {
            Team team1 = new Team() { Id = 1, Name = "Team 1" };
            Team team2 = new Team() { Id = 2, Name = "Team 1" };
            Team team3 = new Team() { Id = 3, Name = "Team 1" };

            Series ps = new Series()
            {
                HomeTeam = team1,
                AwayTeam = team2,
                Games = null,
                HomeWins = 2,
                AwayWins = 1
            };

            AreEqual(1, ps.GetTeamWins(team2));
            AreEqual(2, ps.GetTeamWins(team1));
        
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
                Rule = rule,
                AwayWins = 3,
                HomeWins = 0
            };

            IsFalse(ps.Complete);
            ps.HomeWins = 1;
            IsFalse(ps.Complete);
            ps.HomeWins = 2;
            IsFalse(ps.Complete);
            ps.HomeWins = 3;
            IsFalse(ps.Complete);
            ps.HomeWins = 4;
            IsTrue(ps.Complete);
                       
        }

        [TestMethod]
        public void ShouldSetHomeTeamForGameNotEnoughinString()
        {
            SeriesRule rule = new SeriesRule
            {
                SeriesType = SeriesRule.TYPE_BEST_OF,
                GamesNeeded = 6,
                HomeGames = "0,0,1,1,1,1"
            };

            Team team1 = new Team() { Id = 1, Name = "Team 1" };
            Team team2 = new Team() { Id = 2, Name = "Team 2" };

            Series ps = new Series()
            {
                HomeTeam = team1,
                AwayTeam = team2,
                Rule = rule,
                Games = new List<Game>()                
            };

            var games = new List<Game>();
            int lastNumber = ps.CreateNeededGames(0, games);

            AreEqual(6, ps.Games.Count);
            AreEqual(ps.Games[0].HomeTeam.Id, team2.Id);
            AreEqual(ps.Games[1].HomeTeam.Id, team2.Id);
            AreEqual(ps.Games[2].HomeTeam.Id, team1.Id);
            AreEqual(ps.Games[3].HomeTeam.Id, team1.Id);
            AreEqual(ps.Games[4].HomeTeam.Id, team1.Id);
            AreEqual(ps.Games[5].HomeTeam.Id, team1.Id);

            ps.Games[0].Complete = true; ps.Games[0].HomeScore = 4;
            ps.Games[1].Complete = true; ps.Games[1].HomeScore = 4;
            ps.Games[2].Complete = true; ps.Games[2].HomeScore = 4;
            ps.Games[3].Complete = true; ps.Games[3].AwayScore = 4;
            ps.Games[4].Complete = true; ps.Games[4].HomeScore = 4;
            ps.Games[5].Complete = true; ps.Games[5].HomeScore = 4;

            ps.SetTeamWinsByGames();
            AreEqual(3, ps.GetTeamWins(team1));
            AreEqual(3, ps.GetTeamWins(team2));

            lastNumber = ps.CreateNeededGames(lastNumber, games);

            AreEqual(9, ps.Games.Count);
            AreEqual(ps.Games[6].HomeTeam.Id, team1.Id);
            AreEqual(ps.Games[7].HomeTeam.Id, team2.Id);
            AreEqual(ps.Games[8].HomeTeam.Id, team1.Id);


        }

        //this tests needed games too
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
                Rule = rule,
                Games = new List<Game>(),
                HomeWins = 0,
                AwayWins = 0
            };

            var games = new List<Game>();

            int lastNumber = ps.CreateNeededGames(10, games);
            
            AreEqual(4, ps.Games.Count);

            Game game1 = ps.Games[0];
            Game game2 = ps.Games[1];
            Game game3 = ps.Games[2];
            Game game4 = ps.Games[3];

            game1.Complete = true;
            game1.HomeScore = 3;

            //1-0
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game1.GetWinner().Id);
            AreEqual(1, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(0, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(4, ps.Games.Count);            

            //2-0
            game2.Complete = true;
            game2.AwayScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game2.GetWinner().Id);
            AreEqual(2, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(0, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(4, ps.Games.Count);

            //3-0
            game3.Complete = true;
            game3.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game3.GetWinner().Id);
            AreEqual(3, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(0, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(4, ps.Games.Count);

            //4-0
            game4.Complete = true;
            game4.AwayScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game4.GetWinner().Id);
            AreEqual(4, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(0, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(4, ps.Games.Count);
            IsTrue(ps.Complete);


            //1-1
            game2.Complete = true; game2.AwayScore = 0; game2.HomeScore = 3;
            game3.Complete = false; game3.HomeScore = 0;
            game4.Complete = false; game3.AwayScore = 0;
            ps.SetTeamWinsByGames();
            AreEqual(team2.Id, game2.GetWinner().Id);
            AreEqual(1, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(1, ps.GetTeamWins(ps.AwayTeam));
            lastNumber = ps.CreateNeededGames(lastNumber, games);
            AreEqual(5, ps.Games.Count);
            Game game5 = ps.Games[4];          
            
            //2-1
            game3.Complete = true;
            game3.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game3.GetWinner().Id);
            AreEqual(2, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(1, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(5, ps.Games.Count);

            //3-1
            game4.Complete = true;
            game4.AwayScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game4.GetWinner().Id);
            AreEqual(3, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(1, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(5, ps.Games.Count);            

            //4-1
            game5.Complete = true;
            game5.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game5.GetWinner().Id);
            AreEqual(4, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(1, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(5, ps.Games.Count);
            IsTrue(ps.Complete);

            //2-2
            game4.Complete = true; game4.AwayScore = 0; game4.HomeScore = 4;
            game5.Complete = false; game5.HomeScore = 0;
            ps.SetTeamWinsByGames();
            AreEqual(team2.Id, game4.GetWinner().Id);
            AreEqual(2, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(2, ps.GetTeamWins(ps.AwayTeam));
            lastNumber = ps.CreateNeededGames(lastNumber, games);
            AreEqual(6, ps.Games.Count);
            Game game6 = ps.Games[5];

            //3-2
            game5.Complete = true;
            game5.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game5.GetWinner().Id);
            AreEqual(3, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(2, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(6, ps.Games.Count);

            //will it add one at 3-2?
            games.RemoveAt(5);
            ps.Games.RemoveAt(5);
            game5.Complete = true;
            game5.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(5, ps.Games.Count);
            lastNumber  = ps.CreateNeededGames(lastNumber, games);
            AreEqual(6, ps.Games.Count);
            game6 = ps.Games[5];

            //4-2
            game6.Complete = true;
            game6.AwayScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team1.Id, game6.GetWinner().Id);
            AreEqual(4, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(2, ps.GetTeamWins(ps.AwayTeam));
            AreEqual(lastNumber, ps.CreateNeededGames(lastNumber, games));
            AreEqual(6, ps.Games.Count);
            IsTrue(ps.Complete);

            //3-3
            game6.Complete = true; game6.AwayScore = 0;
            game6.HomeScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team2.Id, game6.GetWinner().Id);
            AreEqual(3, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(3, ps.GetTeamWins(ps.AwayTeam));
            lastNumber = ps.CreateNeededGames(lastNumber, games);
            AreEqual(7, ps.Games.Count);
            IsFalse(ps.Complete);
            Game game7 = ps.Games[6];

            //3-4
            game7.Complete = true; 
            game7.AwayScore = 4;
            ps.SetTeamWinsByGames();
            AreEqual(team2.Id, game6.GetWinner().Id);
            AreEqual(3, ps.GetTeamWins(ps.HomeTeam));
            AreEqual(4, ps.GetTeamWins(ps.AwayTeam));
            lastNumber = ps.CreateNeededGames(lastNumber, games);
            AreEqual(7, ps.Games.Count);
            IsTrue(ps.Complete);            

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
