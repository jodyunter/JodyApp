﻿using System;
using System.Linq;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Season;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JodyApp.Service.Test.Integration
{
    [TestClass]
    public class SeasonScheduleRuleIntegrationTests
    {
        Database.JodyAppContext db = new Database.JodyAppContext();
        ScheduleTestDataDriver driver;
        SeasonService seasonService;
        Season season;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new ScheduleTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            seasonService = new SeasonService(db);
            season = seasonService.CreateNewSeason("MY season", 22);
        }

        [TestMethod]
        public void ShouldGetDivisionsByLevel()
        {
            var rule = db.SeasonScheduleRules.Where(r => r.Name == "Rule 6").FirstOrDefault();

            var divisions = rule.GetDivisionsByLevel(db);

            AreEqual(2, divisions.Count);
            IsNotNull(((SeasonDivision)divisions[0]).Season);
        }
    }
}