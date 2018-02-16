using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Table;
using JodyApp.Domain;
using System.Collections.Generic;

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
            RecordTableTeam HomeTeam = new RecordTableTeam
            {
                Name = "Team 1",
                Skill = 5,
                Stats = new TeamStatitistics()
            };

            RecordTableTeam AwayTeam = new RecordTableTeam
            {
                Name = "Team 2",
                Skill = 5,
                Stats = new TeamStatitistics()
            };

            Game Game = new Game
            {
                HomeTeam = HomeTeam,
                AwayTeam = AwayTeam,
                HomeScore = 10,
                AwayScore = 15,
                Complete = true
            };

            RecordTable.ProcessGame(Game, HomeTeam.Stats, AwayTeam.Stats);

            AreEqual(10, HomeTeam.Stats.GoalsFor);
            AreEqual(10, AwayTeam.Stats.GoalsAgast);
            AreEqual(1, AwayTeam.Stats.Wins);
            AreEqual(1, HomeTeam.Stats.Loses);
        }

        [TestMethod]
        public void ShouldProcessGameNotStatic()
        {
            RecordTableTeam HomeTeam = new RecordTableTeam
            {
                Name = "Team 1",
                Skill = 5,
                Stats = new TeamStatitistics()
            };

            RecordTableTeam AwayTeam = new RecordTableTeam
            {
                Name = "Team 2",
                Skill = 5,
                Stats = new TeamStatitistics()
            };

            Game game = new Game
            {
                HomeTeam = HomeTeam,
                AwayTeam = AwayTeam,
                HomeScore = 10,
                AwayScore = 15,
                Complete = true
            };

            RecordTable table = new RecordTable
            {
                Standings = new Dictionary<string, RecordTableTeam>
                {
                    {"Team 1", HomeTeam },
                    {"Team 2", AwayTeam }
                }
            };

            table.ProcessGame(game);

            AreEqual(10, HomeTeam.Stats.GoalsFor);
            AreEqual(10, AwayTeam.Stats.GoalsAgast);
            AreEqual(1, AwayTeam.Stats.Wins);
            AreEqual(1, HomeTeam.Stats.Loses);
        }
    }
}
