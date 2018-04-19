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

            CreateAndAddDivision("League", null, 0, 1, null, null, divs);
            CreateAndAddDivision("Div 1", null, 1, 1, divs["League"], null, divs);
            CreateAndAddDivision("Div 2", null, 1, 2, divs["League"], null, divs);
            
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

        public override void PrivateCreateScheduleRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, BaseScheduleRule> rules)
        {            
            CreateAndAddRule(BaseScheduleRule.CreateByTeamVsDivision("Rule 1", teams["Team 1"], divs["Div 2"], false, 1), rules);                        
            CreateAndAddRule(BaseScheduleRule.CreateByDivisionVsSelf("Rule 2", divs["Div 2"], true, 1), rules);
            CreateAndAddRule(BaseScheduleRule.CreateByTeamVsTeam("Rule 3", teams["Team 4"], teams["Team 2"], false, 1), rules);            
            CreateAndAddRule(BaseScheduleRule.CreateByDivisionVsDivision("Rule 4", divs["Div 1"], divs["Div 2"], true, 1), rules);
            CreateAndAddRule(BaseScheduleRule.CreateByDivisionLevel("Rule 5", 0, true, 2), rules);
            CreateAndAddRule(BaseScheduleRule.CreateByDivisionLevel("Rule 6", 1, true, 2), rules);


        }

    }
}

