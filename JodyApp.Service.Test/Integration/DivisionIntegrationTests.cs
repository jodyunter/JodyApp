using System;
using System.Collections.Generic;
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
        Database.JodyAppContext db = new Database.JodyAppContext();
        DivisionTestDataDriver driver;



        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            driver = new DivisionTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
        }

        /*Required Tests
         *  Get By Name for season and normal
         *  Get teams in division for season and normal
         *  
         */
        [TestMethod]
        public void ShouldGetByName()
        {
            Division d = new ConfigDivision() { Name = "West" }.GetByName(db);

            IsNotNull(d.Id);
            AreEqual(d.Name, "West");

        }

        [TestMethod]
        public void ShouldGetByParentTwoLevelsUp()
        {
            List<Division> d = new ConfigDivision() { Name = "League" }.GetByName(db).GetDivisionsByParent(db);

            AreEqual(2, d.Count);
        }

        [TestMethod]
        public void ShouldGetByparentOneLevelUp()
        {
            List<Division> d = new ConfigDivision() { Name = "West" }.GetByName(db).GetDivisionsByParent(db);

            AreEqual(3, d.Count);
        }

        [TestMethod]
        public void ShouldGetTeamsInDivision()
        {
            List<Team> teams = new ConfigDivision() { Name = "League" }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(17, teams.Count);

            teams = new ConfigDivision() { Name = "East" }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(8, teams.Count);

            teams = new ConfigDivision() { Name = "West" }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(9, teams.Count);

            teams = new ConfigDivision() { Name = "Atlantic" }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(4, teams.Count);

            teams = new ConfigDivision() { Name = "Central" }.GetByName(db).GetAllTeamsInDivision(db);

            AreEqual(3, teams.Count);
        }
    }

}
