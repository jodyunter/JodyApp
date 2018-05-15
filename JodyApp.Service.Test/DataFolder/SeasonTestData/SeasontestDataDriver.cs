using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;

namespace JodyApp.Service.Test.DataFolder
{
    public class SeasontestDataDriver:BaseTestDataDriver
    {       
        public SeasontestDataDriver() : base() { }

        public override void PrivateCreateLeagues()
        {
            League l = new League() { Name = LeagueName };
            leagues.Add(l.Name, l);
            
        
        }

        public override void PrivateCreateDivisions()
        {
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "East", null, 1, 2, divisions["League"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "West", null, 1, 3, divisions["League"], null);
        }

        public override void PrivateCreateScheduleRules()
        {
            //CreateAndAddRule(leagues[LeagueName], "Rule 1", ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false, 10, 0, 1, rules);
            CreateAndAddScheduleRule(leagues[LeagueName], configCompetitions["My Season"], "Rule 2", ConfigScheduleRule.BY_DIVISION, null, configDivisions["West"], ConfigScheduleRule.NONE, null, null, false, 10, 0, 2, false);
            CreateAndAddScheduleRule(leagues[LeagueName], configCompetitions["My Season"], "Rule 3", ConfigScheduleRule.BY_DIVISION, null, configDivisions["East"], ConfigScheduleRule.NONE, null, null, false, 10, 0, 3, false);
        }

        public override void PrivateCreateTeams()
        {
            CreateAndAddConfigTeam("Los Angelas", 5, configDivisions["West"], leagues[LeagueName], 1, null);
            CreateAndAddConfigTeam("Seattle", 5, configDivisions["West"], leagues[LeagueName], 1, null);
            CreateAndAddConfigTeam("Vancouver", 5, configDivisions["West"], leagues[LeagueName], 1, null);
            CreateAndAddConfigTeam("Minnesota", 5, configDivisions["West"], leagues[LeagueName], 1, null);
            CreateAndAddConfigTeam("Toronto", 5, configDivisions["East"], leagues[LeagueName], 1, null);
            CreateAndAddConfigTeam("Montreal", 5, configDivisions["East"], leagues[LeagueName], 1, null);
        }
    }
}
