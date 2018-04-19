using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Service.Test.DataFolder.DivisionTestData;
using JodyApp.Domain;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table.Display;

namespace JodyApp.Service.Test
{
    [TestClass]
    public class DivisionServiceTests
    {

        Database.JodyAppContext db = new Database.JodyAppContext();
        DivisionService service;
        DivisionTestDataDriver driver;
        SeasonService seasonService;
        ScheduleService scheduleService;
        Season season;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext();
            service = new DivisionService(db);
            driver = new DivisionTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            seasonService = new SeasonService(db);
            season = seasonService.CreateNewSeason("Season Test", 15);
            scheduleService = new ScheduleService(db);
        }
        //these tests depend on specific data
        [TestMethod]
        public void ShouldGetDivisionsByParent()
        {                        
            List<Division> divisions = service.GetDivisionsByParent(service.GetByName("League"));

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
        }

        [TestMethod]
        public void ShouldGetTeamsInDivision()
        {            
            List<Team> teams = service.GetAllTeamsInDivision(service.GetByName("League"));

            AreEqual(17, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("East"));

            AreEqual(8, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("West"));

            AreEqual(9, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Atlantic"));

            AreEqual(4, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Central"));

            AreEqual(3, teams.Count);
        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel0()
        {
            List<Division> divs = service.GetDivisionsByLevel(0);

            AreEqual(1, divs.Count);
            AreEqual("League", divs[0].Name);

        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel0ByDivision()
        {
            Division league = service.GetByName("League");
            List<Division> divs = service.GetDivisionsByLevel(league);

            AreEqual(1, divs.Count);
            AreEqual("League", divs[0].Name);

        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel1()
        {
            List<Division> divs = service.GetDivisionsByLevel(1);

            AreEqual(2, divs.Count);
            AreEqual("West", divs[0].Name);
            AreEqual("East", divs[1].Name);

        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel1ByDivision()
        {
            Division east = service.GetByName("East");
            List<Division> divs = service.GetDivisionsByLevel(east);

            AreEqual(2, divs.Count);
            AreEqual("West", divs[0].Name);
            AreEqual("East", divs[1].Name);
        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel2()
        {
            List<Division> divs = service.GetDivisionsByLevel(2);

            AreEqual(5, divs.Count);
            AreEqual("Pacific", divs[0].Name);
            AreEqual("Central", divs[1].Name);
            AreEqual("North West", divs[2].Name);
            AreEqual("North East", divs[3].Name);
            AreEqual("Atlantic", divs[4].Name);            
        }

        [TestMethod]
        public void ShouldGetDivisionsAtLevel2ByDivision()
        {
            Division northeast = service.GetByName("North East");
            List<Division> divs = service.GetDivisionsByLevel(northeast);

            AreEqual(5, divs.Count);
            AreEqual("Pacific", divs[0].Name);
            AreEqual("Central", divs[1].Name);
            AreEqual("North West", divs[2].Name);
            AreEqual("North East", divs[3].Name);
            AreEqual("Atlantic", divs[4].Name);
        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsByParent()
        {
            List<Division> divisions = service.GetDivisionsByParent(service.GetByName("League", season));

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
        }

        [TestMethod]
        public void ShouldGetSeasonTeamsInSeasonDivision()
        {
            List<Team> teams = service.GetAllTeamsInDivision(service.GetByName("League", season));

            AreEqual(17, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("East", season));

            AreEqual(8, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("West", season));

            AreEqual(9, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Atlantic", season));

            AreEqual(4, teams.Count);

            teams = service.GetAllTeamsInDivision(service.GetByName("Central", season));

            AreEqual(3, teams.Count);
        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel0()
        {
            List<Division> divs = service.GetDivisionsByLevel(0, season);

            AreEqual(1, divs.Count);
            AreEqual("League", divs[0].Name);

        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel0BySeasonDivision()
        {
            Division league = service.GetByName("League", season);
            List<Division> divs = service.GetDivisionsByLevel(league);

            AreEqual(1, divs.Count);
            AreEqual("League", divs[0].Name);

        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel1()
        {
            List<Division> divs = service.GetDivisionsByLevel(1, season);

            AreEqual(2, divs.Count);
            AreEqual("West", divs[0].Name);
            AreEqual("East", divs[1].Name);

        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel1BySeasonDivision()
        {
            Division east = service.GetByName("East", season);
            List<Division> divs = service.GetDivisionsByLevel(east);

            AreEqual(2, divs.Count);
            AreEqual("West", divs[0].Name);
            AreEqual("East", divs[1].Name);
        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel2()
        {
            List<Division> divs = service.GetDivisionsByLevel(2, season);

            AreEqual(5, divs.Count);
            AreEqual("Pacific", divs[0].Name);
            AreEqual("Central", divs[1].Name);
            AreEqual("North West", divs[2].Name);
            AreEqual("North East", divs[3].Name);
            AreEqual("Atlantic", divs[4].Name);
        }

        [TestMethod]
        public void ShouldGetSeasonDivisionsAtLevel2BySeasonDivision()
        {
            Division northeast = service.GetByName("North East", season);
            List<Division> divs = service.GetDivisionsByLevel(northeast);

            AreEqual(5, divs.Count);
            AreEqual("Pacific", divs[0].Name);
            AreEqual("Central", divs[1].Name);
            AreEqual("North West", divs[2].Name);
            AreEqual("North East", divs[3].Name);
            AreEqual("Atlantic", divs[4].Name);
        }

        [TestMethod]
        public void ShouldSortByDivision()
        {
            //create sesaon
            Season season = seasonService.CreateNewSeason("Season Testing", 2);
            season.SetupStandings();
            Random random = new Random(15);
            List<ScheduleGame> scheduleGames = scheduleService.CreateGamesFromRules(season.ScheduleRules);

            scheduleGames.ForEach(game =>
            {
                game.Play(random);
                season.Standings.ProcessGame(game);
            });

            db.SaveChanges();

            SeasonDivision seasonDivision = (SeasonDivision)service.GetByName("West", season);

            var rank = service.SortByDivision(seasonDivision);

            string result = RecordTableDisplay.GetRecordTableRowHeader();
            rank.ForEach(record => { result += "\n" + RecordTableDisplay.GetRecordTableRow(record); });

            db.SaveChanges();
            AreEqual(SHOULDSORTBYDIVISION_EXPECTED, result);            


        }

        private static string SHOULDSORTBYDIVISION_EXPECTED =
@"R    Name               W    L    T  Pts   GP   GF   GA   GD            Div
1    Chicago           34   20   10   78   64  175  128   47        Central
2    Vancouver         29   23   12   70   64  161  160    1        Pacific
3    Winnipeg          30   25    9   69   64  159  139   20     North West
4    Colorado          30   26    8   68   64  168  184  -16        Central
5    Calgary           26   23   15   67   64  162  169   -7     North West
6    Edmonton          23   29   12   58   64  159  169  -10     North West
7    Minnesota         21   29   14   56   64  134  158  -24        Central
8    Seattle           23   34    7   53   64  165  179  -14        Pacific
9    Los Angelas       18   32   14   50   64  156  186  -30        Pacific";
    }
}
