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
using JodyApp.Service.Test.DataFolder;

using JodyApp.Domain.Config;

namespace JodyApp.Service.Test
{

    [TestClass]
    public class SeasonServiceTests
    {     
        JodyAppContext db;
        SeasonService service;
        ScheduleService scheduleService;        
        SeasontestDataDriver driver;
        TeamService teamService;
        League league;

        [TestInitialize]
        public void Setup()
        {
            driver = new SeasontestDataDriver();
            db = driver.db;
            service = new SeasonService(db);
            scheduleService = new ScheduleService(db);
            
            teamService = new TeamService(db);
            driver.DeleteAllData();
            driver.InsertData();
            league = db.Leagues.Where(l => l.Name == driver.LeagueName).First();
        }

        [TestMethod]
        public void ShouldCreateSecondSeason()
        {
            var refSeason = db.Seasons.Where(s => s.Name == "My Season").First();
            Season season = service.CreateNewSeason(refSeason, 1);
            Random random = new Random(55555);

            season.SetupStandings();

            List<Game> scheduleGames = season.Games;

            scheduleGames.ForEach(game =>
            {
                game.Play(random);
                season.Standings.ProcessGame(game);
            });

            db.SaveChanges();
            
            Season season2 = service.CreateNewSeason(refSeason, 2);

            season2.SetupStandings();

            db.SaveChanges();

            //need to get counts based on other things.
            AreEqual(6, teamService.GetTeamsBySeason(season).Count);
            AreEqual(6, teamService.GetTeamsBySeason(season2).Count);
            AreEqual(18, db.Teams.Count());
            AreEqual(6, teamService.GetBaseTeams().Count);

            db.SaveChanges();
        }

        [TestMethod]
        public void ShouldScheduleInOrder()
        {
            var refSeason = db.Seasons.Where(s => s.Name == "My Season").First();
            Season season = service.CreateNewSeason(refSeason, 1);
            Random random = new Random(55555);

            season.SetupStandings();

            var scheduleGames = season.Games;
            int count = 1;

            scheduleGames.OrderBy(s => s.GameNumber).ToList().ForEach(game =>
            {
                if (count < 61)
                {
                    AreNotEqual("Toronto", game.HomeTeam.Name);
                }
                else
                {
                    AreEqual("Toronto", game.HomeTeam.Name);
                }

                count++;

            });

            AreEqual("Toronto", scheduleGames[scheduleGames.Count - 1].HomeTeam.Name);
        }

    }
}
