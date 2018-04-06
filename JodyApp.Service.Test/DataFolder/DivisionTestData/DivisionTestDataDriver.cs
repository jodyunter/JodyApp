using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Service.Test.DataFolder.DivisionTestData
{
    public class DivisionTestDataDriver
    {
        public static void DeleteAllData(Database.JodyAppContext db)
        {
            string[] tables = { "ScheduleRules", "Seasons", "Teams", "TeamStatistics", "Divisions" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public static void InsertData(Database.JodyAppContext db)
        {
            Division divLeague = new Division("League", 0, 1, null);
            Division divWest = new Division("West", 1, 2, divLeague);
            Division divEast = new Division("East", 1, 2, divLeague);
            Division divPacific = new Division("Pacific", 2, 1, divWest);
            Division divCentral = new Division("Central", 2, 2, divWest);
            Division divNorthWest = new Division("North West", 2, 3, divWest);
            Division divNorthEast = new Division("North East", 2, 4, divEast);
            Division divAtlantic = new Division("Atlantic", 2, 4, divEast);

            db.Divisions.AddRange(new Division[] { divLeague, divWest, divEast, divPacific, divCentral, divNorthWest, divNorthEast, divAtlantic });

            db.SaveChanges();

            Team LosAngelas = new Team("Los Angelas", 5, divPacific);
            Team Seattle = new Team("Seattle", 5, divPacific);
            Team Vancouver = new Team("Vancouver", 5, divPacific);
            Team Minnesota = new Team("Minnesota", 5, divCentral);
            Team Colorado = new Team("Colorado", 5, divCentral);
            Team Chicago = new Team("Chicago", 5, divCentral);
            Team Edmonton = new Team("Edmonton", 5, divNorthWest);
            Team Calgary = new Team("Calgary", 5, divNorthWest);
            Team Winnipeg = new Team("Winnipeg", 5, divNorthWest);
            Team Toronto = new Team("Toronto", 5, divNorthEast);
            Team Montreal = new Team("Montreal", 5, divNorthEast);
            Team Ottawa = new Team("Ottawa", 5, divNorthEast);
            Team QuebecCity = new Team("Quebec City", 5, divNorthEast);
            Team Boston = new Team("Boston", 5, divAtlantic);
            Team NewYork = new Team("New York", 5, divAtlantic);
            Team Philadelphia = new Team("Philadelphia", 5, divAtlantic);
            Team Detroit = new Team("Detroit", 5, divAtlantic);

            db.Teams.AddRange(new Team[] { LosAngelas, Seattle, Vancouver, Minnesota, Chicago, Edmonton, Calgary, Winnipeg, Toronto, Montreal, Ottawa, QuebecCity, Boston, NewYork, Philadelphia, Detroit });

            db.SaveChanges();

            divPacific.Teams.AddRange(new Team[] { LosAngelas, Seattle, Vancouver });
            divCentral.Teams.AddRange(new Team[] { Minnesota, Chicago, Colorado });
            divNorthWest.Teams.AddRange(new Team[] { Calgary, Edmonton, Winnipeg });
            divNorthEast.Teams.AddRange(new Team[] { Toronto, Montreal, Ottawa, QuebecCity });

            db.SaveChanges();
        }
    }
}

