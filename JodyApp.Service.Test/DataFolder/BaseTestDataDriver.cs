﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder
{
    public class BaseTestDataDriver : AbstractTestDataDriver
    {
        public BaseTestDataDriver(JodyAppContext db) : base(db) { }


        public override void PrivateCreateDivisions(Dictionary<string, ConfigDivision> divs)
        {
            CreateAndAddDivision("League",null, 0, 1, null, null, divs);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigScheduleRule> rules)
        {
            CreateAndAddRule("Rule 1", ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false, 10, 0, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigDivision> divs)
        {
            CreateAndAddTeam("Los Angelas", 5, divs["League"], teams);
            CreateAndAddTeam("Seattle", 5, divs["League"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["League"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["League"], teams);
        }


    }
}

