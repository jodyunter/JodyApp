using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Table;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain.Table
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void ShouldProcessTeamWin()
        {
            TeamStatitistics stats = new TeamStatitistics();

            RecordTable.ProcessTeamStats(stats, 5, 4);
            AreEqual(1, stats.Wins);
            AreEqual(0, stats.Loses);
            AreEqual(0, stats.Ties);
            AreEqual(5, stats.GoalsFor);
            AreEqual(4, stats.GoalsAgast);

        }
        [TestMethod]
        public void ShouldProcessTeamLoss()
        {
            TeamStatitistics stats = new TeamStatitistics();

            RecordTable.ProcessTeamStats(stats, 5, 6);
            AreEqual(0, stats.Wins);
            AreEqual(1, stats.Loses);
            AreEqual(0, stats.Ties);
            AreEqual(5, stats.GoalsFor);
            AreEqual(6, stats.GoalsAgast);
        }
        [TestMethod]
        public void ShouldProcessTeamTie()
        {
            TeamStatitistics stats = new TeamStatitistics();

            RecordTable.ProcessTeamStats(stats, 15, 15);
            AreEqual(0, stats.Wins);
            AreEqual(0, stats.Loses);
            AreEqual(1, stats.Ties);
            AreEqual(15, stats.GoalsFor);
            AreEqual(15, stats.GoalsAgast);
        }

        [TestMethod]
        public void ShouldProcessGameStatic()
        {
            Fail("Not ready yet");
        }

        [TestMethod]
        public void ShouldProcessGameNotStatic()
        {
            Fail("Not ready yet");
        }
    }
}
