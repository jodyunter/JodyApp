using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder.ScheduleTestData
{
    public class ScheduleTestDataDriver:BaseTestDataDriver
    {        
        public ScheduleTestDataDriver(JodyAppContext db) : base(db) { }

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Division> divs)
        {            

            CreateAndAddDivision(leagues[LeagueName], "League", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues[LeagueName], "Div 1", null, 1, 1, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], "Div 2", null, 1, 2, divs["League"], null, divs);
            
        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {            

            CreateAndAddTeam("Team 1", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 2", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 3", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 4", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 5", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 6", 5, divs["Div 2"], teams);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {            
            CreateAndAddRule(ScheduleRule.CreateByTeamVsDivision(leagues[LeagueName],"Rule 1", teams["Team 1"], divs["Div 2"], false, 1, 1), rules);                        
            CreateAndAddRule(ScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], "Rule 2", divs["Div 2"], true, 1, 1), rules);
            CreateAndAddRule(ScheduleRule.CreateByTeamVsTeam(leagues[LeagueName], "Rule 3", teams["Team 4"], teams["Team 2"], false, 1, 1), rules);            
            CreateAndAddRule(ScheduleRule.CreateByDivisionVsDivision(leagues[LeagueName], "Rule 4", divs["Div 1"], divs["Div 2"], true, 1, 1), rules);
            CreateAndAddRule(ScheduleRule.CreateByDivisionLevel(leagues[LeagueName], "Rule 5", 0, true, 2, 1), rules);
            CreateAndAddRule(ScheduleRule.CreateByDivisionLevel(leagues[LeagueName], "Rule 6", 1, true, 2, 1), rules);


        }

    }
}

