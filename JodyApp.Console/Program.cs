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
        static string RegularSeasonName = "Regular Season";
        static string PlayoffName = "Playoffs";
        static string CentralDivisionName = "Central Division";
        static string WestDivisionName = "West Division";
        static string EastDivisionName = "East Division";
        static string ChampionshipSeriesName = "Championship Series";

        static void UpdateTeams(JodyAppContext db, League league, Random random)
        {
            ConfigService configService = new ConfigService(db);
            configService.SetNewSkills(league, random);
            configService.Save();
        }
        
        static void SetupPlayoff(string leagueName, string name, int type, string referenceName, int order, int? firstYear, int? lastYear, ConfigService configService, LeagueService leagueService)
        {
            var League = leagueService.GetByName(leagueName);
            var CCReference = configService.GetCompetitionByName(League, referenceName);
            var CCPlayoff = configService.CreateCompetition(League, name, type, CCReference, order, firstYear, lastYear);

            var CDLeague = configService.GetDivisionByName(League, "League");
            var CDCentral = configService.GetDivisionByName(League, CentralDivisionName);
            var CDWest = configService.GetDivisionByName(League, WestDivisionName);
            var CDEast = configService.GetDivisionByName(League, EastDivisionName);

            var GDivisionWinners = configService.CreateGroup("Division Winners Group", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromDivision(GDivisionWinners, "Central Winner", CDCentral, 1, 1, firstYear, lastYear);
            configService.CreateGroupRuleFromDivision(GDivisionWinners, "East Winner", CDEast, 1, 1, firstYear, lastYear);
            configService.CreateGroupRuleFromDivision(GDivisionWinners, "West Winner", CDWest, 1, 1, firstYear, lastYear);

            var SRSemiFinalRule = configService.CreateSeriesRule(CCPlayoff, "Semi Final", 1, GDivisionWinners, 2, GDivisionWinners, 3, ConfigSeriesRule.TYPE_BEST_OF, 2, false, "1,0,1", firstYear, lastYear);

            var GChampionshipGroup = configService.CreateGroup("Championship Group", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(GChampionshipGroup, "Semi final Winner", SRSemiFinalRule.Name, firstYear, lastYear);

            var SRChampionshipSeriesRule = configService.CreateSeriesRule(CCPlayoff, ChampionshipSeriesName, 2,
                                        GDivisionWinners, 1,
                                        GChampionshipGroup, ConfigSeriesRule.SERIES_WINNER,
                                        ConfigSeriesRule.TYPE_BEST_OF, 4, false, ConfigSeriesRule.SEVEN_GAME_SERIES_HOME_GAMES, firstYear, lastYear);

            configService.Save();

        }
        
        static void SetupConfig(JodyAppContext db, ConfigService configService, LeagueService leagueService)
        {
            League League;
            ConfigCompetition CCSeason;
            ConfigTeam CTEdmonton, CTCalgary, CTVancouver, CTWinnipeg;
            ConfigDivision CDLeague, CDWest, CDEast, CDCentral;
            
            SimpleTestDataDriver driver = new SimpleTestDataDriver();

            driver.DeleteAllData();
            
            League = leagueService.CreateLeague(LeagueName);
            CCSeason = configService.CreateCompetition(League, RegularSeasonName, ConfigCompetition.SEASON, null, 1, 1, null);            

            CTEdmonton = configService.CreateTeam("Edmonton", 5, null, League, 1, null);
            CTCalgary = configService.CreateTeam("Calgary", 5, null, League, 1, null);
            CTVancouver = configService.CreateTeam("Vancouver", 5, null, League, 1, null);
            CTWinnipeg = configService.CreateTeam("Winnipeg", 5, null, League, 1, null);

            CDLeague = configService.CreateDivision(League, CCSeason, "League", null, 1, 1, null, 1, null);
            CDWest = configService.CreateDivision(League, CCSeason, WestDivisionName, "West", 2, 2, CDLeague, 1, null);
            CDCentral = configService.CreateDivision(League, CCSeason, CentralDivisionName, "Central", 2, 3, CDLeague, 1, null);
            CDEast = configService.CreateDivision(League, CCSeason, EastDivisionName, "East", 2, 3, CDLeague, 1, null);

            CDCentral.Teams.Add(CTEdmonton);
            CDCentral.Teams.Add(CTCalgary);
            CDWest.Teams.Add(CTVancouver);
            CDEast.Teams.Add(CTWinnipeg);

            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 1", CDLeague, true, 5, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 2", CDCentral, true, 5, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsDivision(League, CCSeason, "Schedule Rule 3", CDWest, CDEast, true, 5, 1, false, 1, null);

            configService.Save();
            
        }

        static void UpdateScheuleRules(string leagueName, string competitionName, int? firstYear, int? lastYear,ConfigService configService, LeagueService leagueService)
        {
            var League = leagueService.GetByName(leagueName);
            var CCSeason = configService.GetCompetitionByName(League, competitionName);
            var rule1 = configService.GetScheduleRuleByName(League, CCSeason, "Schedule Rule 1");
            var rule2 = configService.GetScheduleRuleByName(League, CCSeason, "Schedule Rule 2");
            var rule3 = configService.GetScheduleRuleByName(League, CCSeason, "Schedule Rule 3");

            rule2.LastYear = lastYear;
            rule3.LastYear = lastYear;

            configService.Save();
        }
        static void AddTeam(string name, int skill, string divisionName, int? startYear, int? lastYear, ConfigService configService, LeagueService leagueService)
        {
            var League = leagueService.GetByName(LeagueName);
            var CDDivision = configService.GetDivisionByName(League, divisionName);
            var CTNewTeam = configService.CreateTeam(name, skill, CDDivision, League, startYear, lastYear);                       

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
            PlayoffService playoffService = new PlayoffService(db);

            //SetupConfig(db, configService, leagueService);

            //SetupPlayoff(LeagueName, PlayoffName, ConfigCompetition.PLAYOFF, RegularSeasonName, 2, 4, null, configService, leagueService);

            //AddTeam("Minnesota", 2, EastDivisionName, 6, null, configService, leagueService);
            //AddTeam("Victoria", 2, WestDivisionName, 8, null, configService, leagueService);
            //AddTeam("Seattle", 1, WestDivisionName, 9, null, configService, leagueService);
            //AddTeam("Saskatoon", 1, CentralDivisionName, 10, null, configService, leagueService);
            //AddTeam("Toronto", 1, EastDivisionName, 11, null, configService, leagueService);

            //UpdateScheuleRules(LeagueName, RegularSeasonName, null, 5, configService, leagueService);

            var League = leagueService.GetByName(LeagueName);
            Random random = new Random();                        

            if (leagueService.IsYearDone(League)) League.CurrentYear++;

            while (!leagueService.IsYearDone(League))
            {

                var nextCompetition = leagueService.GetNextCompetition(League);

                if (!nextCompetition.Started) nextCompetition.StartCompetition();
                competitionService.PlayGames(competitionService.GetNextGames(nextCompetition), nextCompetition, random);

                if (nextCompetition.IsComplete())
                {
                    if (nextCompetition is Season)
                    {
                        PrintSeason(seasonService, (Season)nextCompetition, League, divisionService.GetByName("League", League, (Season)nextCompetition), "League");
                        seasonService.SortAllDivisions((Season)nextCompetition);
                    }
                    else if (nextCompetition is Playoff) PrintPlayoff((Playoff)nextCompetition);
                }

                leagueService.Save();

            }

            var div = configService.GetDivisionByName(League, "League");
            var divisionFormat = "{0,5}. {1,15}";
            WriteLine(div.Name + " Champions");
            divisionService.GetListOfDivisionWinners(null, null, div).OrderByDescending(dr => dr.Division.Season.Year).ToList().ForEach(dr =>
            {
                WriteLine(string.Format(divisionFormat, dr.Division.Season.Year, dr.Team.Name));
            });

            var series = playoffService.GetSeries(ChampionshipSeriesName);

            WriteLine("Championship Results");
            var playoffFormat = "{0,5}. {1,15} - {2,3}:{3,3} - {4,15}";
            series.OrderByDescending(s => s.Playoff.Year).ToList().ForEach(s =>
            {
                WriteLine(string.Format(playoffFormat, s.Playoff.Year, s.GetWinner().Name, s.GetTeamWins(s.GetWinner()), s.GetTeamWins(s.GetLoser()), s.GetLoser().Name));
            });

            UpdateTeams(db, League, random);
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

        public static void PrintPlayoff(Playoff playoff)
        {
            for (int i = 1; i <= playoff.CurrentRound; i++)
            {
                WriteLine("Round " + i + ":");
                playoff.GetSeriesForRound(i).ForEach(series => WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));
            }
        }

    }
    }
