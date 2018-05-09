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
            JodyAppContext db = new JodyAppContext(JodyAppContext.WORK_TEST);
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            //driver.DeleteAllData();
            //driver.InsertData();
            driver.UpdateData();
            

            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);
            LeagueService leagueService = new LeagueService(db);       

            Random random = new Random();
            int lastGameNumber = 0;

            League league = db.Leagues.Include("ReferenceCompetitions.Playoff").Where(l => l.Name == "Jody League").First();            
            if (leagueService.IsYearDone(league))
            {
                league.CurrentYear++;

            }
                        
            
            while (!leagueService.IsYearDone(league))
            {
                Competition c = leagueService.GetNextCompetition(league);

                if (!c.Started) c.StartCompetition();
                while (!c.Complete)
                {                    
                    var nextGames = c.GetNextGames(lastGameNumber);
                    c.PlayGames(nextGames, random);
                    c.IsComplete();
                }

                if (c is Season)
                {
                    seasonService.SortAllDivisions((Season)c);


                    var div = db.Divisions.Where(d => d.Season.Id == ((Season)c).Id && d.Name == "League").First();


                    var teams = seasonService.GetTeamsInDivisionByRank(div);
                    teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

                    System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("Premier", teams.Where(t => t.Division.Name == "Premier").ToList()));
                    System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("Division1", teams.Where(t => t.Division.Name == "Division1").ToList()));
                }
                else if (c is Playoff)
                {
                    for (int i = 1; i <= ((Playoff)c).CurrentRound; i++)
                    {
                        System.Console.WriteLine("Round " + i + ":");
                        ((Playoff)c).GetSeriesForRound(i).ForEach(series => System.Console.WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));
                    }
                }

                db.SaveChanges();
                
            }



            teamService.SetNewSkills(random);
            
            var promotionSeries = playoffService.GetSeriesByYear("Qualification", league.CurrentYear);

            Team Promoted = promotionSeries.GetWinner().Parent;
            Team Relegated = promotionSeries.GetLoser().Parent;

            teamService.ChangeDivision(Promoted, "Premier");
            teamService.ChangeDivision(Relegated, "Division1");

            
            db.SaveChanges();

            var playoffWinners = new Dictionary<int, string>();
            var playoffLosers = new Dictionary<int, string>();

            playoffService.GetSeries("Final").OrderBy(s => s.Playoff.Year).ToList().ForEach(series =>
            {
                playoffWinners.Add(series.Playoff.Year, series.GetWinner().Name);
                playoffLosers.Add(series.Playoff.Year, series.GetLoser().Name);
            });

            var divisionRanks = db.DivisionRanks
                .Include("Division.Season")
                .Include("Team")
                .Where(dr => dr.Division.Name == "League" && dr.Rank == 1).ToDictionary(dr => dr.Division.Season.Year, dr => dr.Team.Name);

            divisionRanks.OrderByDescending(m => m.Key);

            string formatter = "{0,3}{1,12}{2,12}{3,12}{4,12}{5,12}";
            System.Console.WriteLine("\n");
            System.Console.WriteLine(String.Format(formatter, "Yr", "Champion", "Runner-Up", "Season", "Promotoed", "Relegated"));

            var qualificationSeries = playoffService.GetSeries("Qualification");

            for (int i = playoffWinners.Count; i > 0; i--)
            {
                var promoted = qualificationSeries.Where(s => s.Playoff.Year == i).FirstOrDefault();
                System.Console.WriteLine(String.Format(formatter, i, playoffWinners[i], playoffLosers[i], divisionRanks[i],
                    promoted != null ? promoted.GetWinner().Name : "",
                    promoted != null ? promoted.GetLoser().Name : ""));
            }


            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }

    }
}
