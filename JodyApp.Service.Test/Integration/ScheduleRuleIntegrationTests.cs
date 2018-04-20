using System;
using System.Linq;
using JodyApp.Domain.Schedule;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JodyApp.Service.Test.Integration
{
    [TestClass]
    public class ScheduleRuleIntegrationTests
    {
        Database.JodyAppContext db = new Database.JodyAppContext();
        ScheduleTestDataDriver driver;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new ScheduleTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
        }

        [TestMethod]
        public void ShouldGetDivisionsByLevel()
        {
            var rule = db.ScheduleRules.Where(r => r.Name == "Rule 6").FirstOrDefault();

            var divisions = rule.GetDivisionsByLevel(db);

            AreEqual(2, divisions.Count);
        }
    }
}
