using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;

namespace JodyApp.Service.Test.DataFolder.DivisionTestData
{
    public class DivisionTestDataDriver
    {
        public static void DeleteAllData(JodyAppContext db)
        {
            string[] tables = { "ScheduleRules", "Seasons", "Teams", "TeamStatistics", "Divisions" };
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
            CreateAndAddDivision("West", 1, 2, divs["League"], divs);
            CreateAndAddDivision("East", 1, 2, divs["League"], divs);
            CreateAndAddDivision("Pacific", 2, 1, divs["West"], divs);
            CreateAndAddDivision("Central", 2, 2, divs["West"], divs);
            CreateAndAddDivision("North West", 2, 3, divs["West"], divs);
            CreateAndAddDivision("North East", 2, 4, divs["East"], divs);
            CreateAndAddDivision("Atlantic", 2, 4, divs["East"], divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;
            
        }

        public static Dictionary<string, Team> CreateTeams(JodyAppContext db, Dictionary<string, Division> divs)
        {
            var teams = new Dictionary<string, Team>();

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

            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public static void InsertData(Database.JodyAppContext db)
        {
            //create divisions
            Dictionary<string, Division> divs = CreateDivisions(db);
            db.SaveChanges();
            Dictionary<string, Team> teams = CreateTeams(db, divs);
            db.SaveChanges();
        }
    }
}

