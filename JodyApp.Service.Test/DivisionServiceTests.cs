using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Service.Test.DataFolder.DivisionTestData;
using JodyApp.Domain;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class DivisionServiceTests
    {

        //these tests depend on specific data
        [TestMethod]
        public void ShouldGetDivisionsByParent()
        {
            Database.JodyAppContext db = new Database.JodyAppContext();
            DivisionTestDataDriver.DeleteAllData(db);
            DivisionTestDataDriver.InsertData(db);
            DivisionService service = new DivisionService(db);

            List<Division> divisions = service.GetDivisionsByParent(service.GetByName("League"));

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
        }

        [TestMethod]
        public void ShouldGetTeamsInDivision()
        {
            Database.JodyAppContext db = new Database.JodyAppContext();
            DivisionTestDataDriver.DeleteAllData(db);
            DivisionTestDataDriver.InsertData(db);
            DivisionService service = new DivisionService(db);

            List<Team> teams = service.GetAllTeamsInDivision(service.GetByName("League"));

            AreEqual(17, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("East"));

            AreEqual(8, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("West"));

            AreEqual(9, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Atlantic"));

            AreEqual(4, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Central"));

            AreEqual(3, teams.Count);
        }
    }
}
