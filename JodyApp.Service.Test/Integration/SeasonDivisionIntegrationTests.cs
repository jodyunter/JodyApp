using System;
using System.Collections.Generic;
using System.Linq;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Season;
using JodyApp.Service.Test.DataFolder.DivisionTestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JodyApp.Service.Test.Integration
{
    [TestClass]
    public class SeasonDivisionIntegrationTests
    {     
        Database.JodyAppContext db = new Database.JodyAppContext();
        DivisionTestDataDriver driver;
        SeasonService service;
        Season season;
        League league;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new DivisionTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();

            league = db.Leagues.Where(l => l.Name == driver.LeagueName).First();
            service = new SeasonService(db);
            season = service.CreateNewSeason(league, "New Season", 15);
        }

        [TestMethod]
        public void ShouldGetByName()
        {
            Division d = new ConfigDivision() { Name = "West", League = league}.GetByName(db);
            Division sd = new SeasonDivision() { Name = "West" , Season = season, League = league }.GetByName(db);

            IsNotNull(sd.Id);
            AreEqual(sd.Name, "West");
            AreNotEqual(sd.Id, d.Id);

        }

        [TestMethod]
        public void ShouldGetByParentTwoLevelsUp()

        {
            SeasonDivision sd = (SeasonDivision)new SeasonDivision() { Name = "League", Season = season, League = league }.GetByName(db);
            List<SeasonDivision> d = sd.GetDivisionsByParent(db);

            AreEqual(2, d.Count);
        }

        [TestMethod]
        public void ShouldGetByparentOneLevelUp()
        {
            SeasonDivision sd = (SeasonDivision)new SeasonDivision() { Name = "West", Season = season, League = league }.GetByName(db);
            List<SeasonDivision> d = sd.GetDivisionsByParent(db);

            AreEqual(3, d.Count);
        }


        [TestMethod]
        public void ShouldGetSeasonTeamsInSeasonDivision()
        {
            List<SeasonTeam> teams = new SeasonDivision() { Name = "League", Season = season, League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(17, teams.Count);

            teams = new SeasonDivision() { Name = "East", Season = season, League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(8, teams.Count);

            teams = new SeasonDivision() { Name = "West", Season = season, League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(9, teams.Count);

            teams = new SeasonDivision() { Name = "Atlantic", Season = season, League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(4, teams.Count);

            teams = new SeasonDivision() { Name = "Central", Season = season, League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(3, teams.Count);
        }

    }

}
