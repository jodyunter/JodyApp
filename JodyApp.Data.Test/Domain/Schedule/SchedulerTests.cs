using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
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
    }
}
