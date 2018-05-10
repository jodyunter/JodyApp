using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Service.Test.DataFolder.DivisionTestData;
using JodyApp.Domain;
using JodyApp.Domain.Schedule;
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
        Season season;
        League league;

        [TestInitialize]
        public void Setup()
        {
            db = new Database.JodyAppContext(Database.JodyAppContext.CURRENT_DATABASE);
            driver = new DivisionTestDataDriver();
            service = new DivisionService(driver.db);
            
            driver.DeleteAllData();
            driver.InsertData();
            scheduleService = new ScheduleService(db);
            league = db.Leagues.Where(l => l.Name == driver.LeagueName).First();
            season = league.ReferenceCompetitions.Where(s => s.League.Id == league.Id && s.Season.Name == "My Season").First().Season;
            seasonService = new SeasonService(db);            

        }
 
        [TestMethod]
        public void ShouldGetSeasonDivisionsByParent()
        {
            var refSeason = db.Seasons.Where(s => s.Name == "My Season").First();
            season = seasonService.CreateNewSeason(refSeason, 15);
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

            var refSeason = db.Seasons.Where(s => s.Name == "My Season").First();
            Season season = seasonService.CreateNewSeason(refSeason, 2);            
            Random random = new Random(15);

            season.SetupStandings();

            List<Game> scheduleGames = season.Games;

            scheduleGames.ForEach(game =>
            {
                game.Play(random);
                season.Standings.ProcessGame(game);
            });

            db.SaveChanges();


            Division seasonDivision = service.GetByName("West", league, season);

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
            AreEqual(1, service.GetDivisionsByParent(service.GetByName("Extra Top", db.Leagues.Where(l => l.Name.Equals("Extra")).First(), season)).Count);            
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
            AreEqual(2, service.GetDivisionsByLevel(0, season).Count);
            AreEqual(3, service.GetDivisionsByLevel(1, season).Count);
            AreEqual(5, service.GetDivisionsByLevel(2, season).Count);
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
