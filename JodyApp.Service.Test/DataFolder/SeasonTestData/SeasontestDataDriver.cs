using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder.SeasonTestData
{
    public class SeasontestDataDriver:BaseTestDataDriver
    {       
        public SeasontestDataDriver(JodyAppContext db) : base(db) { }

        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {
            League l = new League() { Name = LeagueName };
            leagues.Add(l.Name, l);
            
        
        }

        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "East", null, 1, 2, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "West", null, 1, 3, divs["League"], null, divs);
        }

        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules)
        {
            //CreateAndAddRule(leagues[LeagueName], "Rule 1", ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false, 10, 0, 1, rules);
            CreateAndAddScheduleRule(leagues[LeagueName], seasons["My Season"], "Rule 2", ScheduleRule.BY_DIVISION, null, divs["West"], ScheduleRule.NONE, null, null, false, 10, 0, 2, false, rules);
            CreateAndAddScheduleRule(leagues[LeagueName], seasons["My Season"], "Rule 3", ScheduleRule.BY_DIVISION, null, divs["East"], ScheduleRule.NONE, null, null, false, 10, 0, 3, false, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {
            CreateAndAddTeam("Los Angelas", 5, divs["West"], teams);
            CreateAndAddTeam("Seattle", 5, divs["West"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["West"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["West"], teams);
            CreateAndAddTeam("Toronto", 5, divs["East"], teams);
            CreateAndAddTeam("Montreal", 5, divs["East"], teams);
        }
    }
}
