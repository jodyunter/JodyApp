using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class TeamStatisticsTests
    {
        [TestMethod]
        public void ShouldGetPoints()
        {
            TeamStatitistics stats = new TeamStatitistics();

            stats.Wins = 15;
            AreEqual(30, stats.Points);
            stats.Ties = 5;
            AreEqual(35, stats.Points);
        }
        [TestMethod]
        public void ShouldGetGoalDifference()
        {
            TeamStatitistics stats = new TeamStatitistics();

            stats.GoalsFor = 55;
            stats.GoalsAgast = 255;

            AreEqual(-200, stats.GoalDifference);
        }
        [TestMethod]
        public void ShouldGetGamesPlayed()
        {
            TeamStatitistics stats = new TeamStatitistics();

            stats.Wins = 3;
            stats.Loses = 4;
            stats.Ties = 1;

            AreEqual(8, stats.GamesPlayed);
        }
    }
}
