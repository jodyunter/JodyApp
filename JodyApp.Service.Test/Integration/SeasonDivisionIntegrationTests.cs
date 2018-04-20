using System;
using System.Collections.Generic;
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

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new DivisionTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            service = new SeasonService(db);
            season = service.CreateNewSeason("New Season", 15);
        }

        /*Required Tests
         *  Get By Name for season and normal
         *  Get teams in division for season and normal
         *  
         */
        [TestMethod]
        public void ShouldGetByName()
        {
            Division d = new ConfigDivision() { Name = "West"}.GetByName(db);
            Division sd = new SeasonDivision() { Name = "West" , Season = season}.GetByName(db);

            IsNotNull(sd.Id);
            AreEqual(sd.Name, "West");
            AreNotEqual(sd.Id, d.Id);

        }

        [TestMethod]
        public void ShouldGetByParentTwoLevelsUp()

        {
            SeasonDivision sd = (SeasonDivision)new SeasonDivision() { Name = "League", Season = season }.GetByName(db);
            List<Division> d = sd.GetDivisionsByParent(db);

            AreEqual(2, d.Count);
        }

        [TestMethod]
        public void ShouldGetByparentOneLevelUp()
        {
            SeasonDivision sd = (SeasonDivision)new SeasonDivision() { Name = "West", Season = season }.GetByName(db);
            List<Division> d = sd.GetDivisionsByParent(db);

            AreEqual(3, d.Count);
        }


        [TestMethod]
        public void ShouldGetSeasonTeamsInSeasonDivision()
        {
            List<Team> teams = new SeasonDivision() { Name = "League", Season = season }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(17, teams.Count);

            teams = new SeasonDivision() { Name = "East", Season = season }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(8, teams.Count);

            teams = new SeasonDivision() { Name = "West", Season = season }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(9, teams.Count);

            teams = new SeasonDivision() { Name = "Atlantic", Season = season }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(4, teams.Count);

            teams = new SeasonDivision() { Name = "Central", Season = season }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(3, teams.Count);
        }

    }

}
