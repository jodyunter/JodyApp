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
    public class DivisionTestDataDriver:BaseTestDataDriver
    {        

        public DivisionTestDataDriver() : base() { }


        public override void PrivateCreateLeagues(Dictionary<string, League> leagues)
        {            

            CreateAndAddLeague(this.LeagueName, leagues);
            CreateAndAddLeague("Extra", leagues);
        }
        public override void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs)
        {            
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "West", null, 1, 2, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "East", null, 1, 2, divs["League"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Pacific", null, 2, 1, divs["West"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Central", null, 2, 2, divs["West"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "North West", null, 2, 3, divs["West"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "North East", null, 2, 4, divs["East"], null, divs);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Atlantic", null, 2, 4, divs["East"], null, divs);

            CreateAndAddDivision(leagues["Extra"], seasons["My Season"], "Extra Top", null, 0, 1, null, null, divs);
            CreateAndAddDivision(leagues["Extra"], seasons["My Season"], "Extra Child", null, 1, 2,divs["Extra Top"], null, divs);


        }

        public override void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Season> seasons, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ConfigScheduleRule> rules)
        {
            CreateAndAddScheduleRule(leagues[LeagueName], seasons["My Season"], "Rule 1", ConfigScheduleRule.BY_DIVISION, null, divs["League"], ConfigScheduleRule.BY_DIVISION, null, divs["League"], false, 2, 0, 1, false, rules);
        }

        public override void PrivateCreateTeams(Dictionary<string, Team> teams, Dictionary<string, Division> divs)
        {            
            CreateAndAddTeam("Los Angelas", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Seattle", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["Pacific"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["Central"], teams);
            CreateAndAddTeam("Colorado", 5, divs["Central"], teams);
            CreateAndAddTeam("Chicago", 5, divs["Central"], teams);
            CreateAndAddTeam("Edmonton", 5, divs["North West"], teams);
            CreateAndAddTeam("Calgary", 5, divs["North West"], teams);
            CreateAndAddTeam("Winnipeg", 5, divs["North West"], teams);
            CreateAndAddTeam("Toronto", 5, divs["North East"], teams);
            CreateAndAddTeam("Montreal", 5, divs["North East"], teams);
            CreateAndAddTeam("Ottawa", 5, divs["North East"], teams);
            CreateAndAddTeam("Quebec City", 5, divs["North East"], teams);
            CreateAndAddTeam("Boston", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("New York", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("Philadelphia", 5, divs["Atlantic"], teams);
            CreateAndAddTeam("Detroit", 5, divs["Atlantic"], teams);

            CreateAndAddTeam("Pittsburgh", 5, divs["Extra Child"], teams);
            CreateAndAddTeam("Minneapolis", 5, divs["Extra Child"], teams);

        }
    }
}

