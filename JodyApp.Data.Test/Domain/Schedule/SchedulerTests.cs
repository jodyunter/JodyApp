using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Collections.Generic;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using System.Linq;

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

            int lastGameNumber = Scheduler.ScheduleGames(scheduleGames, 25, TeamTests.CreateBasicTeams(teams).ToArray(), null, true, 1);

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
            Scheduler.ScheduleGames(scheduleGames, 5, TeamTests.CreateBasicTeams(teams).ToArray(), new Team[] { }, true, 1);

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
                false, 1);

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
                true, 1);

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
                false, 1);

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
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                TeamTests.CreateBasicTeams(homeTeams).ToArray(),
                false, 1);

            int count = 1;
            scheduleGames.OrderBy(s => s.GameNumber).ToList().ForEach(g =>
           {
               AreEqual(count, g.GameNumber);
               count++;
           });
            
        }

        [TestMethod]
        public void ShouldSortGamesIntoDays()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            var homeTeamArray = TeamTests.CreateBasicTeams(homeTeams).ToArray();

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                homeTeamArray,
                homeTeamArray,
                false, 1);

            var gamesInDays = Scheduler.SortGamesIntoDays(scheduleGames);

            AreEqual(13, gamesInDays.Count);

        }

        [TestMethod]
        public void TeamShouldPlayInDayHomeTeam()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            var homeTeamArray = TeamTests.CreateBasicTeams(homeTeams).ToArray();
            var team7 = TeamTests.CreateBasicTeam("Team 7", 5);

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                homeTeamArray,
                homeTeamArray,
                false, 1);

            IsTrue(Scheduler.DoesTeamPlayInDay(scheduleGames, homeTeamArray[5], team7, 1));            

        }

        [TestMethod]
        public void TeamsShouldNotPlayInDay()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            var homeTeamArray = TeamTests.CreateBasicTeams(homeTeams).ToArray();
            var team7 = TeamTests.CreateBasicTeam("Team 7", 5);
            var team8 = TeamTests.CreateBasicTeam("Team 8", 5);

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                homeTeamArray,
                homeTeamArray,
                false, 1);

            IsFalse(Scheduler.DoesTeamPlayInDay(scheduleGames, team8, team7, 1));
        }
        [TestMethod]
        public void TeamShouldPlayInDayAwayTeam()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            var homeTeamArray = TeamTests.CreateBasicTeams(homeTeams).ToArray();
            var team7 = TeamTests.CreateBasicTeam("Team 7", 5);

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                homeTeamArray,
                homeTeamArray,
                false, 1);

            IsTrue(Scheduler.DoesTeamPlayInDay(scheduleGames, team7, homeTeamArray[5], 1));
        }
        [TestMethod]
        public void TeamShouldPlayInDayBoth()
        {
            string[] homeTeams = { "Team 1", "Team 2", "Team 3", "Team 4", "Team 5", "Team 6" };
            var homeTeamArray = TeamTests.CreateBasicTeams(homeTeams).ToArray();            

            Dictionary<string, ScheduleCounts> data = new Dictionary<string, ScheduleCounts>();

            var scheduleGames = new List<Game>();
            Scheduler.ScheduleGames(scheduleGames, 0,
                homeTeamArray,
                homeTeamArray,
                false, 1);

            IsTrue(Scheduler.DoesTeamPlayInDay(scheduleGames, homeTeamArray[5], homeTeamArray[3], 1));
        }
    }
}
