using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;
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
        abstract public void PrivateCreateScheduleRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams, Dictionary<string, BaseScheduleRule> rules);                        
        public void DeleteAllData()
        {
            string[] tables = { "ScheduleRules", "Teams", "TeamStatistics", "Divisions", "Seasons" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public Division CreateAndAddDivision(string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules, Dictionary<string, BaseDivision> map)
        {
            BaseDivision div = new BaseDivision(name, shortName, level, order, parent, sortingRules);
            map.Add(div.Name, div);
            return div;
        }

        public Team CreateAndAddTeam(string name, int skill, Division division, Dictionary<string, BaseTeam> map)
        {
            BaseTeam team = new BaseTeam(name, skill, division);
            map.Add(team.Name, team);
            division.Teams.Add(team);
            return team;
        }        
        
        public ScheduleRule CreateAndAddRule(ScheduleRule newRule, Dictionary<string, BaseScheduleRule> map)
        {
            BaseScheduleRule rule = new BaseScheduleRule(newRule);
            map.Add(rule.Name, rule);
            return rule;
        }

        public ScheduleRule CreateAndAddRule(string name, 
                                int homeType, Team homeTeam, Division homeDivision,
                                int awayType, Team awayTeam, Division awayDivision,
                                bool homeAndAway, int rounds, int divisionLevel,
                                    Dictionary<string, BaseScheduleRule> map)
        {
            BaseScheduleRule rule = new BaseScheduleRule(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, homeAndAway, rounds, divisionLevel);
            map.Add(rule.Name, rule);
            return rule;
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

        public Dictionary<string, BaseScheduleRule> CreateRules(Dictionary<string, BaseDivision> divs, Dictionary<string, BaseTeam> teams)
        {
            var rules = new Dictionary<string, BaseScheduleRule>();

            PrivateCreateScheduleRules(divs, teams, rules);
            
            db.ScheduleRules.AddRange(rules.Values);
            db.SaveChanges();
            return rules;

        }

        public void InsertData()
        {
            //create divisions
            Dictionary<string, BaseDivision> divs = CreateDivisions();
            Dictionary<string, BaseTeam> teams = CreateTeams(divs);
            Dictionary<string, BaseScheduleRule> scheduleRules = CreateRules(divs, teams);
        }
    }
}
