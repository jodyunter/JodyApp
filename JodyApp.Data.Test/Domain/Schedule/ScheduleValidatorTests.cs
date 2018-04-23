using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Schedule;
using System.Collections.Generic;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain.Schedule
{
    [TestClass]
    public class ScheduleValidatorTests
    {
        [TestMethod]
        public void ShouldProcessGame()
        {
            string home = "Team 1";
            string away = "Team 2";

            Game game = ScheduleGameTests.CreateBasicScheduleGame(TeamTests.CreateBasicTeam(home, 5), TeamTests.CreateBasicTeam(away, 5));

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();
            ScheduleValidator.ProcessGame(data,game);

            AreEqual(1, data[home].HomeGames);
            AreEqual(0, data[home].AwayGames);
            AreEqual(1, data[home].HomeGamesVsTeams[away]);
            AreEqual(0, data[home].AwayGamesVsTeams[away]);

            AreEqual(0, data[away].HomeGames);
            AreEqual(1, data[away].AwayGames);
            AreEqual(1, data[away].AwayGamesVsTeams[home]);
            AreEqual(0, data[away].HomeGamesVsTeams[home]);
        }

        [TestMethod]
        public void ShouldProcessGames()
        {
            List<Game> games1 = Scheduler.ScheduleGames(TeamTests.CreateBasicTeams(new string[] { "Team 1", "Team 2", "Team 3", "Team 4" }).ToArray(), false);
            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();
            ScheduleValidator.ProcessGames(data, games1);
            ScheduleValidator.ProcessGames(data, games1);
            
            AreEqual(6, data["Team 1"].HomeGames);
            AreEqual(0, data["Team 1"].AwayGames);            
            AreEqual(2, data["Team 1"].HomeGamesVsTeams["Team 2"]);
            AreEqual(2, data["Team 1"].HomeGamesVsTeams["Team 3"]);
            AreEqual(2, data["Team 1"].HomeGamesVsTeams["Team 4"]);
            AreEqual(0, data["Team 1"].AwayGamesVsTeams["Team 2"]);
            AreEqual(0, data["Team 1"].AwayGamesVsTeams["Team 3"]);
            AreEqual(0, data["Team 1"].AwayGamesVsTeams["Team 4"]);

            AreEqual(4, data["Team 2"].HomeGames);
            AreEqual(2, data["Team 2"].AwayGames);
            AreEqual(2, data["Team 2"].AwayGamesVsTeams["Team 1"]);
            AreEqual(2, data["Team 2"].HomeGamesVsTeams["Team 3"]);
            AreEqual(2, data["Team 2"].HomeGamesVsTeams["Team 4"]);
            AreEqual(0, data["Team 2"].HomeGamesVsTeams["Team 1"]);
            AreEqual(0, data["Team 2"].AwayGamesVsTeams["Team 3"]);
            AreEqual(0, data["Team 2"].AwayGamesVsTeams["Team 4"]);

            AreEqual(2, data["Team 3"].HomeGames);
            AreEqual(4, data["Team 3"].AwayGames);
            AreEqual(2, data["Team 3"].AwayGamesVsTeams["Team 1"]);
            AreEqual(2, data["Team 3"].AwayGamesVsTeams["Team 2"]);
            AreEqual(2, data["Team 3"].HomeGamesVsTeams["Team 4"]);
            AreEqual(0, data["Team 3"].HomeGamesVsTeams["Team 1"]);
            AreEqual(0, data["Team 3"].HomeGamesVsTeams["Team 2"]);
            AreEqual(0, data["Team 3"].AwayGamesVsTeams["Team 4"]);


            AreEqual(0, data["Team 4"].HomeGames);
            AreEqual(6, data["Team 4"].AwayGames);
            AreEqual(2, data["Team 4"].AwayGamesVsTeams["Team 1"]);
            AreEqual(2, data["Team 4"].AwayGamesVsTeams["Team 2"]);
            AreEqual(2, data["Team 4"].AwayGamesVsTeams["Team 3"]);
            AreEqual(0, data["Team 4"].HomeGamesVsTeams["Team 1"]);
            AreEqual(0, data["Team 4"].HomeGamesVsTeams["Team 2"]);
            AreEqual(0, data["Team 4"].HomeGamesVsTeams["Team 3"]);

        }
    }
}
