using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.Test.DataFolder
{
    public abstract class AbstractTestDataDriver
    {
        JodyAppContext db;

        public AbstractTestDataDriver(JodyAppContext db)
        {
            this.db = db;
        }
        abstract public void PrivateCreateTeams( Dictionary<string, BaseTeam> teams, Dictionary<string, BaseDivision> divs);
        abstract public void PrivateCreateDivisions(Dictionary<string, BaseDivision> divs);
        abstract public void PrivateCreateRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, ScheduleRule> rules);

        public void DeleteAllData()
        {
            string[] tables = { "ScheduleRules", "Teams", "TeamStatistics", "Seasons", "Divisions" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public void CreateAndAddDivision(string name, int level, int order, Division parent, Dictionary<string, BaseDivision> map)
        {
            BaseDivision div = new BaseDivision(name, level, order, parent);
            map.Add(div.Name, div);
        }

        public void CreateAndAddTeam(string name, int skill, Division division, Dictionary<string, BaseTeam> map)
        {
            BaseTeam team = new BaseTeam(name, skill, division);
            map.Add(team.Name, team);
            division.Teams.Add(team);
        }        


        public void CreateAndAddRule(string name, 
                                int homeType, Team homeTeam, Division homeDivision,
                                int awayType, Team awayTeam, Division awayDivision,
                                bool homeAndAway, int rounds,
                                    Dictionary<string, ScheduleRule> map)
        {
            ScheduleRule rule = new ScheduleRule(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, homeAndAway, rounds);
            map.Add(rule.Name, rule);            
        }


        public Dictionary<string, BaseDivision> CreateDivisions()
        {
            var divs = new Dictionary<string, BaseDivision>();

            PrivateCreateDivisions(divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;

        }


        public Dictionary<string, BaseTeam> CreateTeams(Dictionary<string, BaseDivision> divs)
        {
            var teams = new Dictionary<string, BaseTeam>();

            PrivateCreateTeams(teams, divs);

            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public Dictionary<string, ScheduleRule> CreateRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams)
        {
            var rules = new Dictionary<string, ScheduleRule>();

            PrivateCreateRules(divs, teams, rules);
            
            db.ScheduleRules.AddRange(rules.Values);
            db.SaveChanges();
            return rules;

        }

        public void InsertData()
        {
            //create divisions
            Dictionary<string, BaseDivision> divs = CreateDivisions();
            Dictionary<string, BaseTeam> teams = CreateTeams(divs);
            Dictionary<string, ScheduleRule> scheduleRules = CreateRules(divs, teams);
        }
    }
}
