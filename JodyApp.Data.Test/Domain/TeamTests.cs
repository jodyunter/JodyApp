using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JodyApp.Domain;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Data.Test.Domain
{
    [TestClass]
    public class TeamTests
    {
        public static List<Team> CreateBasicTeams(string[] teams)
        {
            List<Team> list = new List<Team>();

            foreach(string t in teams)
            {
                list.Add(CreateBasicTeam(t, 5));
            }

            return list;

        }
        public static Team CreateBasicTeam(String name, int skill)
        {
            return new ConfigTeam
            {
                Name = name,
                Skill = skill
            };
        }

    }
}
