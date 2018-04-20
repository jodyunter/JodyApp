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
using JodyApp.Domain.Config;

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
            Division league = new ConfigDivision() { Name = "League" }.GetByName(db);
            List<Division> divisions = league.GetDivisionsByParent(db);            

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
        }
 
        [TestMethod]
        public void ShouldGetSeasonDivisionsByParent()
        {
            Division league = new ConfigDivision() { Name = "League" }.GetByName(db);
            List<Division> divisions = league.GetDivisionsByParent(db);

            AreEqual(2, divisions.Count);
            AreEqual("East", divisions[1].Name);
            AreEqual("West", divisions[0].Name);
        }

        [TestMethod]
        public void ShouldSortByDivision()
        {

            Season season = seasonService.CreateNewSeason("Season Testing", 2);            
            Random random = new Random(15);

            season.SetupStandings();

            List<ScheduleGame> scheduleGames = scheduleService.CreateGamesFromRules(season.ScheduleRules);

            scheduleGames.ForEach(game =>
            {
                game.Play(random);
                season.Standings.ProcessGame(game);
            });

            db.SaveChanges();
            
            
            SeasonDivision seasonDivision = (SeasonDivision)(new SeasonDivision() { Name = "West", Season = season }.GetByName(db));

            var rank = service.SortByDivision(seasonDivision);

            string result = RecordTableDisplay.GetRecordTableRowHeader();
            rank.ForEach(record =>
            {
                result += "\r\n" + RecordTableDisplay.GetRecordTableRow(record);
            }
            );

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
