using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Service.Test.DataFolder.ScheduleTestData;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;
using System.Linq;
using System.Collections.Generic;
using JodyApp.Database;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class LeagueServiceTests
    {
        JodyAppContext db = new JodyAppContext(JodyAppContext.CURRENT_DATABASE);

        [TestMethod]
        public void ShouldTestWhenYearIsDone()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldGetSeasonCompetitionByReference()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldGetPlayoffCompetitionByReference()
        {
            throw new NotImplementedException();
        }
    }
}
