using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Service.Test.DataFolder;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Table.Display;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class ConfigServiceTests
    {
        ConfigService configService;
        LeagueService leagueService;
        SimpleTestDataDriver driver;
        League league;
        ConfigTeam getTeam;
        [TestInitialize]
        public void Setup()
        {
            driver = new SimpleTestDataDriver();
            driver.Setup();
            leagueService = new LeagueService(driver.db);
            configService = new ConfigService(driver.db, leagueService);
            

            league = leagueService.CreateLeague("My League");
            leagueService.Save();

            getTeam = configService.CreateTeam("Test Team", 5, null, null, null, null);
            configService.Save();
        }
        #region Competition
        [TestMethod]
        public void ShouldCreateCompetitionNoReference()
        {
            //also tests getby name
            var newComp = configService.CreateCompetition(league, "First Comp", ConfigCompetition.SEASON, null, 1, 1, 15);

            configService.Save();

            var comp = configService.GetCompetitionByName(league, "First Comp");

            AreEqual(comp, newComp);
            AreEqual(1, league.ReferenceCompetitions.Count);

        }

        [TestMethod]
        public void ShouldCreateCompetitionWithReference()
        {
            //also tests get by id
            var seasonComp = configService.CreateCompetition(league, "First Comp", ConfigCompetition.SEASON, null, 1, 1, 15);
            var playoffComp = configService.CreateCompetition(league, "Second Comp", ConfigCompetition.SEASON, seasonComp, 2, 1, 15);

            configService.Save();

            var comp = configService.GetCompetitionById((int)playoffComp.Id);

            AreEqual(comp, playoffComp);
            AreEqual(comp.Reference, seasonComp);
            AreEqual(2, league.ReferenceCompetitions.Count);
        }
        #endregion
        #region Team
        [TestMethod]
        public void ShouldGetTeamByName()
        {
            var team = configService.GetTeamByName("Test Team");

            AreEqual(team, getTeam);
        }

        [TestMethod]
        public void ShouldGetTeamById()
        {
            //assumes create team is working
            var team = configService.GetTeamById((int)getTeam.Id);

            AreEqual(team, getTeam);
        }
        [TestMethod]
        public void ShouldCreateTeamNoDivisionNoLeagueNoStartNoLast()
        {
            var newTeam = configService.CreateTeam("Team 1", 3, null, null, null, null);

            configService.Save();

            var team = configService.GetTeamByName("Team 1");

            AreEqual(newTeam, team);

        }
        #endregion
        #region Divisions
        [TestMethod]
        public void ShouldCreateDivisionNoParent()
        {
            var season = configService.CreateCompetition(league, "Season", ConfigCompetition.SEASON, null, 1, 1, null);
            var newDivision = configService.CreateDivision(league, season, "Division 1", "Div 1", 1, 1, null, 1, null);

            configService.Save();

            var division = configService.GetDivisionByName(league, "Division 1");

            AreEqual(newDivision, division);
        }
        [TestMethod]
        public void ShouldCreateDivisionWithParent()
        {
            var season = configService.CreateCompetition(league, "Season", ConfigCompetition.SEASON, null, 1, 1, null);
            var parentDivision = configService.CreateDivision(league, season, "Parent", "P 1", 1, 1, null, 1, null);
            var newDivision = configService.CreateDivision(league, season, "Division 1", "Div 1", 1, 1, parentDivision, 1, null);

            configService.Save();

            var division = configService.GetDivisionById((int)newDivision.Id);

            AreEqual(newDivision, division);
            AreEqual(division.Parent, parentDivision);
        }

        #endregion
        #region Schedule Rules
        [TestMethod]
        public void ShouldCreateScheduleRule()
        {
            //also tests get by Id
            var season = configService.CreateCompetition(league, "Season", ConfigCompetition.SEASON, null, 1, 1, null);
            var division = configService.CreateDivision(league, season, "Division 1", "Div 1", 0, 0, null, 1, null);
            var team = configService.CreateTeam("Team 1", 5, division, league, 1, null);
            var newRule = configService.CreateScheduleRuleByDivisionVsTeam(league, season, "Rule 1", division, team, true, 1, 1, false, 1, null);

            configService.Save();
            var rule = configService.GetScheduleRuleById((int)newRule.Id);

            AreEqual(newRule, rule);
        }
        [TestMethod]
        public void ShouldGetScheduleRuleByName()
        {
            var season = configService.CreateCompetition(league, "Season", ConfigCompetition.SEASON, null, 1, 1, null);
            var division = configService.CreateDivision(league, season, "Division 1", "Div 1", 0, 0, null, 1, null);
            var team = configService.CreateTeam("Team 1", 5, division, league, 1, null);
            var newRule = configService.CreateScheduleRuleByDivisionVsTeam(league, season, "Rule 1", division, team, true, 1, 1, false, 1, null);

            configService.Save();
            var rule = configService.GetScheduleRuleByName(league, season, "Rule 1");

            AreEqual(newRule, rule);
        }
        [TestMethod]
        public void ShouldGetScheduleRulesByCompetition()
        {
            var season1 = configService.CreateCompetition(league, "Season1", ConfigCompetition.SEASON, null, 1, 1, null);
            var season2 = configService.CreateCompetition(league, "Season2", ConfigCompetition.SEASON, null, 2, 1, null);
            var division1 = configService.CreateDivision(league, season1, "Division 1", "Div 1", 0, 0, null, 1, null);
            var division2 = configService.CreateDivision(league, season2, "Division 2", "Div 2", 0, 0, null, 1, null);

            var rule1A = configService.CreateScheduleRuleByDivisionVsSelf(league, season1, "Test 1a", division1, false, 1, 1, false, 1, null);
            var rule2A = configService.CreateScheduleRuleByDivisionVsSelf(league, season1, "Test 2a", division1, false, 1, 1, false, 1, null);
            var rule3A = configService.CreateScheduleRuleByDivisionVsSelf(league, season1, "Test 3a", division1, false, 1, 1, false, 1, null);
            var rule4A = configService.CreateScheduleRuleByDivisionVsSelf(league, season1, "Test 4a", division1, false, 1, 1, false, 1, null);

            var rule1B = configService.CreateScheduleRuleByDivisionVsSelf(league, season2, "Test 1b", division1, false, 1, 1, false, 1, null);
            var rule2B = configService.CreateScheduleRuleByDivisionVsSelf(league, season2, "Test 2b", division1, false, 1, 1, false, 1, null);
            var rule3B = configService.CreateScheduleRuleByDivisionVsSelf(league, season2, "Test 3b", division1, false, 1, 1, false, 1, null);
            var rule4B = configService.CreateScheduleRuleByDivisionVsSelf(league, season2, "Test 4b", division1, false, 1, 1, false, 1, null);

            configService.Save();

            var season1Rules = configService.GetScheduleRulesByCompetition(season1);
            var season2Rules = configService.GetScheduleRulesByCompetition(season2);

            AreEqual(4, season1Rules.Count);
            AreEqual(4, season2Rules.Count);

            IsTrue(season1Rules.Contains(rule1A));
            IsTrue(season1Rules.Contains(rule2A));
            IsTrue(season1Rules.Contains(rule3A));
            IsTrue(season1Rules.Contains(rule4A));
            IsTrue(season2Rules.Contains(rule1B));
            IsTrue(season2Rules.Contains(rule2B));
            IsTrue(season2Rules.Contains(rule3B));
            IsTrue(season2Rules.Contains(rule4B));

            IsFalse(season2Rules.Contains(rule1A));
            IsFalse(season2Rules.Contains(rule2A));
            IsFalse(season2Rules.Contains(rule3A));
            IsFalse(season2Rules.Contains(rule4A));
            IsFalse(season1Rules.Contains(rule1B));
            IsFalse(season1Rules.Contains(rule2B));
            IsFalse(season1Rules.Contains(rule3B));
            IsFalse(season1Rules.Contains(rule4B));
        }
        #endregion
        #region sorting rules
        [TestMethod]
        public void ShouldCreateSortingRule()
        {
            //also tests get by id
            var competition = configService.CreateCompetition(league, "Comp 1", ConfigCompetition.SEASON, null, 1, 1, null);
            var division = configService.CreateDivision(league, competition, "Division 1", "Div 1", 1, 1, null, 1, null);
            var newRule = configService.CreateSortingRule("Sorting RUle 1", 1, division, division, "1,2,3", 0, 0, 1, null);

            configService.Save();

            var rule = configService.GetSortingRuleById((int)newRule.Id);

            AreEqual(rule, newRule);
        }
        #endregion
        #region Groups
        [TestMethod]
        public void ShouldCreateGroup()
        {
            var playoff = configService.CreateCompetition(league, "Playoffs", ConfigCompetition.PLAYOFF, null, 1, 1, null);
            var sortByDivision = configService.CreateDivision(league, playoff, "Division 1", "Div 1", 1, 1, null, 1, null);
            var newGroup = configService.CreateGroup("Group 1", playoff, new List<ConfigGroupRule>(), sortByDivision, 1, null);

            configService.Save();

            var group = configService.GetGroupById((int)newGroup.Id);
            AreEqual(group, newGroup);           
        }
        #endregion
        #region Group Rules
        [TestMethod]
        public void ShouldCreateGroupRule()
        {
            var playoff = configService.CreateCompetition(league, "Playoffs", ConfigCompetition.PLAYOFF, null, 1, 1, null);
            var sortByDivision = configService.CreateDivision(league, playoff, "Division 1", "Div 1", 1, 1, null, 1, null);
            var group = configService.CreateGroup("Group 1", playoff, new List<ConfigGroupRule>(), sortByDivision, 1, null);
            var team = configService.CreateTeam("Team 1", 5, sortByDivision, league, 1, null);
            var newGroupRule = configService.CreateGroupRuleFromTeam(group, "Team Group Rule", team, 1, null);

            configService.Save();

            var groupRule = configService.GetGroupRuleById((int)newGroupRule.Id);

            AreEqual(groupRule, newGroupRule);
        }
        #endregion
        #region series rules
        [TestMethod]
        public void ShouldCreateSeriesRule()
        {
            var playoff = configService.CreateCompetition(league, "Playoffs", ConfigCompetition.PLAYOFF, null, 1, 1, null);
            var sortByDivision = configService.CreateDivision(league, playoff, "Division 1", "Div 1", 1, 1, null, 1, null);
            var homeTeamFromGroup = configService.CreateGroup("Group 1", playoff, new List<ConfigGroupRule>(), sortByDivision, 1, null);
            var awayTeamFromGroup = configService.CreateGroup("Group 2", playoff, new List<ConfigGroupRule>(), sortByDivision, 1, null);
            var newSeriesRule = configService.CreateSeriesRule(playoff, "Series 1", 1, homeTeamFromGroup, 1, awayTeamFromGroup, 1,
                ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, 1, null);

            configService.Save();

            var seriesRule = configService.GetSeriesRuleById((int)newSeriesRule.Id);

            AreEqual(seriesRule, newSeriesRule);
        }
        #endregion
    }
}
