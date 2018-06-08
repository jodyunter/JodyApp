using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Service.Test
{
    public class ScheduleTestDataDriver:BaseTestDataDriver
    {        
        public ScheduleTestDataDriver() : base() { }

        public override void PrivateCreateDivisions()
        {            

            CreateAndAddDivision(GetConfigDivision("League"), leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null);
            CreateAndAddDivision(GetConfigDivision("Div 1"), leagues[LeagueName], seasons["My Season"], "Div 1", null, 1, 1, divisions["League"], null);
            CreateAndAddDivision(GetConfigDivision("Div 2"), leagues[LeagueName], seasons["My Season"], "Div 2", null, 1, 2, divisions["League"], null);
            
        }

        public override void PrivateCreateTeams()
        {            

            CreateAndAddTeam("Team 1", 5, divisions["Div 1"]);
            CreateAndAddTeam("Team 2", 5, divisions["Div 1"]);
            CreateAndAddTeam("Team 3", 5, divisions["Div 1"]);
            CreateAndAddTeam("Team 4", 5, divisions["Div 2"]);
            CreateAndAddTeam("Team 5", 5, divisions["Div 2"]);
            CreateAndAddTeam("Team 6", 5, divisions["Div 2"]);
        }

        public override void PrivateCreateScheduleRules()
        {            
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByTeamVsDivision(leagues[LeagueName], configCompetitions["My Season"], "Rule 1", configTeams["Team 1"], configDivisions["Div 2"], false, 1, 1, false, 1, null));                        
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionVsSelf(leagues[LeagueName], configCompetitions["My Season"], "Rule 2", configDivisions["Div 2"], true, 1, 1, false, 1, null));
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByTeamVsTeam(leagues[LeagueName], configCompetitions["My Season"], "Rule 3", configTeams["Team 4"], configTeams["Team 2"], false, 1, 1, false, 1, null));            
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionVsDivision(leagues[LeagueName], configCompetitions["My Season"], "Rule 4", configDivisions["Div 1"], configDivisions["Div 2"], true, 1, 1, false, 1, null));
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionLevel(leagues[LeagueName], configCompetitions["My Season"], "Rule 5", 0, true, 2, 1, false, 1, null));
            CreateAndAddScheduleRule(ConfigScheduleRule.CreateByDivisionLevel(leagues[LeagueName], configCompetitions["My Season"], "Rule 6", 1, true, 2, 1, false, 1, null));


        }

        public override void PrivateCreateConfigDivisions()
        {
            base.PrivateCreateConfigDivisions();
        }

        public override void PrivateCreateConfigTeams()
        {
            base.PrivateCreateConfigTeams();
        }

    }
}

