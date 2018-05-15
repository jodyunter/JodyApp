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
            configService = new ConfigService(driver.db);
            leagueService = new LeagueService(driver.db);

            league = leagueService.CreateLeague("My League");
            leagueService.Save();

            getTeam = configService.CreateTeam("Test Team", 5, null, null, null, null);
            configService.Save();
        }
        
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

        
    }
}
