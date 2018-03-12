using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Table;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain.Table
{
    [TestClass]
    public class RecordTableTeamTests
    {
        RecordTableTeam homeTeam;
        RecordTableTeam awayTeam;

        [TestInitialize]
        public void Setup()
        {
            homeTeam = new RecordTableTeam
            {
                Name = "HomeTeamName",
                Skill = 5,
                Stats = new TeamStatistics()
            };

            awayTeam = new RecordTableTeam
            {
                Name = "AwayTeamName",
                Skill = 5,
                Stats = new TeamStatistics()
            };
        }

        [TestMethod]
        public void ShouldBeEqual()
        {
            homeTeam.Stats.Wins = 4;
            homeTeam.Stats.Ties = 2;
            homeTeam.Stats.Loses = 4;
            homeTeam.Stats.GoalsFor = 50;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 4;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 4;
            awayTeam.Stats.GoalsFor = 50;
            awayTeam.Stats.GoalsAgainst = 50;

            AreEqual(0, homeTeam.CompareTo(awayTeam));
        }
        [TestMethod]
        public void ShouldSortByPoints()
        {
            homeTeam.Stats.Wins = 5;
            homeTeam.Stats.Ties = 2;
            homeTeam.Stats.Loses = 3;
            homeTeam.Stats.GoalsFor = 50;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 3;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 5;
            awayTeam.Stats.GoalsFor = 50;
            awayTeam.Stats.GoalsAgainst = 50;
            
            IsTrue(homeTeam.CompareTo(awayTeam) > 0);
            IsTrue(awayTeam.CompareTo(homeTeam) < 0);
        }

        [TestMethod]
        public void ShouldSortByGamesPlayed()
        {
            homeTeam.Stats.Wins = 4;
            homeTeam.Stats.Ties = 2;
            homeTeam.Stats.Loses = 3;
            homeTeam.Stats.GoalsFor = 50;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 4;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 4;
            awayTeam.Stats.GoalsFor = 50;
            awayTeam.Stats.GoalsAgainst = 50;

            IsTrue(homeTeam.CompareTo(awayTeam) > 0);
            IsTrue(awayTeam.CompareTo(homeTeam) < 0);
        }

        [TestMethod]
        public void ShouldSortByWins()
        {
            homeTeam.Stats.Wins = 3;
            homeTeam.Stats.Ties = 4;
            homeTeam.Stats.Loses = 3;
            homeTeam.Stats.GoalsFor = 50;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 4;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 4;
            awayTeam.Stats.GoalsFor = 50;
            awayTeam.Stats.GoalsAgainst = 50;

            IsTrue(homeTeam.CompareTo(awayTeam) < 0);
            IsTrue(awayTeam.CompareTo(homeTeam) > 0);
        }

        [TestMethod]
        public void ShouldSortByGoalDifference()
        {
            homeTeam.Stats.Wins = 4;
            homeTeam.Stats.Ties = 2;
            homeTeam.Stats.Loses = 4;
            homeTeam.Stats.GoalsFor = 55;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 4;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 4;
            awayTeam.Stats.GoalsFor = 50;
            awayTeam.Stats.GoalsAgainst = 55;

            IsTrue(homeTeam.CompareTo(awayTeam) > 0);
            IsTrue(awayTeam.CompareTo(homeTeam) < 0);
        }

        [TestMethod]
        public void ShouldSortByGoalsFor()
        {
            homeTeam.Stats.Wins = 4;
            homeTeam.Stats.Ties = 2;
            homeTeam.Stats.Loses = 4;
            homeTeam.Stats.GoalsFor = 55;
            homeTeam.Stats.GoalsAgainst = 50;
            awayTeam.Stats.Wins = 4;
            awayTeam.Stats.Ties = 2;
            awayTeam.Stats.Loses = 4;
            awayTeam.Stats.GoalsFor = 45;
            awayTeam.Stats.GoalsAgainst = 40;

            IsTrue(homeTeam.CompareTo(awayTeam) > 0);
            IsTrue(awayTeam.CompareTo(homeTeam) < 0);
        }
    }
}
