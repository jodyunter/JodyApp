using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Service.Test.DataFolder;
using JodyApp.Domain;

using JodyApp.Domain.Table.Display;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class DivisionServiceTests
    {
        Database.JodyAppContext db;
        DivisionService service;
        DivisionTestDataDriver driver;
        SeasonService seasonService;
        ScheduleService scheduleService;
        LeagueService leagueService;
        CompetitionService competitionService;
        DivisionService divisionService;
        ConfigService configService;
        PlayoffService playoffService;
        League league;
        Season season;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext(Database.JodyAppContext.CURRENT_DATABASE);
            driver = new DivisionTestDataDriver();
            service = new DivisionService(driver.db);
            
            driver.DeleteAllData();
            driver.InsertData();
            leagueService = new LeagueService(db);
            divisionService = new DivisionService(db);
            configService = new ConfigService(db, leagueService);            
            competitionService = new CompetitionService(db, leagueService, seasonService, playoffService);
            scheduleService = new ScheduleService(db, divisionService);
            seasonService = new SeasonService(db, configService, divisionService, scheduleService);
            
            league = leagueService.GetByName(driver.LeagueName);
            season = seasonService.GetSeason(league, "My Season", 1);
        }
 
        [TestMethod]
        public void ShouldGetSeasonDivisionsByParent()
        {
            Division leagueDiv = service.GetByName("League", league, season);
            List<Division> divisions = service.GetDivisionsByParent(leagueDiv);

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
            IsNotNull(divisions[1].Season);
        }

        [TestMethod]
        public void ShouldSortByDivision()
        {

            var division = db.Divisions.Where(d => d.Name == "West").FirstOrDefault();
            

            var rank = service.SortByDivision(division);

            string result = RecordTableDisplay.GetRecordTableRowHeader();
            rank.ForEach(record =>
            {
                result += "\r\n" + RecordTableDisplay.GetRecordTableRow(record);
            }
            );

            db.SaveChanges();
            AreEqual(SHOULDSORTBYDIVISION_EXPECTED, result);              

        }
        [TestMethod]
        public void ShouldGetAllTeamsInDivision()
        {
            var extraLeague = leagueService.GetByName("Extra");
            var extraSeason = seasonService.GetSeason(extraLeague, "Extra Season", 1);
            var leagueDiv = service.GetByName("League", league, season);
            var east = service.GetByName("East", league, season);
            var west = service.GetByName("West", league, season);
            var pacific = service.GetByName("Pacific", league, season);
            var central = service.GetByName("Central", league, season);
            var northWest = service.GetByName("North West", league, season);
            var northEast = service.GetByName("North East", league, season);
            var atlantic = service.GetByName("Atlantic", league, season);
            var extraTop = service.GetByName("Extra Top", extraLeague, extraSeason);
            var extraChild = service.GetByName("Extra Child", extraLeague, extraSeason);


            AreEqual(2, service.GetAllTeamsInDivision(extraTop).Count);
            AreEqual(2, service.GetAllTeamsInDivision(extraChild).Count);
            AreEqual(17, service.GetAllTeamsInDivision(leagueDiv).Count);
            AreEqual(8, service.GetAllTeamsInDivision(east).Count);
            AreEqual(9, service.GetAllTeamsInDivision(west).Count);
            AreEqual(3, service.GetAllTeamsInDivision(pacific).Count);
            AreEqual(3, service.GetAllTeamsInDivision(central).Count);
            AreEqual(3, service.GetAllTeamsInDivision(northWest).Count);
            AreEqual(4, service.GetAllTeamsInDivision(northEast).Count);
            AreEqual(4, service.GetAllTeamsInDivision(atlantic).Count);

        }
        [TestMethod]
        public void ShouldGetDivisionsByLeague()
        {            
            var divisions = service.GetByLeague(league);

            divisions.ForEach(div =>
            {
                AreNotEqual("Extra Child", div.Name);
                AreNotEqual("Extra Top", div.Name);
            });

            divisions = service.GetByLeague(db.Leagues.Where(l => l.Name == "Extra").First());

            divisions.ForEach(div =>
            {
                IsTrue(div.Name.Equals("Extra Child") || div.Name.Equals("Extra Top"));
            });

        }
        [TestMethod]
        public void ShouldGetByName()
        {
            Division d = service.GetByName( "West", league, season);

            IsNotNull(d);
            IsNotNull(d.Id);
            AreEqual(d.Name, "West");

        }

        [TestMethod]
        public void ShouldGetDivisionsByParent()
        {
            AreEqual(2, service.GetDivisionsByParent(service.GetByName("League", league, season)).Count);
            AreEqual(3, service.GetDivisionsByParent(service.GetByName("West", league, season)).Count);
            AreEqual(2, service.GetDivisionsByParent(service.GetByName("East", league, season)).Count);
            var extraLeague = leagueService.GetByName("Extra");
            var extraSeason = seasonService.GetSeason(extraLeague, "Extra Season", 1);
            AreEqual(1, service.GetDivisionsByParent(service.GetByName("Extra Top", extraLeague, extraSeason)).Count);            
        }

        [TestMethod]
        public void ShouldGetTeamsInDivision()
        {
            List<Team> teams = service.GetAllTeamsInDivision(service.GetByName("League", league, season));

            AreEqual(17, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("East", league, season));

            AreEqual(8, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("West", league, season));

            AreEqual(9, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Atlantic", league, season));

            AreEqual(4, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Central", league, season));

            AreEqual(3, teams.Count);
        }

        [TestMethod]
        public void ShouldGetDivisionsByLevel()
        {
            AreEqual(1, service.GetDivisionsByLevel(0, season).Count);
            AreEqual(2, service.GetDivisionsByLevel(1, season).Count);
            AreEqual(5, service.GetDivisionsByLevel(2, season).Count);
        }

        [TestMethod]
        public void ShouldCreateDivisionNoParent()
        {           
            var newDivision = service.CreateDivision(null, null, null, "Division 1", "Div 1", 1, 1, null);
            service.Save();

            var division = service.GetById((int)newDivision.Id);

            AreEqual(division, newDivision);
            
        }

        private static string SHOULDSORTBYDIVISION_EXPECTED =
@"R    Name               W    L    T  Pts   GP   GF   GA   GD            Div
1    Chicago           34   20   10   78   64  178   47  131        Central
2    Vancouver         30   20   10   70   60  175  128   47        Pacific
3    Winnipeg          29   23   12   70   64  161  160    1     North West
4    Colorado          30   26    8   68   64  168  184  -16        Central
5    Calgary           26   23   15   67   64  162  169   -7     North West
6    Edmonton          23   29   12   58   64  159  169  -10     North West
7    Minnesota         21   29   14   56   64  134  158  -24        Central
8    Seattle           23   34    7   53   64  165  179  -14        Pacific
9    Los Angelas       18   32   14   50   64  156  186  -30        Pacific";
    }
}
