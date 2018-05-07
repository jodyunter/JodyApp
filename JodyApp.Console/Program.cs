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
            JodyAppContext db = new JodyAppContext(JodyAppContext.WORK_TEST) ;
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);
            LeagueService leagueService = new LeagueService(db);       

            Random random = new Random();            

            League league = db.Leagues.Include("ReferenceCompetitions.Playoff").Where(l => l.Name == "Jody League").First();            
            if (leagueService.IsCurrentYearComplete(league))
            {
                league.CurrentYear++;

                Season referenceSeason = league.ReferenceCompetitions.Where(rc => rc.Order == 1).First().Season;
                Season season = seasonService.CreateNewSeason(referenceSeason, league.CurrentYear);

            }

            Competition c = leagueService.GetNextCompetition(league);
            if (c is Season)
            {
                var nextGames = c.GetNextGames(db);
                c.StartCompetition();
                c.PlayGames(nextGames, random);
                c.IsComplete(db);
                seasonService.SortAllDivisions((Season)c);
                db.SaveChanges();

                var div = db.Divisions.Where(d => d.Season.Id == ((Season)c).Id && d.Name == "League").First();


                var teams = seasonService.GetTeamsInDivisionByRank(div);
                teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

                System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("League", teams.ToList<Team>()));
            }

            Competition p = leagueService.GetNextCompetition(league);
            if (c == null)
            {
                Playoff referencePlayoff = league.ReferenceCompetitions.Where(rc => rc.Order == 2).First().Playoff;
                p = playoffService.CreateNewPlayoff(referencePlayoff, ((Season)c), "Playoffs", league.CurrentYear);
            }

            p.StartCompetition();
            var pGames = new List<Game>();
            while (!p.Complete)
            {

                playoffService.PlayRound((Playoff)p, random).ForEach(g => System.Console.WriteLine(g));
                System.Console.WriteLine("Round " + ((Playoff)p).CurrentRound + ":");
                //((Playoff)p).GetSeriesForRound(((Playoff)p).CurrentRound).ForEach(series => System.Console.WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));
            }
            

            db.SaveChanges();

            System.Console.WriteLine("\n");
            System.Console.WriteLine("Champion List");
            db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == "Final").OrderByDescending(s => s.Playoff.Year).ToList().ForEach(series =>
            {
                System.Console.WriteLine(series.Playoff.Year + " : " + series.GetWinner().Name);
            });


            teamService.SetNewSkills(random);

            db.SaveChanges();

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }

    }
}
