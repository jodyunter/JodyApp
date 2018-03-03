using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Service;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class SeasonServiceTests
    {
        SeasonService service = new SeasonService();

        [TestMethod]
        public void ShouldProcessTeamWin()
        {
            TeamStatitistics stats = new TeamStatitistics();

            service.ProcessTeamStats(stats, 5, 4);
            AreEqual(1, stats.Wins);
            AreEqual(0, stats.Loses);
            AreEqual(0, stats.Ties);
            AreEqual(5, stats.GoalsFor);
            AreEqual(4, stats.GoalsAgainst);

        }
        [TestMethod]
        public void ShouldProcessTeamLoss()
        {
            TeamStatitistics stats = new TeamStatitistics();

            service.ProcessTeamStats(stats, 5, 6);
            AreEqual(0, stats.Wins);
            AreEqual(1, stats.Loses);
            AreEqual(0, stats.Ties);
            AreEqual(5, stats.GoalsFor);
            AreEqual(6, stats.GoalsAgainst);
        }
        [TestMethod]
        public void ShouldProcessTeamTie()
        {
            TeamStatitistics stats = new TeamStatitistics();

            service.ProcessTeamStats(stats, 15, 15);
            AreEqual(0, stats.Wins);
            AreEqual(0, stats.Loses);
            AreEqual(1, stats.Ties);
            AreEqual(15, stats.GoalsFor);
            AreEqual(15, stats.GoalsAgainst);
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

            service.ProcessGame(Game, HomeTeam.Stats, AwayTeam.Stats);

            AreEqual(10, HomeTeam.Stats.GoalsFor);
            AreEqual(10, AwayTeam.Stats.GoalsAgainst);
            AreEqual(1, AwayTeam.Stats.Wins);
            AreEqual(1, HomeTeam.Stats.Loses);
        }
     
    }
}
