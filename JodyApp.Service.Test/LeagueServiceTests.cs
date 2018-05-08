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
    public class LeagueServiceTests
    {
        Database.JodyAppContext db = new Database.JodyAppContext();

        [TestMethod]
        public void ShouldTestWhenYearIsDone()
        {
            throw new NotImplementedException();
        }
    }
}
