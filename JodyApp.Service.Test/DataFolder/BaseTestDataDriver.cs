using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder
{
    public class BaseTestDataDriver
    {
        public static void DeleteAllData(JodyAppContext db)
        {
            string[] tables = { "ScheduleRules", "Teams", "TeamStatistics", "Seasons", "Divisions" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public static void CreateAndAddDivision(string name, int level, int order, Division parent, Dictionary<string, Division> map)
        {
            Division div = new Division(name, level, order, parent);
            map.Add(div.Name, div);
        }

        public static void CreateAndAddTeam(string name, int skill, Division division, Dictionary<string, Team> map)
        {
            Team team = new Team(name, skill, division);
            map.Add(team.Name, team);
            division.Teams.Add(team);
        }
        
                    

        public static Dictionary<string, Division> CreateDivisions(JodyAppContext db)
        {
            var divs = new Dictionary<string, Division>();

            CreateAndAddDivision("League", 0, 1, null, divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;
            
        }

        public static Dictionary<string, Team> CreateTeams(JodyAppContext db, Dictionary<string, Division> divs)
        {
            var teams = new Dictionary<string, Team>();

            CreateAndAddTeam("Los Angelas", 5, divs["League"], teams);
            CreateAndAddTeam("Seattle", 5, divs["League"], teams);
            CreateAndAddTeam("Vancouver", 5, divs["League"], teams);
            CreateAndAddTeam("Minnesota", 5, divs["League"], teams);


            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public static Dictionary<string, ScheduleRule> CreateRules(JodyAppContext db, Dictionary<string, Division> divs, Dictionary<string, Team> teams)
        {
            var rules = new Dictionary<string, ScheduleRule>();

            ScheduleRule rule1 = new ScheduleRule(ScheduleRule.BY_DIVISION, null, divs["League"], ScheduleRule.BY_DIVISION, null, divs["League"], false);

            rules.Add("Rule 1", rule1);
            db.ScheduleRules.AddRange(rules.Values);
            db.SaveChanges();
            return rules;

        }

        public static void InsertData(JodyAppContext db)
        {
            //create divisions
            Dictionary<string, Division> divs = CreateDivisions(db);            
            Dictionary<string, Team> teams = CreateTeams(db, divs);            
            Dictionary<string, ScheduleRule> scheduleRules = CreateRules(db, divs, teams);            
        }
    }
}

