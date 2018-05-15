using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Database;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class LeagueTests
    {
        [TestMethod]
        public void ShouldNotBeSame()
        {
            var leagueA = new League("My League");
            leagueA.CurrentYear = 12;

            var leagueB = new League("That League");
            leagueB.CurrentYear = 12;

            IsFalse(leagueA.AreTheSame(leagueB));
        }

        [TestMethod]
        public void ShouldBeTheSameDifferentYearNullId()
        {
            var leagueA = new League("My League");
            var leagueB = new League("My League");
            leagueA.Id = 12;
            leagueA.CurrentYear = 15;
            leagueB.CurrentYear = 25;

            IsTrue(leagueA.AreTheSame(leagueB));
        }
    }
}
