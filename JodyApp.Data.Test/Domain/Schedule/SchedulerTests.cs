using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Collections.Generic;
using JodyApp.Domain.Schedule;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain.Schedule
{
    [TestClass]
    public class SchedulerTests
    {
        [TestMethod]
        public void ShouldSetupGame()
        {
            Team HomeTeam = TeamTests.CreateBasicTeam("Team 1", 5);
            Team AwayTeam = TeamTests.CreateBasicTeam("Team 2", 5);

            ScheduleGame scheduleGame = Scheduler.SetupGame(HomeTeam, AwayTeam);

            AreEqual(HomeTeam, scheduleGame.Home);
            AreEqual(AwayTeam, scheduleGame.Away);
            AreEqual(0, scheduleGame.HomeScore);
            AreEqual(0, scheduleGame.AwayScore);
            IsFalse(scheduleGame.Complete);


        }

        [TestMethod]
        public void ShouldScheduleJustHomeTeamsDirectMethodJustHome()
        {
            string[] teams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = Scheduler.ScheduleGames(TeamTests.CreateBasicTeams(teams).ToArray(), false);

            AreEqual(15, scheduleGames.Count);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            //verify all teams played same number of games
            for (int i = 0; i < teams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(5, data[name].HomeGames + data[name].AwayGames);
                AreEqual(i, data[name].AwayGames);
                AreEqual(teams.Length - i - 1, data[name].HomeGames);
            }
        }

        [TestMethod]
        public void ShouldScheduleJustHomeTeamsDirectMethodHomeAndAway()
        {
            string[] teams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = Scheduler.ScheduleGames(TeamTests.CreateBasicTeams(teams).ToArray(), true);

            AreEqual(30, scheduleGames.Count);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            //verify all teams played same number of games
            for (int i = 0; i < teams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(10, data[name].HomeGames + data[name].AwayGames);
                AreEqual(5, data[name].AwayGames);
                AreEqual(5, data[name].HomeGames);
            }
        }

        [TestMethod]
        public void ShouldScheduleJustHomeTeamsInDirectMethodNull()
        {
            string[] teams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = Scheduler.ScheduleGames(TeamTests.CreateBasicTeams(teams).ToArray(), null, true);

            AreEqual(30, scheduleGames.Count);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            //verify all teams played same number of games
            for (int i = 0; i < teams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(10, data[name].HomeGames + data[name].AwayGames);
                AreEqual(5, data[name].AwayGames);
                AreEqual(5, data[name].HomeGames);
            }
        }

        [TestMethod]
        public void ShouldScheduleJustHomeTeamsInDirectMethod0Length()
        {
            string[] teams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = Scheduler.ScheduleGames(TeamTests.CreateBasicTeams(teams).ToArray(), new Team[] { }, true);

            AreEqual(30, scheduleGames.Count);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            //verify all teams played same number of games
            for (int i = 0; i < teams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(10, data[name].HomeGames + data[name].AwayGames);
                AreEqual(5, data[name].AwayGames);
                AreEqual(5, data[name].HomeGames);
            }
        }        

        [TestMethod]
        public void ShouldScheduleWithVisitorsJustHome()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldScheduileWithVisitorsHomeAndAway()
        {
            throw new NotImplementedException();
        }
    }
}
