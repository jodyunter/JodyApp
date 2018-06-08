using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Service.Test
{
    public class DivisionTestDataDriver:SimpleTestDataDriver
    {
        public string LeagueName = "Division Test Data League";

        League MyLeague, ExtraLeague;
        Season Season, ExtraSeason;
        public DivisionTestDataDriver() : base() { }

        public override void PrivateCreateSeasons()
        {
            Season = CreateAndAddSeason(MyLeague, "My Season", 1, 0);
            ExtraSeason = CreateAndAddSeason(ExtraLeague, "Extra Season", 1, 0);
        }


        public override void PrivateCreateLeagues()
        {            

            MyLeague = CreateAndAddLeague(this.LeagueName);
            ExtraLeague = CreateAndAddLeague("Extra");
        }
        public override void PrivateCreateDivisions()
        {            
            CreateAndAddDivision(GetConfigDivision("League"), MyLeague, Season, "League", null, 0, 1, null, null);
            CreateAndAddDivision(GetConfigDivision("West"), MyLeague, Season, "West", null, 1, 2, divisions["League"], null);
            CreateAndAddDivision(GetConfigDivision("East"), MyLeague, Season, "East", null, 1, 2, divisions["League"], null);
            CreateAndAddDivision(GetConfigDivision("Pacific"), MyLeague, Season, "Pacific", null, 2, 1, divisions["West"], null);
            CreateAndAddDivision(GetConfigDivision("Central"), MyLeague, Season, "Central", null, 2, 2, divisions["West"], null);
            CreateAndAddDivision(GetConfigDivision("North West"), MyLeague, Season, "North West", null, 2, 3, divisions["West"], null);
            CreateAndAddDivision(GetConfigDivision("North East"), MyLeague, Season, "North East", null, 2, 4, divisions["East"], null);
            CreateAndAddDivision(GetConfigDivision("Atlantic"), MyLeague, Season, "Atlantic", null, 2, 4, divisions["East"], null);

            CreateAndAddDivision(GetConfigDivision("Extra Top"), ExtraLeague, ExtraSeason, "Extra Top", null, 0, 1, null, null);
            CreateAndAddDivision(GetConfigDivision("Extra Child"), ExtraLeague, ExtraSeason, "Extra Child", null, 1, 2, divisions["Extra Top"], null);


        }

        public override void PrivateCreateTeams()
        {            
            CreateAndAddTeam("Los Angelas", 5, new TeamStatistics(-1, 18, 32, 14, 156, 186), divisions["Pacific"]);
            CreateAndAddTeam("Seattle", 5, new TeamStatistics(-1, 23, 34, 7, 165, 179), divisions["Pacific"]);
            CreateAndAddTeam("Vancouver", 5, new TeamStatistics(-1, 30, 20, 10, 175, 128), divisions["Pacific"]);
            CreateAndAddTeam("Minnesota", 5, new TeamStatistics(-1, 21, 29, 14, 134, 158), divisions["Central"]);
            CreateAndAddTeam("Colorado", 5, new TeamStatistics(-1, 30, 26, 8, 168, 184), divisions["Central"]);
            CreateAndAddTeam("Chicago", 5, new TeamStatistics(-1, 34, 20, 10, 178, 47), divisions["Central"]);
            CreateAndAddTeam("Edmonton", 5, new TeamStatistics(-1, 23, 29, 12, 159, 169), divisions["North West"]);
            CreateAndAddTeam("Calgary", 5, new TeamStatistics(-1, 26, 23, 15, 162, 169), divisions["North West"]);
            CreateAndAddTeam("Winnipeg", 5, new TeamStatistics(-1, 29, 23, 12, 161, 160), divisions["North West"]);
            CreateAndAddTeam("Toronto", 5, new TeamStatistics(), divisions["North East"]);
            CreateAndAddTeam("Montreal", 5, new TeamStatistics(), divisions["North East"]);
            CreateAndAddTeam("Ottawa", 5, new TeamStatistics(), divisions["North East"]);
            CreateAndAddTeam("Quebec City", 5, new TeamStatistics(), divisions["North East"]);
            CreateAndAddTeam("Boston", 5, new TeamStatistics(), divisions["Atlantic"]);
            CreateAndAddTeam("New York", 5, new TeamStatistics(), divisions["Atlantic"]);
            CreateAndAddTeam("Philadelphia", 5, new TeamStatistics(), divisions["Atlantic"]);
            CreateAndAddTeam("Detroit", 5, new TeamStatistics(), divisions["Atlantic"]);

            CreateAndAddTeam("Pittsburgh", 5, new TeamStatistics(), divisions["Extra Child"]);
            CreateAndAddTeam("Minneapolis", 5, new TeamStatistics(), divisions["Extra Child"]);


        }
    }
}

