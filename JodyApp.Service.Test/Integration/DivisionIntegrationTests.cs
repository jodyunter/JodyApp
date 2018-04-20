using System;
using System.Collections.Generic;
using System.Linq;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Service.Test.DataFolder.DivisionTestData;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JodyApp.Service.Test.Integration
{
    [TestClass]
    public class DivisionIntegrationTests
    {
        String LeagueName = "My League";
        Database.JodyAppContext db = new Database.JodyAppContext();
        DivisionTestDataDriver driver;
        League league;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new DivisionTestDataDriver(db);            
            driver.DeleteAllData();
            driver.InsertData();
            league = db.Leagues.Where(l => l.Name == LeagueName).First();
        }

        /*Required Tests
         *  Get By Name for season and normal
         *  Get teams in division for season and normal
         *  
         */
        [TestMethod]
        public void ShouldGetByName()
        {
            Division d = new ConfigDivision() { Name = "West", League = league}.GetByName(db);

            IsNotNull(d.Id);
            AreEqual(d.Name, "West");

        }

        [TestMethod]
        public void ShouldGetByParentTwoLevelsUp()
        {
            List<Division> d = new ConfigDivision() { Name = "League", League = league }.GetByName(db).GetDivisionsByParent(db);

            AreEqual(2, d.Count);
        }

        [TestMethod]
        public void ShouldGetByparentOneLevelUp()
        {
            List<Division> d = new ConfigDivision() { Name = "West", League = league }.GetByName(db).GetDivisionsByParent(db);

            AreEqual(3, d.Count);
        }

        [TestMethod]
        public void ShouldGetTeamsInDivision()
        {
            List<Team> teams = new ConfigDivision() { Name = "League", League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(17, teams.Count);

            teams = new ConfigDivision() { Name = "East", League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(8, teams.Count);

            teams = new ConfigDivision() { Name = "West", League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(9, teams.Count);

            teams = new ConfigDivision() { Name = "Atlantic", League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(4, teams.Count);

            teams = new ConfigDivision() { Name = "Central", League = league }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(3, teams.Count);
        }
    }

}
