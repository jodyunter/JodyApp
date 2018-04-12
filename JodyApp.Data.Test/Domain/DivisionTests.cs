using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class DivisionTests
    {
        [TestMethod]
        public void ShouldGetShortNameWhenNull()
        {
            Division d = new Division("Name", null, 0, 0, null);

            AreEqual(d.Name, d.ShortName);
        }
        [TestMethod]
        public void ShouldGetShortNameWhenNotNull()
        {
            Division d = new Division("Name", "SHORT", 0, 0, null);

            AreNotEqual(d.Name, d.ShortName);
            AreEqual("SHORT", d.ShortName);
        }
    }
}
