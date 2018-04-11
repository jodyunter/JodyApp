using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class ScheduleServiceTests
    {
        Database.JodyAppContext db = new Database.JodyAppContext();
        ScheduleService service;
        DivisionService divisionService;
        TeamService teamService;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            service = new ScheduleService(db);
            teamService = new TeamService(db);
            divisionService = new DivisionService(db);

            ScheduleTestDataDriver.DeleteAllData(db);
            ScheduleTestDataDriver.InsertData(db);
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsDivision()
        {
            ScheduleRule rule = new ScheduleRule
            {
                
            };

            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldCreateGamesFromRuleHomeTeamVsAwayTeam()
        {
            throw new NotImplementedException();
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
