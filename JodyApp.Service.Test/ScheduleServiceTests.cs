using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;
using System.Linq;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class ScheduleServiceTests
    {
        Database.JodyAppContext db = new Database.JodyAppContext();
        ScheduleService service;
        DivisionService divisionService;
        TeamService teamService;
        ScheduleTestDataDriver driver;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            service = new ScheduleService(db);
            teamService = new TeamService(db);
            divisionService = new DivisionService(db);

            driver = new ScheduleTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleNoAwayTeam()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 2").FirstOrDefault();

            var games = service.CreateGamesFromRule(rule);

            AreEqual(2, games.Count);
        }
        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsDivision()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 1").FirstOrDefault();

            var games = service.CreateGamesFromRule(rule);

            AreEqual(2, games.Count);
                        
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsAwayTeam()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 3").FirstOrDefault();

            var games = service.CreateGamesFromRule(rule);

            AreEqual(1, games.Count);
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleNoGamesJustHomeTeam()
        {
            //should throw a configuration error
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleAwayTeamVsDivision()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldCreateGamesFromRuleNoGamesJustAwayTeam()
        {
            //should throw a rule configuration error
            throw new NotImplementedException();
        }
    }
}
