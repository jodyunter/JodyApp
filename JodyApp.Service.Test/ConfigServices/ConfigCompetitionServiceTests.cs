using JodyApp.Service.ConfigServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace JodyApp.Service.Test.ConfigServices
{
    [TestClass]
    public class ConfigCompetitionServiceTests
    {
        ConfigCompetitionTestDataDriver driver;
        ConfigCompetitionService service;
        [TestInitialize]
        public void setup()
        {
            driver = new ConfigCompetitionTestDataDriver();
            driver.Setup();

            service = new ConfigCompetitionService(driver.db);
        }

        [TestMethod]
        public void ShouldGetAll()
        {
            var result = service.GetAll();

            AreEqual(3, result.Items.Count());
        }

        
    }
}
