using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;

namespace JodyApp.Data.Test.Domain.Schedule
{
    [TestClass]
    public class ScheduleGameTests
    {
        public static ScheduleGame CreateBasicScheduleGame(Team home, Team away)
        {
            return new ScheduleGame
            {
                HomeTeam = home,
                AwayTeam = away,
                Complete = false,
                HomeScore = 0,
                AwayScore = 0
            };
        }
    }
}
