using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Service;
using JodyApp.Domain.Table;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder.Jody;

using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table.Display;
using JodyApp.Domain.Playoffs;
using JodyApp.Domain.Playoffs.Display;

namespace JodyApp.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            string prod = "Jody";
            string test = "JodyTest";
            JodyAppContext db = new JodyAppContext(test) ;
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            //driver.DeleteAllData();
            //driver.InsertData();
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);


            int year = 0;
            if (db.Seasons.ToList().Count <= 0)
            {
                year = 0;
            } 
            else
            {
                year = db.Seasons.Max(s => s.Year);
            }

            year++;

            Random random = new Random();            

            League league = db.Leagues.Where(l => l.Name == "Jody League").First();
            Season season = seasonService.CreateNewSeason(league, "My Season", year);

            season.SetupStandings();

            season.Games.ForEach(game =>
           {
               game.Play(random);
               season.Standings.ProcessGame(game);
           });

            seasonService.SortAllDivisions(season);
            db.SaveChanges();

            var div = db.Divisions.Where(d => d.Season.Id == season.Id && d.Name == "League").First();



            var teams = seasonService.GetTeamsInDivisionByRank(div);
            teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

            System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("League", teams.ToList<Team>()));

            Playoff p = playoffService.CreateNewPlayoff(league, season, "Playoff", year);

            var pGames = new List<Game>();
            while (!p.Complete)
            {
                PlayRound(p, pGames, random, db);                
            }
            

            db.SaveChanges();

            System.Console.WriteLine("\n");
            System.Console.WriteLine("Champion List");
            db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == "Final").ToList().ForEach(series =>
            {
                System.Console.WriteLine(series.Playoff.Year + " : " + series.GetWinner().Name);
            });
                        

            teamService.GetBaseTeams().ForEach(team =>
            {
                int num = random.Next(0, 9);
                if (num < 2) team.Skill += 1;
                if (num > 7) team.Skill += 1;
                if (team.Skill > 10) team.Skill = 10;
                if (team.Skill < 1) team.Skill = 1;

            });

            db.SaveChanges();

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }

        static void PlayRound(Playoff p, List<Game> pGames, Random random, JodyAppContext db)
        {
            if (!p.Started)
            {
                p.NextRound();
                db.SaveChanges();
            }

            System.Console.WriteLine("Round " + p.CurrentRound + ":");                 
            while (!p.IsRoundComplete(p.CurrentRound))
            {
                pGames.AddRange(p.GetNextGamesForRound(p.CurrentRound, 0));

                pGames.Where(g => !g.Complete).ToList().ForEach(game =>
                {
                    game.Play(random);
                });
            }

            p.GetSeriesForRound(p.CurrentRound).ForEach(series => System.Console.WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));

            p.NextRound();
            db.SaveChanges();

        }
    }
}
