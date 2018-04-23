using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder.SeasonTestData;

using JodyApp.Domain.Schedule;

namespace JodyApp.Service.Test
{

    [TestClass]
    public class SeasonServiceTests
    {     
        JodyAppContext db;
        SeasonService service;
        ScheduleService scheduleService;        
        SeasontestDataDriver driver;
        League league;

        [TestInitialize]
        public void Setup()
        {
            db = new JodyAppContext();
            service = new SeasonService(db);
            scheduleService = new ScheduleService(db);
            driver = new SeasontestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            league = db.Leagues.Where(l => l.Name == driver.LeagueName).First();
        }

        [TestMethod]
        public void ShouldCreateSecondSeason()
        {

            Season season = service.CreateNewSeason(league, "My Season", 1);
            Random random = new Random(55555);

            season.SetupStandings();

            List<Game> scheduleGames = scheduleService.CreateGamesFromRules(season.ScheduleRules);

            scheduleGames.ForEach(game =>
            {
                game.Play(random);
                season.Standings.ProcessGame(game);
            });

            db.SaveChanges();

            Season season2 = service.CreateNewSeason(league, "Season 2", 2);

            season2.SetupStandings();

            db.SaveChanges();

            //need to get counts based on other things.
            AreEqual(4, db.Teams.Count());
            AreEqual(8, db.Teams.Count());

            db.SaveChanges();
        }

    }
}
