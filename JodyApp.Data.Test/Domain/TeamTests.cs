﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Database;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void ShouldInsertTeam()
        {
            var db = new JodyAppContext();

            Team team = new Team
            {
                Name = "My Team",
                Skill = 12,
                Division = null

            };

            db.Teams.Add(team);
            db.SaveChanges();


        }

    }
}
