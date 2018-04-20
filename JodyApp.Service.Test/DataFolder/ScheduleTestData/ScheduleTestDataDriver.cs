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

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, ConfigDivision> divs)
        {            

            CreateAndAddDivision(leagues[LeagueName], "League", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues[LeagueName], "Div 1", null, 1, 1, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], "Div 2", null, 1, 2, divs["League"], null, divs);
            
        }

        public override void PrivateCreateTeams(Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigDivision> divs)
        {            

            CreateAndAddTeam("Team 1", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 2", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 3", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 4", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 5", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 6", 5, divs["Div 2"], teams);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigScheduleRule> rules)
        {            
            CreateAndAddRule(ConfigScheduleRule.CreateByTeamVsDivision("Rule 1", teams["Team 1"], divs["Div 2"], false, 1), rules);                        
            CreateAndAddRule(ConfigScheduleRule.CreateByDivisionVsSelf("Rule 2", divs["Div 2"], true, 1), rules);
            CreateAndAddRule(ConfigScheduleRule.CreateByTeamVsTeam("Rule 3", teams["Team 4"], teams["Team 2"], false, 1), rules);            
            CreateAndAddRule(ConfigScheduleRule.CreateByDivisionVsDivision("Rule 4", divs["Div 1"], divs["Div 2"], true, 1), rules);
            CreateAndAddRule(ConfigScheduleRule.CreateByDivisionLevel("Rule 5", 0, true, 2), rules);
            CreateAndAddRule(ConfigScheduleRule.CreateByDivisionLevel("Rule 6", 1, true, 2), rules);


        }

    }
}

