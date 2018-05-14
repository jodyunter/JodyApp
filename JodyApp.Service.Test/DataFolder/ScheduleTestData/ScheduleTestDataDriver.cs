using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Service.Test.DataFolder
{
    public class ScheduleTestDataDriver:BaseTestDataDriver
    {        
        public ScheduleTestDataDriver() : base() { }

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {            

            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Div 1", null, 1, 1, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Div 2", null, 1, 2, divs["League"], null, divs);
            
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

        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ConfigScheduleRule> rules)
        {            
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByTeamVsDivision(leagues[LeagueName], seasons["My Season"], "Rule 1", teams["Team 1"], divs["Div 2"], false, 1, 1, false), rules);                        
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], seasons["My Season"], "Rule 2", divs["Div 2"], true, 1, 1, false), rules);
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByTeamVsTeam(leagues[LeagueName], seasons["My Season"], "Rule 3", teams["Team 4"], teams["Team 2"], false, 1, 1, false), rules);            
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionVsDivision(leagues[LeagueName], seasons["My Season"], "Rule 4", divs["Div 1"], divs["Div 2"], true, 1, 1, false), rules);
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionLevel(leagues[LeagueName], seasons["My Season"], "Rule 5", 0, true, 2, 1, false), rules);
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionLevel(leagues[LeagueName], seasons["My Season"], "Rule 6", 1, true, 2, 1, false), rules);


        }

    }
}

