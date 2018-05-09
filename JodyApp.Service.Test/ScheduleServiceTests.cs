using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;
using System.Linq;
using System.Collections.Generic;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class ScheduleServiceTests
    {
        Database.JodyAppContext db;
        ScheduleService service;
        DivisionService divisionService;
        TeamService teamService;
        ScheduleTestDataDriver driver;

        [TestInitialize]
        public void Setup()
        {
            driver = new ScheduleTestDataDriver();
            db = driver.db;
            service = new ScheduleService(db);
            teamService = new TeamService(db);
            divisionService = new DivisionService(db);

            
            driver.DeleteAllData();
            driver.InsertData();
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleNoAwayTeam()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 2").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule, games, 0);

            AreEqual(6, games.Count);
        }
        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsDivision()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 1").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule, games, 0);

            AreEqual(3, games.Count);

            int count = 1;
            games.OrderBy(g => g.GameNumber).ToList().ForEach(g => { AreEqual(count, g.GameNumber); count++; });
                        
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsAwayTeam()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 3").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule, games, 0);

            AreEqual(1, games.Count);
            AreEqual(1, games[0].GameNumber);
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeAndAwayDivs()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 4").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule, games, 5);

            AreEqual(18, games.Count);

            int count = 6;
            games.OrderBy(g => g.GameNumber).ToList().ForEach(g => { AreEqual(count, g.GameNumber); count++; });
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleByDivisionLevel0()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 5").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule,games, 0);

            AreEqual(60, games.Count);
            int count = 1;
            games.OrderBy(g => g.GameNumber).ToList().ForEach(g => { AreEqual(count, g.GameNumber); count++; });

        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleByDivisionLevel1()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 6").FirstOrDefault();

            var games = new List<Game>();
            service.CreateGamesFromRule(rule, games, 0);

            AreEqual(24, games.Count);

            int count = 1;
            games.OrderBy(g => g.GameNumber).ToList().ForEach(g => { AreEqual(count, g.GameNumber); count++; });

        }

        //need special test for season schedule rule
        [TestMethod]
        public void ShouldCreateGamesFromRuleTeamReversed()
        {
            throw new NotImplementedException();
        }
    }
}
