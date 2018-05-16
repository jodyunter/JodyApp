using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Service;
using JodyApp.Domain.Table;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder;

using JodyApp.Domain.Config;
using JodyApp.Domain.Table.Display;
using JodyApp.Domain.Playoffs;
using JodyApp.Domain.Playoffs.Display;
using JodyApp.Console.Views;
using JodyApp.ViewModel;
using JodyApp.Console.Controllers;
using static System.Console;

namespace JodyApp.Console
{
    class Program
    {
        static string LeagueName = "Jody's League";

        static void UpdateTeams(JodyAppContext db, League league, Random random)
        {
            ConfigService configService = new ConfigService(db);
            configService.SetNewSkills(league, random);
            configService.Save();
        }
        static void SetupConfig(JodyAppContext db)
        {
            League League;
            ConfigCompetition CCSeason, CCPlayoff;
            ConfigTeam CTEdmonton, CTCalgary, CTVancouver;
            ConfigDivision CDLeague, CDWest;
            
            SimpleTestDataDriver driver = new SimpleTestDataDriver();

            driver.DeleteAllData();

            LeagueService leagueService = new LeagueService(db);
            ConfigService configService = new ConfigService(db);
            League = leagueService.CreateLeague(LeagueName);
            CCSeason = configService.CreateCompetition(League, "Regular Season", ConfigCompetition.SEASON, null, 1, 1, null);
            //CCPlayoff = configService.CreateCompetition(League, "Playoffs", ConfigCompetition.PLAYOFF, CCSeason, 2, 1, null);



            CTEdmonton = configService.CreateTeam("Edmonton", 5, null, League, 1, null);
            CTCalgary = configService.CreateTeam("Calgary", 5, null, League, 1, null);
            CTVancouver = configService.CreateTeam("Vancouver", 5, null, League, 1, null);

            CDLeague = configService.CreateDivision(League, CCSeason, "League", null, 1, 1, null, 1, null);
            CDWest = configService.CreateDivision(League, CCSeason, "Western Conference", "W. Conf", 2, 2, CDLeague, 1, null);

            CDWest.Teams.Add(CTEdmonton);
            CDWest.Teams.Add(CTCalgary);
            CDWest.Teams.Add(CTVancouver);

            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 1", CDWest, true, 5, 1, false);

            configService.Save();
        }
        
        static void AddTeam(string name, int skill, string divisionName, int? startYear, int? lastYear, ConfigService configService, LeagueService leagueService)
        {
            var League = leagueService.GetByName(LeagueName);
            var CDDivision = configService.GetDivisionByName(League, divisionName);
            var CTNewTeam = configService.CreateTeam("Winnipeg", 5, CDDivision, League, startYear, lastYear);                       

            configService.Save();           

        }
        static void Main(string[] args)
        {            
            
            JodyAppContext db = new JodyAppContext(JodyAppContext.CURRENT_DATABASE);
            LeagueService leagueService = new LeagueService(db);
            CompetitionService competitionService = new CompetitionService(db);
            SeasonService seasonService = new SeasonService(db);
            DivisionService divisionService = new DivisionService(db);
            ConfigService configService = new ConfigService(db);

            //SetupConfig(db, LeagueName);
            //AddTeam("Winnipeg", 3, "Western Conference", 1, null, configService, leagueService);     

            var League = leagueService.GetByName(LeagueName);

            var nextCompetition = leagueService.GetNextCompetition(League);

            if (nextCompetition == null)
            {
                League.CurrentYear++;
                nextCompetition = leagueService.GetNextCompetition(League);                
            }

            Random random = new Random();
            if (!nextCompetition.Started) nextCompetition.StartCompetition();
            competitionService.PlayGames(competitionService.GetNextGames(nextCompetition), nextCompetition, random);
            if (nextCompetition.IsComplete())
            {
                if (nextCompetition is Season) PrintSeason(seasonService, (Season)nextCompetition, League, divisionService.GetByName("League", League, (Season)nextCompetition), "Western Conference");
            }
            
            leagueService.Save();

            UpdateTeams(db, League, random);
            
            var div = configService.GetDivisionByName(League, "League");
            var format = "{0,-5}.{1,15}";
            WriteLine(div.Name + " Champions");
            divisionService.GetListOfDivisionWinners(null, null, div).OrderByDescending(dr => dr.Division.Season.Year).ToList().ForEach(dr =>
            {
                WriteLine(string.Format(format, dr.Division.Season.Year, dr.Team.Name));
            });
            WriteLine("Press Enter to End the Program");
            ReadLine();


        }

        public static void PrintSeason(SeasonService seasonService, Season season, League league, Division divisionToDisplay, params string[] divisionNamesToShow)
        {
            seasonService.SortAllDivisions(season);

            var teams = seasonService.GetTeamsInDivisionByRank(divisionToDisplay);
            teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

            StandingsView standingsView = new StandingsView();
            StandingsViewModel standingsViewModel = new StandingsViewModel(seasonService.db);
            standingsView.viewModel = standingsViewModel;
            standingsViewModel.SetStandingsCurrentYear(league.Name, season.Name , divisionNamesToShow);
            WriteLine(standingsView.GetDisplayString());


        }
/*        static void Main(string[] args)
        {
            JodyAppContext db = new JodyAppContext(JodyAppContext.CURRENT_DATABASE);
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            driver.UpdateData();
                        
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);
            LeagueService leagueService = new LeagueService(db);
            ConfigService configService = new ConfigService(db);

            BaseController controller = new StartController();

            string LeagueName = "Jody League";
            string RegularSeasonName = "Regular Season";

            Random random = new Random();
            int lastGameNumber = 0;

            League league = db.Leagues.Where(l => l.Name == LeagueName).First();            
            
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

                    StandingsView standingsView = new StandingsView();
                    StandingsViewModel standingsViewModel = new StandingsViewModel(db);
                    standingsView.viewModel = standingsViewModel;
                    standingsViewModel.SetStandingsCurrentYear(LeagueName, RegularSeasonName, "Premier", "Division1", "Division2");
                    System.Console.WriteLine(standingsView.GetDisplayString());

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



            configService.SetNewSkills(league, random);
            
            var promotionSeries = playoffService.GetSeriesByYear("Qualification", league.CurrentYear);

            ConfigTeam PromotedD1 = promotionSeries.GetWinner().Parent;
            ConfigTeam RelegatedP = promotionSeries.GetLoser().Parent;

            configService.ChangeDivision(PromotedD1, "Premier");
            configService.ChangeDivision(RelegatedP, "Division1");

            var d1promotionSeries = playoffService.GetSeriesByYear("D1 Qualification", league.CurrentYear);
            if (d1promotionSeries != null)
            {
                ConfigTeam PromotedD2 = d1promotionSeries.GetWinner().Parent;
                ConfigTeam RelegatedD1 = d1promotionSeries.GetLoser().Parent;

                configService.ChangeDivision(PromotedD2, "Division1");
                configService.ChangeDivision(RelegatedD1, "Division2");
            }
            
            db.SaveChanges();

            var playoffWinners = new Dictionary<int, string>();
            var playoffLosers = new Dictionary<int, string>();

            playoffService.GetSeries("Final").OrderBy(s => s.Playoff.Year).ToList().ForEach(series =>
            {
                playoffWinners.Add(series.Playoff.Year, series.GetWinner().Name);
                playoffLosers.Add(series.Playoff.Year, series.GetLoser().Name);
            });

            var divisionRanks = db.DivisionRanks.Include("Division").Include("Division.Season").Include("Team")
                .Where(dr => dr.Division.Name == "League" && dr.Rank == 1).ToDictionary(dr2 => dr2.Division.Season.Year, dr2 => dr2.Team.Name);

            divisionRanks.OrderByDescending(m => m.Key);

            string formatter = "{0,3}{1,15}{2,15}{3,15}{4,15}{5,15}{6,15}{7,15}";
            WriteLine("\n");
            WriteLine(String.Format(formatter, "Yr", "Champion", "Runner-Up", "Season", "To Premier", "To D1", "To D1", "To D2"));

            var qualificationSeries = playoffService.GetSeries("Qualification");
            var qualificationSeries2 = playoffService.GetSeries("D1 Qualification");

            for (int i = playoffWinners.Count; i > 0; i--)
            {
                var promoted = qualificationSeries.Where(s => s.Playoff.Year == i).FirstOrDefault();
                var promoted2 = qualificationSeries2.Where(s => s.Playoff.Year == i).FirstOrDefault();
                System.Console.WriteLine(String.Format(formatter, i, playoffWinners[i], playoffLosers[i], divisionRanks[i],
                    promoted != null ? promoted.GetWinner().Name : "",
                    promoted != null ? promoted.GetLoser().Name : "",
                    promoted2 != null ? promoted2.GetWinner().Name : "",
                    promoted2 != null ? promoted2.GetLoser().Name : ""));
            }


            WriteLine(controller.ParseInput(new List<string> { "League", "Get", "6005"}, 0).GetDisplayString());
            WriteLine(controller.ParseInput(new List<string> { "Season", "List", "6005" }, 0).GetDisplayString());
            WriteLine(controller.ParseInput(new List<string> { "Season", "List" }, 0).GetDisplayString());
            WriteLine(controller.ParseInput(new List<string> { "Season", "Get", "7013" }, 0).GetDisplayString());


            WriteLine("Press ENTER to end program.");
            ReadLine();

            

        }

    }
    */

    }
    }
