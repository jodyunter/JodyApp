using System;
using System.Collections.Generic;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;

namespace JodyApp.Data.Test.Domain.Playoffs
{
    [TestClass]
    public class GroupRuleValidationTests
    {
        [TestMethod]
        public void ShouldValidateGroupRuleInvalidGroupType()
        {
            var expected = "No message here. Type: Invalid Group Type. Name: Name1.";
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            GroupRule rule = new GroupRule(group, "Name1", 55, null, "", 1, 1, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);           

        }

        [TestMethod]
        public void ShouldValidateGroupRuleNoGroup()
        {
            var expected = "Group Cannot Be Null. Type: From Division Top. Name: Name1.";
            GroupRule rule = new GroupRule(null, "Name1", GroupRule.FROM_DIVISION, null, "", 1, 1, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);
        }

        [TestMethod]
        public void ShouldValidateGroupRuleNoFromDivision()
        {
            var expected = "From Division Cannot be null. Type: From Division Top. Name: Name1.";
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_DIVISION, null, "", 1, 1, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);
        }

        [TestMethod]
        public void ShouldValidateGroupRuleNoFromDivisionTeams()
        {
            var expected = "From Division does not have enough teams, expecting: 3, found: 2. Type: From Division Top. Name: Name1.";
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            Division fromDivision = new Division(null, null, "Test Me", null, 1, 1, null);
            fromDivision.Teams.Add(new Team("Temp Team", 0, fromDivision));
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_DIVISION, fromDivision, "", 1, 3, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);
        }

        [TestMethod]
        public void ShouldValidateGroupRuleFromDivision_Success()
        {            
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            Division fromDivision = new Division(null, null, "Test Me", null, 1, 1, null);
            fromDivision.Teams.Add(new Team("Temp Team", 0, fromDivision));
            fromDivision.Teams.Add(new Team("Temp Team 2", 0, fromDivision));
            fromDivision.Teams.Add(new Team("Temp Team 3", 0, fromDivision));
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_DIVISION, fromDivision, "", 1, 3, null, false);

            var errors = new List<string>();

            IsTrue(rule.ValidateConfiguration(errors));
            AreEqual(0, errors.Count);            
        }

        [TestMethod]
        public void ShouldValidateGroupRuleFromSeriesNoSeries()
        {
            var expected = "From Series Cannot be null. Type: From Series. Name: Name1.";
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());            
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_SERIES, null, "", 1, 3, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);
        }

        [TestMethod]
        public void ShouldValidateGroupRuleFromSeriesBadWinnerLoser()
        {
            var expected = "Value must be Series Winner or Series Loser. Type: From Series. Name: Name1.";
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_SERIES, null, "My Series", -10, 1, null, false);

            var errors = new List<string>();

            IsFalse(rule.ValidateConfiguration(errors));
            AreEqual(1, errors.Count);
            AreEqual(expected, errors[0]);
        }

        [TestMethod]
        public void ShouldValidateGroupRuleFromSeriesWinner_Success()
        {            
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_SERIES, null, "My Series", GroupRule.SERIES_WINNER, 3, null, false);

            var errors = new List<string>();

            IsTrue(rule.ValidateConfiguration(errors));
            AreEqual(0, errors.Count);            
        }


        [TestMethod]
        public void ShouldValidateGroupRuleFromSeriesLoser_Success()
        {            
            Playoff p = new Playoff(null, "Test", 1, false, false, 0, null);
            Group group = new Group("Group 1", p, null, new List<GroupRule>());
            GroupRule rule = new GroupRule(group, "Name1", GroupRule.FROM_SERIES, null, "MY Sereis", GroupRule.SERIES_LOSER, 3, null, false);

            var errors = new List<string>();

            IsTrue(rule.ValidateConfiguration(errors));
            AreEqual(0, errors.Count);            
        }
    }
}
