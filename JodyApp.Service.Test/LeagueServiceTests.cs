using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;
using System.Linq;
using System.Collections.Generic;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class LeagueServiceTests
    {
        JodyAppContext db;
        LeagueTestDataDriver driver;
        LeagueService service;
        CompetitionService competitionService;

        [TestInitialize]
        public void Setup()
        {
            db = new JodyAppContext(JodyAppContext.CURRENT_DATABASE);
            driver = new LeagueTestDataDriver();
            driver.DeleteAllData();
            driver.InsertData();
            service = new LeagueService(db);
            competitionService = new CompetitionService(db);

        }

        [TestMethod]
        public void ShouldTestWhenYearIsDone()
        {
            var random = new Random(12);

            var league = service.GetByName(LeagueTestDataDriver.LeagueName);

            IsTrue(service.IsYearDone(league));
            AreEqual(0, league.CurrentYear);

            league.CurrentYear++;

            while (!(service.IsYearDone(league)))
            {
                var competition = service.GetNextCompetition(league);
                if (!competition.Started) competition.StartCompetition();

                var games = competitionService.GetNextGames(competition);
                competitionService.PlayGames(games, competition, random);

                competition.IsComplete();

                service.Save();
            }

            IsTrue(service.IsYearDone(league));
            AreEqual(1, league.CurrentYear);

            league.CurrentYear++;

            while (!(service.IsYearDone(league)))
            {
                var competition = service.GetNextCompetition(league);
                if (!competition.Started) competition.StartCompetition();

                var games = competitionService.GetNextGames(competition);
                competitionService.PlayGames(games, competition, random);

                competition.IsComplete();

                service.Save();
            }

            IsTrue(service.IsYearDone(league));
            AreEqual(2, league.CurrentYear);
        }
        /* Not sure why i put these here
        [TestMethod]
        public void ShouldGetSeasonCompetitionByReference()
        {
        }
        [TestMethod]
        public void ShouldGetPlayoffCompetitionByReference()
        {
            throw new NotImplementedException();
            
        }
        */
            
        [TestMethod]
        public void ShouldGetNextCompetitionNewYear()
        {
            var league = service.GetByName(LeagueTestDataDriver.LeagueName);

            AreEqual(league.Name, LeagueTestDataDriver.LeagueName);
            AreEqual(2, league.ReferenceCompetitions.Count);

            var nextCompetition = service.GetNextCompetition(league);

            IsNull(nextCompetition);

            league.CurrentYear++;

            nextCompetition = service.GetNextCompetition(league);

            IsNotNull(nextCompetition);
            IsTrue(nextCompetition is Season);
            AreEqual(1, nextCompetition.Year);
            AreEqual(LeagueTestDataDriver.RegularSeasonName, nextCompetition.Name);

        }

        [TestMethod]
        public void ShouldGetNextCompetitionNoneStartedYet()
        {
            var leagueA = service.GetByName(LeagueTestDataDriver.LeagueName);
            leagueA.CurrentYear++;

            var nextCompetitionA = service.GetNextCompetition(leagueA);

            service.Save();

            var league = service.GetByName(LeagueTestDataDriver.LeagueName);

            AreEqual(1, league.CurrentYear);

            var nextCompetition = service.GetNextCompetition(league);

            IsNotNull(nextCompetition);
            IsTrue(nextCompetition is Season);
            AreEqual(1, nextCompetition.Year);
            AreEqual(LeagueTestDataDriver.RegularSeasonName, nextCompetition.Name);
        }
        [TestMethod]
        public void ShouldGetNextCompetitionCompetitionStartedButNotComplete()
        {
            Random random = new Random(12);

            var leagueA = service.GetByName(LeagueTestDataDriver.LeagueName);
            leagueA.CurrentYear++;
            
            var nextCompetitionA = service.GetNextCompetition(leagueA);
            nextCompetitionA.StartCompetition();

            var gamesListA = competitionService.GetNextGames(nextCompetitionA);
            var gameA = gamesListA[0];

            nextCompetitionA.PlayGames(new List<Game>() { gameA }, random);

            service.Save();

            var league = service.GetByName(LeagueTestDataDriver.LeagueName);
            var nextCompetition = service.GetNextCompetition(league);
            IsTrue(nextCompetition.Started);
            IsFalse(nextCompetition.IsComplete());

            AreEqual(-1, competitionService.GetNextGames(nextCompetition).Count - gamesListA.Count);

            
        }
        [TestMethod]
        public void ShouldGetNextCompetitionFirstCompetitionCompleteSecondNotStarted()
        {
            Random random = new Random(12);

            var leagueA = service.GetByName(LeagueTestDataDriver.LeagueName);
            leagueA.CurrentYear++;

            var nextCompetitionA = service.GetNextCompetition(leagueA);
            nextCompetitionA.StartCompetition();

            var gamesListA = competitionService.GetNextGames(nextCompetitionA);
            competitionService.PlayGames(gamesListA, nextCompetitionA, random);

            IsTrue(nextCompetitionA.IsComplete());

            service.Save();

            var league = service.GetByName(LeagueTestDataDriver.LeagueName);

            var nextCompetition = service.GetNextCompetition(league);

            IsTrue(nextCompetition is Playoff);


        }
        [TestMethod]
        public void ShouldGetNextCompetitionFirstCompletedSecondStartedButNotFinished()
        {
            Random random = new Random(12);

            var leagueA = service.GetByName(LeagueTestDataDriver.LeagueName);
            leagueA.CurrentYear++;

            var nextCompetitionA = service.GetNextCompetition(leagueA);
            nextCompetitionA.StartCompetition();

            var gamesListA = competitionService.GetNextGames(nextCompetitionA);
            competitionService.PlayGames(gamesListA, nextCompetitionA, random);


            IsTrue(nextCompetitionA.IsComplete());

            service.Save();            

            var nextCompetitionB = service.GetNextCompetition(leagueA);

            nextCompetitionB.StartCompetition();

            var games = competitionService.GetNextGames(nextCompetitionB);

            nextCompetitionB.PlayGames(new List<Game>() { games[0], games[1], games[2] }, random);

            service.Save();

            var league = service.GetByName(LeagueTestDataDriver.LeagueName);

            var nextCompetition = service.GetNextCompetition(league);

            AreEqual(LeagueTestDataDriver.PlayoffName, nextCompetition.Name);
            AreEqual(1, nextCompetition.Year);
            IsTrue(nextCompetition.Started);
            IsFalse(nextCompetition.IsComplete());

            var nextGames = competitionService.GetNextGames(nextCompetition);

            bool onlyUnfinishedGames = true;

            nextGames.ForEach(g =>
            {
                onlyUnfinishedGames = onlyUnfinishedGames && !g.Complete;
            });

            IsTrue(onlyUnfinishedGames);
        }
    }
}
