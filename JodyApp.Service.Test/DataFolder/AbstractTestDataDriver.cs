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
        abstract public void PrivateCreateTeams( Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigDivision> divs);
        abstract public void PrivateCreateDivisions(Dictionary<string, ConfigDivision> divs);
        abstract public void PrivateCreateScheduleRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams, Dictionary<string, ConfigScheduleRule> rules);                        
        public void DeleteAllData()
        {
            string[] tables = { "DivisionRanks", "ScheduleRules", "Teams", "TeamStatistics", "Divisions", "Seasons" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public Division CreateAndAddDivision(string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules, Dictionary<string, ConfigDivision> map)
        {
            ConfigDivision div = new ConfigDivision(name, shortName, level, order, parent, sortingRules);
            map.Add(div.Name, div);
            return div;
        }

        public Team CreateAndAddTeam(string name, int skill, Division division, Dictionary<string, ConfigTeam> map)
        {
            ConfigTeam team = new ConfigTeam(name, skill, division);
            map.Add(team.Name, team);
            division.Teams.Add(team);
            return team;
        }        
        
        public ScheduleRule CreateAndAddRule(ScheduleRule newRule, Dictionary<string, ConfigScheduleRule> map)
        {
            ConfigScheduleRule rule = new ConfigScheduleRule(newRule);
            map.Add(rule.Name, rule);
            return rule;
        }

        public ScheduleRule CreateAndAddRule(string name, 
                                int homeType, Team homeTeam, Division homeDivision,
                                int awayType, Team awayTeam, Division awayDivision,
                                bool homeAndAway, int rounds, int divisionLevel,
                                    Dictionary<string, ConfigScheduleRule> map)
        {
            ConfigScheduleRule rule = new ConfigScheduleRule(name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, homeAndAway, rounds, divisionLevel);
            map.Add(rule.Name, rule);
            return rule;
        }


        public Dictionary<string, ConfigDivision> CreateDivisions()
        {
            var divs = new Dictionary<string, ConfigDivision>();

            PrivateCreateDivisions(divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;

        }


        public Dictionary<string, ConfigTeam> CreateTeams(Dictionary<string, ConfigDivision> divs)
        {
            var teams = new Dictionary<string, ConfigTeam>();

            PrivateCreateTeams(teams, divs);

            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public Dictionary<string, ConfigScheduleRule> CreateRules(Dictionary<string, ConfigDivision> divs, Dictionary<string, ConfigTeam> teams)
        {
            var rules = new Dictionary<string, ConfigScheduleRule>();

            PrivateCreateScheduleRules(divs, teams, rules);
            
            db.ScheduleRules.AddRange(rules.Values);
            db.SaveChanges();
            return rules;

        }

        public void InsertData()
        {
            //create divisions
            Dictionary<string, ConfigDivision> divs = CreateDivisions();
            Dictionary<string, ConfigTeam> teams = CreateTeams(divs);
            Dictionary<string, ConfigScheduleRule> scheduleRules = CreateRules(divs, teams);
        }
    }
}
