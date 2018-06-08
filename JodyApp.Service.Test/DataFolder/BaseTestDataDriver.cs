using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain.Table;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Service.Test
{
    public class BaseTestDataDriver : AbstractTestDataDriver
    {
        public String LeagueName = "Base League Name";

        public BaseTestDataDriver() : base() { }

        public BaseTestDataDriver(JodyAppContext db) : base(db) { }

        public override void PrivateCreateLeagues()
        {
            League l = new League() { Name = LeagueName };
            leagues.Add(l.Name, l);
            
        
        }

        public override void PrivateCreateConfigCompetitions()
        {
            CreateAndAddSeason(leagues[LeagueName], "My Season", 1, 0);
        }

        public override void PrivateCreateDivisions()
        {
            CreateAndAddDivision(GetConfigDivision("League"), leagues[LeagueName], seasons["My Season"], "League",null, 0, 1, null, null);
        }

        public override void PrivateCreateScheduleRules()
        {
            CreateAndAddScheduleRule(leagues[LeagueName], configCompetitions["My Season"], "Rule 1", ConfigScheduleRule.BY_DIVISION, null, configDivisions["League"], ConfigScheduleRule.BY_DIVISION, null, configDivisions["League"], false, 10, 0, 1, false, 1, null);
        }

        public override void PrivateCreateTeams()
        {
            CreateAndAddTeam("Los Angelas", 5, divisions["League"]);
            CreateAndAddTeam("Seattle", 5, divisions["League"]);
            CreateAndAddTeam("Vancouver", 5, divisions["League"]);
            CreateAndAddTeam("Minnesota", 5, divisions["League"]);
        }

        public override void PrivateCreateSortingRules()
        {
            //no special sorting rules by default
            return;
        }

        public override void PrivateCreateSeriesRules()
        {
            return;
        }

        public override void PrivateCreateGroups()
        {
            return;
        }
        public override void PrivateCreateGroupRules()
        {
            return;
        }

        public override void PrivateCreatePlayoffs()
        {
            return;
        }

        public override void PrivateCreateConfigDivisions()
        {
            return;
        }

        public override void PrivateCreateSeries()
        {
            return;
        }

        public override void PrivateCreateConfigSeriesRules()
        {
            return;
        }

        public override void PrivateCreateConfigGroupRules()
        {
            return;
        }

        public override void PrivateCreateConfigGroups()
        {
            return;
        }

        public override void PrivateCreateConfigSortingRules()
        {
            return;
        }

        public override void PrivateCreateConfigTeams()
        {
            return;
        }

        public override void PrivateCreateConfigPlayoffs()
        {
            return;
        }


        public override void PrivateCreateSeasons()
        {
            return;
        }
    }
}

