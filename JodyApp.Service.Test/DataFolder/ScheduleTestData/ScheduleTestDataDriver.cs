using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder.ScheduleTestData
{
    public class ScheduleTestDataDriver:BaseTestDataDriver
    {

        public ScheduleTestDataDriver(JodyAppContext db) : base(db) { }

        public override void PrivateCreateDivisions(Dictionary<string, BaseDivision> divs)
        {            

            CreateAndAddDivision("League", null, 0, 1, null, divs);
            CreateAndAddDivision("Div 1", null, 1, 1, divs["League"], divs);
            CreateAndAddDivision("Div 2", null, 1, 2, divs["League"], divs);
            
        }

        public override void PrivateCreateTeams(Dictionary<string, BaseTeam> teams, Dictionary<string, BaseDivision> divs)
        {            

            CreateAndAddTeam("Team 1", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 2", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 3", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 4", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 5", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 6", 5, divs["Div 2"], teams);
        }

        public override void PrivateCreateRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, BaseScheduleRule> rules)
        {


            CreateAndAddRule("Rule 1", ScheduleRule.BY_TEAM, teams["Team 1"], null, ScheduleRule.BY_DIVISION, null, divs["Div 2"], false, 1, 0, rules);            
            CreateAndAddRule("Rule 2", ScheduleRule.BY_DIVISION, null, divs["Div 2"], ScheduleRule.NONE, null, null, true, 1, 0,rules);
            CreateAndAddRule("Rule 3", ScheduleRule.BY_TEAM, teams["Team 4"], null, ScheduleRule.BY_TEAM, teams["Team 2"], null, false, 1, 0, rules);
            CreateAndAddRule("Rule 4", ScheduleRule.BY_DIVISION, null, divs["Div 1"], ScheduleRule.BY_DIVISION, null, divs["Div 2"], true, 1, 0, rules);

        }

    }
}

