using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Data.Test.Domain.Config
{
    [TestClass]
    public class ConfigTeamTests

    {
        [TestMethod]
        public void ShouldNotBeSame()
        {
            var teamA = new ConfigTeam("Team 1", 5, null, null, 1, 15);
            var teamB = new ConfigTeam("Team 2", 5, null, null, 1, 15);

            IsFalse(teamA.AreTheSame(teamB));
        }

        [TestMethod]
        public void ShouldBeTheSame()
        {
            var teamA = new ConfigTeam("Team 1", 5, null, null, 1, 15);
            var teamB = new ConfigTeam("Team 1", 15, new ConfigDivision(), null, 2, 55);

            IsTrue(teamA.AreTheSame(teamB));
        }
    }
}
