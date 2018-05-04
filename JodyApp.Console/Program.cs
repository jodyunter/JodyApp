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

namespace JodyApp.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            JodyAppContext db = new JodyAppContext();
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);


            Random random = new Random();

            //Array.Sort(teamList, StandingsSorter.SortByDivisionLevel_0);


            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORTY_BY_CONFERENCE));
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORT_BY_DIVISION));

            League league = db.Leagues.Where(l => l.Name == "Jody League").First();
            Season season = seasonService.CreateNewSeason(league, "My Season", 1);

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

            Playoff p = new Playoff() { CurrentRound = 0, Name = "Playoff Test", League = league, Started = true, StartingDay = 15 };


            var leagueDivision = divisionService.GetByName("League", league, season);
            var westDivision = divisionService.GetByName("West", league, season);
            var eastDivision = divisionService.GetByName("East", league, season);
            var centralDivision = divisionService.GetByName("Central", league, season);
            var seriesRule0 = new SeriesRule(league, p, "Quarter Final", 1, "Division Pool", 3, "Division Pool", 4, SeriesRule.TYPE_BEST_OF, 2, false, "1,0,1");
            var seriesRule1 = new SeriesRule(league, p, "Semi Final", 2, "Division Pool", 2, "Quarter Final Winner", 1, SeriesRule.TYPE_BEST_OF, 3, false, "0,1,1,0,1");
            var seriesRule2 = new SeriesRule(league, p, "Final", 3, "Division Pool", 1, "Semi Final Winner", 1, SeriesRule.TYPE_BEST_OF, 4, false, "1,1,0,0,1,0,0");
            p.Series = new List<Series>()
            {
                new Series(p, seriesRule0, null, null, new List<Game>(), "Quarter Final"),
                new Series(p, seriesRule1, null, null, new List<Game>(), "Semi Final"),
                new Series(p, seriesRule2, null, null, new List<Game>(), "Final")
            };

            p.GroupRules = new List<GroupRule>
            {
                GroupRule.CreateFromDivision(league, "Division Pool",leagueDivision, eastDivision, 1, 2),
                GroupRule.CreateFromDivision(league, "Division Pool",leagueDivision, westDivision, 1, 1),
                GroupRule.CreateFromDivision(league, "Division Pool",leagueDivision, centralDivision, 1, 1),                
                GroupRule.CreateFromSeriesWinner(league, "Semi Final Winner", p.GetSeriesByName("Semi Final"), null),
                GroupRule.CreateFromSeriesWinner(league, "Quarter Final Winner", p.GetSeriesByName("Quarter Final"), null)
            };


            db.Playoffs.Add(p);
            db.SaveChanges();

           
            db.SaveChanges();

            var pGames = new List<Game>();
            while (!p.Complete)
            {
                PlayRound(p, pGames, random, db);                
            }
            

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
                    System.Console.WriteLine(game.Series.Name + ": " + game);
                });
            }

            p.NextRound();
            db.SaveChanges();

        }
    }
}
