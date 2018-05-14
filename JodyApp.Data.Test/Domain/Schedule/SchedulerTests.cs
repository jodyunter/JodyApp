using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Collections.Generic;
using JodyApp.Domain.Config;
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

            Game scheduleGame = Scheduler.SetupGame(15, HomeTeam, AwayTeam);

            AreEqual(HomeTeam, scheduleGame.HomeTeam);
            AreEqual(AwayTeam, scheduleGame.AwayTeam);
            AreEqual(0, scheduleGame.HomeScore);
            AreEqual(0, scheduleGame.AwayScore);
            AreEqual(16, scheduleGame.GameNumber);
            IsFalse(scheduleGame.Complete);


        }

        [TestMethod]
        public void ShouldScheduleJustHomeTeamsDirectMethodJustHome()
        {
            string[] teams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();

            Scheduler.ScheduleGames(scheduleGames, 0, TeamTests.CreateBasicTeams(teams).ToArray(), false);

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

            var scheduleGames = new List<Game>();

            int LastGameNumber = Scheduler.ScheduleGames(scheduleGames, 0, TeamTests.CreateBasicTeams(teams).ToArray(), true);

            AreEqual(30, LastGameNumber);
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

            var scheduleGames = new List<Game>();

            int lastGameNumber = Scheduler.ScheduleGames(scheduleGames, 25, TeamTests.CreateBasicTeams(teams).ToArray(), null, true);

            AreEqual(55, lastGameNumber);
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

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 5, TeamTests.CreateBasicTeams(teams).ToArray(), new Team[] { }, true);

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
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            string[] awayTeams = { "Team 7", "Team 8", "Team 9", "Team 10", "Team 11", "Team 12" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();
            var scheduleGames = new List<Game>();

            Scheduler.ScheduleGames(scheduleGames, 0,
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                TeamTests.CreateBasicTeams(awayTeams).ToArray(),
                false);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            AreEqual(36, scheduleGames.Count);

            for (int i = 0; i < homeTeams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(6, data[name].HomeGames + data[name].AwayGames);
                AreEqual(0, data[name].AwayGames);
                AreEqual(6, data[name].HomeGames);

                name = "Team " + (i + 7);

                AreEqual(6, data[name].HomeGames + data[name].AwayGames);
                AreEqual(6, data[name].AwayGames);
                AreEqual(0, data[name].HomeGames);

            }
        }

        [TestMethod]
        public void ShouldScheduileWithVisitorsHomeAndAway()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            string[] awayTeams = { "Team 7", "Team 8", "Team 9", "Team 10", "Team 11", "Team 12" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                TeamTests.CreateBasicTeams(awayTeams).ToArray(),
                true);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            AreEqual(72, scheduleGames.Count);

            for (int i = 0; i < homeTeams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(12, data[name].HomeGames + data[name].AwayGames);
                AreEqual(6, data[name].AwayGames);
                AreEqual(6, data[name].HomeGames);

                name = "Team " + (i + 7);

                AreEqual(12, data[name].HomeGames + data[name].AwayGames);
                AreEqual(6, data[name].AwayGames);
                AreEqual(6, data[name].HomeGames);

            }
        }

        [TestMethod]
        public void ShouldScheduileWithSameHomeAndVisitorsNoHomeAndAway()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0, 
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                false);

            ScheduleValidator.ProcessGames(data, scheduleGames);

            AreEqual(30, scheduleGames.Count);

            for (int i = 0; i < homeTeams.Length; i++)
            {
                string name = "Team " + (i + 1);

                AreEqual(10, data[name].HomeGames + data[name].AwayGames);
                AreEqual(5, data[name].AwayGames);
                AreEqual(5, data[name].HomeGames);

            }
        }

        [TestMethod]
        public void ShouldSetupGameNumbers()
        {
            throw new NotImplementedException();
        }

    }
}
