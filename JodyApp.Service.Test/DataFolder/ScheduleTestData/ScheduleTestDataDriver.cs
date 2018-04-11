using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test.DataFolder.ScheduleTestData
{
    public class ScheduleTestDataDriver
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
            CreateAndAddDivision("Div 1", 1, 1, divs["League"], divs);
            CreateAndAddDivision("Div 2", 1, 2, divs["League"], divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;
            
        }

        public static Dictionary<string, Team> CreateTeams(JodyAppContext db, Dictionary<string, Division> divs)
        {
            var teams = new Dictionary<string, Team>();

            CreateAndAddTeam("Team 1", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 2", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 3", 5, divs["Div 1"], teams);
            CreateAndAddTeam("Team 4", 5, divs["Div 2"], teams);
            CreateAndAddTeam("Team 5", 5, divs["Div 2"], teams);


            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public static Dictionary<string, ScheduleRule> CreateRules(JodyAppContext db, Dictionary<string, Division> divs, Dictionary<string, Team> teams)
        {
            var rules = new Dictionary<string, ScheduleRule>();

            ScheduleRule rule1 = new ScheduleRule("Rule 1", ScheduleRule.BY_TEAM, teams["Team 1"], null, ScheduleRule.BY_DIVISION, null, divs["Div 2"], false, 1);            
            ScheduleRule rule2 = new ScheduleRule("Rule 2", ScheduleRule.BY_DIVISION, null, divs["Div 2"], ScheduleRule.NONE, null, null, true, 1);
            ScheduleRule rule3 = new ScheduleRule("Rule 3", ScheduleRule.BY_TEAM, teams["Team 4"], null, ScheduleRule.BY_TEAM, teams["Team 2"], null, false, 1);
            //ScheduleRule rule6 = new ScheduleRule("Rule 6", ScheduleRule.BY_DIVISION, null, divs["Div 1"], ScheduleRule.BY_DIVISION, null, divs["Div 2"], false);            

            rules.Add("Rule 1", rule1);
            rules.Add("Rule 2", rule2);
            rules.Add("Rule 3", rule3);
            //rules.Add("Rule 6", rule6);
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

