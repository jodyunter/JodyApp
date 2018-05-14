using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using JodyApp.Domain.Config;

namespace JodyApp.Data.Test.Domain.Schedule
{
    [TestClass]
    public class ScheduleGameTests
    {
        public static Game CreateBasicScheduleGame(Team home, Team away)
        {
            return new Game
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
