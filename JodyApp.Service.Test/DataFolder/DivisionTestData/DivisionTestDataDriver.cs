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


        public override void PrivateCreateLeagues()
        {            

            CreateAndAddLeague(this.LeagueName);
            CreateAndAddLeague("Extra");
        }
        public override void PrivateCreateDivisions()
        {            
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "League", null, 0, 1, null, null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "West", null, 1, 2, divisions["League"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "East", null, 1, 2, divisions["League"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Pacific", null, 2, 1, divisions["West"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Central", null, 2, 2, divisions["West"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "North West", null, 2, 3, divisions["West"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "North East", null, 2, 4, divisions["East"], null);
            CreateAndAddDivision(leagues[LeagueName], seasons["My Season"], "Atlantic", null, 2, 4, divisions["East"], null);

            CreateAndAddDivision(leagues["Extra"], seasons["My Season"], "Extra Top", null, 0, 1, null, null);
            CreateAndAddDivision(leagues["Extra"], seasons["My Season"], "Extra Child", null, 1, 2, divisions["Extra Top"], null);


        }

        public override void PrivateCreateScheduleRules()
        {
            CreateAndAddScheduleRule(leagues[LeagueName], configSeasons["My Season"], "Rule 1", ConfigScheduleRule.BY_DIVISION, null, configDivisions["League"], ConfigScheduleRule.BY_DIVISION, null, configDivisions["League"], false, 2, 0, 1, false);
        }

        public override void PrivateCreateTeams()
        {            
            CreateAndAddTeam("Los Angelas", 5, divisions["Pacific"]);
            CreateAndAddTeam("Seattle", 5, divisions["Pacific"]);
            CreateAndAddTeam("Vancouver", 5, divisions["Pacific"]);
            CreateAndAddTeam("Minnesota", 5, divisions["Central"]);
            CreateAndAddTeam("Colorado", 5, divisions["Central"]);
            CreateAndAddTeam("Chicago", 5, divisions["Central"]);
            CreateAndAddTeam("Edmonton", 5, divisions["North West"]);
            CreateAndAddTeam("Calgary", 5, divisions["North West"]);
            CreateAndAddTeam("Winnipeg", 5, divisions["North West"]);
            CreateAndAddTeam("Toronto", 5, divisions["North East"]);
            CreateAndAddTeam("Montreal", 5, divisions["North East"]);
            CreateAndAddTeam("Ottawa", 5, divisions["North East"]);
            CreateAndAddTeam("Quebec City", 5, divisions["North East"]);
            CreateAndAddTeam("Boston", 5, divisions["Atlantic"]);
            CreateAndAddTeam("New York", 5, divisions["Atlantic"]);
            CreateAndAddTeam("Philadelphia", 5, divisions["Atlantic"]);
            CreateAndAddTeam("Detroit", 5, divisions["Atlantic"]);

            CreateAndAddTeam("Pittsburgh", 5, divisions["Extra Child"]);
            CreateAndAddTeam("Minneapolis", 5, divisions["Extra Child"]);

        }
    }
}

