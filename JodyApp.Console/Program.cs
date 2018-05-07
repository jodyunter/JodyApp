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
            JodyAppContext db = new JodyAppContext(JodyAppContext.WORK_PROD) ;
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            //driver.DeleteAllData();
            //driver.InsertData();
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);
            LeagueService leagueService = new LeagueService(db);       

            Random random = new Random();
            int lastGameNumber = 0;

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
                var nextGames = c.GetNextGames(lastGameNumber);
                c.StartCompetition();
                c.PlayGames(nextGames, random);
                c.IsComplete();
                seasonService.SortAllDivisions((Season)c);
               

                var div = db.Divisions.Where(d => d.Season.Id == ((Season)c).Id && d.Name == "League").First();


                var teams = seasonService.GetTeamsInDivisionByRank(div);
                teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

                System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("League", teams.ToList<Team>()));
            }

            Competition p = leagueService.GetNextCompetition(league);
            if (p == null)
            {
                Playoff referencePlayoff = league.ReferenceCompetitions.Where(rc => rc.Order == 2).First().Playoff;
                p = playoffService.CreateNewPlayoff(referencePlayoff, ((Season)c), "Playoffs", league.CurrentYear);
            }

            if (p is Playoff)
            {
                p.StartCompetition();                
                while (!p.Complete)
                {                                  
                    var pGames = p.GetNextGames(lastGameNumber);
                    p.PlayGames(pGames, random);         
                    
                    p.IsComplete();
                }

                for (int i = 1; i <= ((Playoff)p).CurrentRound; i++)
                {
                    System.Console.WriteLine("Round " + i + ":");
                    ((Playoff)p).GetSeriesForRound(i).ForEach(series => System.Console.WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));                    
                }

            }


            db.SaveChanges();


            teamService.SetNewSkills(random);

            db.SaveChanges();

            var playoffWinners = new Dictionary<int, string>();
            var playoffLosers = new Dictionary<int, string>();

            db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == "Final").OrderByDescending(s => s.Playoff.Year).ToList().ForEach(series =>
            {
                playoffWinners.Add(series.Playoff.Year, series.GetWinner().Name);
                playoffLosers.Add(series.Playoff.Year, series.GetLoser().Name);
            });

            var divisionRanks = db.DivisionRanks
                .Include("Division.Season")
                .Include("Team")
                .Where(dr => dr.Division.Name == "League" && dr.Rank == 1).ToDictionary(dr => dr.Division.Season.Year, dr => dr.Team.Name);

            divisionRanks.OrderByDescending(m => m.Key);

            string formatter = "{0,3}{1,12}{2,12}{3,12}";
            System.Console.WriteLine("\n");
            System.Console.WriteLine(String.Format(formatter, "Yr", "Champion", "Runner-Up", "Season"));
            
            for (int i = playoffWinners.Count; i > 0; i--)
            {
                System.Console.WriteLine(String.Format(formatter, i, playoffWinners[i], playoffLosers[i], divisionRanks[i]));
            }


            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }

    }
}
