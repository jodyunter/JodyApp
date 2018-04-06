using System;
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
        public static Team CreateBasicTeam(String name, int skill)
        {
            return new Team
            {
                Name = name,
                Skill = skill
            };
        }

    }
}
