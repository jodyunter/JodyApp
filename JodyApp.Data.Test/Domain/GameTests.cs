using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Data.Test.Common;
using System.Collections.Generic;
using JodyApp.Domain.Config;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class GameTests
    {
        [TestMethod]
        public void OTShouldBeRequiredGameTied()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true
            };

            IsTrue(game.IsOTPeriodRequired(1));
            IsTrue(game.IsOTPeriodRequired(2));
            IsFalse(game.IsOTPeriodRequired(3));

        }

        [TestMethod]
        public void OTShouldBeRequiredGameWon()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = true,
                CanTie = true,
                HomeScore = 3,
                AwayScore = 1
            };

            IsFalse(game.IsOTPeriodRequired(1));
            IsFalse(game.IsOTPeriodRequired(2));
            IsFalse(game.IsOTPeriodRequired(3));

        }

        [TestMethod]
        public void ShouldGetScore()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true,
                HomeScore = 3,
                AwayScore = 1
            };

            Random random = new Random(1234676765);
            int score = game.GetScore(game.HomeTeam, game.AwayTeam, random);
            AreEqual(3, score);
            score = game.GetScore(game.HomeTeam, game.AwayTeam, random);
            AreEqual(2, score);
        }

        [TestMethod]
        public void ShouldPlayAndComplete()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true
            };

            Random random = new Random(1234676765);            
            
            game.Play(random);

            IsTrue(game.Complete);
            AreEqual(3, game.HomeScore);
            AreEqual(2, game.AwayScore);
            AreEqual("Team 1", game.GetWinner().Name);
            AreEqual("Team 2", game.GetLoser().Name);
        }

        [TestMethod]
        public void ShouldGetWinner()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = true,
                CanTie = true,
                HomeScore = 3,
                AwayScore = 0
            };

            AreEqual("Team 1", game.GetWinner().Name);
            game.AwayScore = 16;
            AreEqual("Team 2", game.GetWinner().Name);
            game.AwayScore = 3;
            IsNull(game.GetWinner());

        }

        [TestMethod]
        public void ShouldGetLoser()
        {
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = true,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 1
            };

            AreEqual("Team 1", game.GetLoser().Name);
            game.HomeScore = 16;
            AreEqual("Team 2", game.GetLoser().Name);
            game.HomeScore = 1;
            IsNull(game.GetLoser());

        }

        [TestMethod]
        public void ShouldPlayGameHomeWins()
        {
            FakeRandom random = new FakeRandom(new List<int> { 6, 5 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(6, game.HomeScore);
            AreEqual(5, game.AwayScore);
            IsTrue(game.Complete);            

        }

        [TestMethod]
        public void ShouldPlayGameAwayWins()
        {
            FakeRandom random = new FakeRandom(new List<int> { 5, 6 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(5, game.HomeScore);
            AreEqual(6, game.AwayScore);
            IsTrue(game.Complete);
        }

        [TestMethod]
        public void ShouldPlayGameCanTieNoOverTimeTie()
        {
            FakeRandom random = new FakeRandom(new List<int> { 6, 6 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 0,
                Complete = false,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(6, game.HomeScore);
            AreEqual(6, game.AwayScore);            
            IsTrue(game.Complete);
            IsNull(game.GetWinner());
            IsNull(game.GetLoser());
        }

        [TestMethod]
        public void ShouldPlayGameCanTieWithOverTimeGameTies()
        {
            FakeRandom random = new FakeRandom(new List<int> { 6, 6,12,12,15,15 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(6, game.HomeScore);
            AreEqual(6, game.AwayScore);
            IsTrue(game.Complete);
        }

        [TestMethod]
        public void ShouldPlayGameCanTieWithOverTimeGameWins()
        {
            FakeRandom random = new FakeRandom(new List<int> { 6, 6, 12, 15, 15, 13 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = true,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(6, game.HomeScore);
            AreEqual(7, game.AwayScore);
            IsTrue(game.Complete);
        }

        [TestMethod]
        public void ShouldPlayGameCannotTieWithOverTime()
        {
            FakeRandom random = new FakeRandom(new List<int> { 6, 6, 15, 15, 15, 15, 21, 21, 6,1 });
            Game game = new Game
            {
                HomeTeam = new BaseTeam { Name = "Team 1", Skill = 5 },
                AwayTeam = new BaseTeam { Name = "Team 2", Skill = 5 },
                MaxOverTimePeriods = 2,
                Complete = false,
                CanTie = false,
                HomeScore = 0,
                AwayScore = 0
            };

            game.Play(random);

            AreEqual(7, game.HomeScore);
            AreEqual(6, game.AwayScore);
            IsTrue(game.Complete);
        }
    }
}
