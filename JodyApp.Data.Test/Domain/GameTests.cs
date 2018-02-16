using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
                HomeTeam = new Team { Name = "Team 1", Skill = 5 },
                AwayTeam = new Team { Name = "Team 2", Skill = 5 },
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
                HomeTeam = new Team { Name = "Team 1", Skill = 5 },
                AwayTeam = new Team { Name = "Team 2", Skill = 5 },
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
    }
}
