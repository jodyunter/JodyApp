using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using JodyApp.Service;
using JodyApp.Service.Test;
using System.Collections.Generic;
using System.Linq;
using JodyApp.Domain.Table;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;
using JodyApp.Service.ConfigServices;
using static JodyApp.ConsoleApp.App.AppConstants;

namespace JodyApp.ConsoleApp.Commands
{
    public class SetupCommands
    {
        static string LEAGUENAME = "Jody's League";
        static string REGULARSEASONNAME = "Regular Season";
        static string PLAYOFFNAME = "Playoffs";
        static string CENTRALDIVISIONNAME = "Central Division";
        static string WESTDIVISIONNAME = "West Division";
        static string EASTDIVISIONNAME = "East Division";
        static string ATLANTICDIVISIONNAME = "Atlantic Division";
        static string SOUTHDIVISIONNAME = "South Division";
        static string CHAMPIONSHIPSERIES = "Championship Series";

        public SetupCommands() { }

        public SetupCommands(ApplicationContext context) { }

        [Command]
        public BaseView Setup(ApplicationContext context)
        {            

            SetupConfig(context.DbContext,
                (ConfigTeamService)context.ServiceLibraries[SERVICE_CONFIGTEAM],
                (ConfigDivisionService)context.ServiceLibraries[SERVICE_CONFIGDIVISION],
                (ConfigScheduleRuleService)context.ServiceLibraries[SERVICE_CONFIGSCHEDULERULE],
                (ConfigCompetitionService)context.ServiceLibraries[SERVICE_CONFIGCOMPETITION],
                (ConfigSortingRuleService)context.ServiceLibraries[SERVICE_CONFIGSORTINGRULE],
                (LeagueService)context.ServiceLibraries[SERVICE_LEAGUE]);

            SetupPlayoff(LEAGUENAME, PLAYOFFNAME, ConfigCompetition.PLAYOFF, REGULARSEASONNAME, 2, 1, null,
                (ConfigCompetitionService)context.ServiceLibraries[SERVICE_CONFIGCOMPETITION],
                (ConfigDivisionService)context.ServiceLibraries[SERVICE_CONFIGDIVISION],
                (ConfigGroupRuleService)context.ServiceLibraries[SERVICE_CONFIGGROUPRULE],
                (ConfigGroupService)context.ServiceLibraries[SERVICE_CONFIGGROUP],
                (ConfigSeriesRuleService)context.ServiceLibraries[SERVICE_CONFIGSERIESRULE],                
                (LeagueService)context.ServiceLibraries[SERVICE_LEAGUE]);

            return new MessageView("Setup Done");
        }

        [Command]
        public BaseView SetupColour(ApplicationContext context)
        {

            SetupColourConfig(context.DbContext,
                (ConfigTeamService)context.ServiceLibraries[SERVICE_CONFIGTEAM],
                (ConfigDivisionService)context.ServiceLibraries[SERVICE_CONFIGDIVISION],
                (ConfigScheduleRuleService)context.ServiceLibraries[SERVICE_CONFIGSCHEDULERULE],
                (ConfigCompetitionService)context.ServiceLibraries[SERVICE_CONFIGCOMPETITION],
                (ConfigSortingRuleService)context.ServiceLibraries[SERVICE_CONFIGSORTINGRULE],
                (LeagueService)context.ServiceLibraries[SERVICE_LEAGUE]);


            return new MessageView("Setup Done");
        }


        [Command]
        public BaseView SetupSeason(ApplicationContext context)
        {
            SetupConfig(context.DbContext,
                (ConfigTeamService)context.ServiceLibraries[SERVICE_CONFIGTEAM],
                (ConfigDivisionService)context.ServiceLibraries[SERVICE_CONFIGDIVISION],
                (ConfigScheduleRuleService)context.ServiceLibraries[SERVICE_CONFIGSCHEDULERULE],
                (ConfigCompetitionService)context.ServiceLibraries[SERVICE_CONFIGCOMPETITION],
                (ConfigSortingRuleService)context.ServiceLibraries[SERVICE_CONFIGSORTINGRULE],
                (LeagueService)context.ServiceLibraries[SERVICE_LEAGUE]);
            
            return new MessageView("Setup Done");
        }
        [Command]
        public BaseView SetupPlayoff(ApplicationContext context)
        {
            SetupPlayoff(LEAGUENAME, PLAYOFFNAME, ConfigCompetition.PLAYOFF, REGULARSEASONNAME, 2, 1, null,
                (ConfigCompetitionService)context.ServiceLibraries[SERVICE_CONFIGCOMPETITION],
                (ConfigDivisionService)context.ServiceLibraries[SERVICE_CONFIGDIVISION],
                (ConfigGroupRuleService)context.ServiceLibraries[SERVICE_CONFIGGROUPRULE],
                (ConfigGroupService)context.ServiceLibraries[SERVICE_CONFIGGROUP],
                (ConfigSeriesRuleService)context.ServiceLibraries[SERVICE_CONFIGSERIESRULE],
                (LeagueService)context.ServiceLibraries[SERVICE_LEAGUE]);

            return new MessageView("Setup Done");
        }

        void SetupPlayoff(string leagueName, string name, int type, string referenceName, int order, int? firstYear, int? lastYear, 
            ConfigCompetitionService configCompetitionService, ConfigDivisionService configDivisionService, 
            ConfigGroupRuleService configGroupRuleService, ConfigGroupService configGroupService, 
            ConfigSeriesRuleService configSeriesRuleService, LeagueService leagueService)
        {            
            var League = leagueService.GetByName(leagueName);
            var CCReference = configCompetitionService.GetCompetitionByName(League, referenceName);
            var CCPlayoff = configCompetitionService.CreateCompetition(League, name, type, CCReference, order, firstYear, lastYear);

            var CDLeague = configDivisionService.GetDivisionByName(League, "League");
            var CDCentral = configDivisionService.GetDivisionByName(League, CENTRALDIVISIONNAME);
            var CDWest = configDivisionService.GetDivisionByName(League, WESTDIVISIONNAME);
            var CDEast = configDivisionService.GetDivisionByName(League, EASTDIVISIONNAME);
            var CDSouth = configDivisionService.GetDivisionByName(League, SOUTHDIVISIONNAME);

            var GPool = configGroupService.CreateGroup("Playoff Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromDivision(GPool, "Playoff Pool Rule 1", CDLeague, 1, 12, firstYear, lastYear);

            var SRQ1 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "Q-1", 1, GPool, 5, GPool, 12, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ2 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "Q-2", 1, GPool, 6, GPool, 11, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ3 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "Q-3", 1, GPool, 7, GPool, 10, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ4 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "Q-4", 1, GPool, 8, GPool, 9, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var GRound1Winners = configGroupService.CreateGroup("Round 1 Winners", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 1", "Q-1", firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 2", "Q-2", firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 3", "Q-3", firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 4", "Q-4", firstYear, lastYear);

            var SRQF1 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "QF-1", 2, GPool, 1, GRound1Winners, 4, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF2 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "QF-2", 2, GPool, 2, GRound1Winners, 3, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF3 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "QF-3", 2, GPool, 3, GRound1Winners, 2, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF4 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "QF-4", 2, GPool, 4, GRound1Winners, 1, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var SemiFinalPool = configGroupService.CreateGroup("Semi Final Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP1", SRQF1.Name, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP2", SRQF2.Name, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP3", SRQF3.Name, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP4", SRQF4.Name, firstYear, lastYear);

            var SRSF1 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "SF-1", 3, SemiFinalPool, 1, SemiFinalPool, 4, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRSF2 = configSeriesRuleService.CreateSeriesRule(CCPlayoff, "SF-2", 3, SemiFinalPool, 2, SemiFinalPool, 3, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var FinalPool = configGroupService.CreateGroup("Final Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(FinalPool, "Final Pool 1", SRSF1.Name, firstYear, lastYear);
            configGroupRuleService.CreateGroupRuleFromSeriesWinner(FinalPool, "Final Pool 2", SRSF2.Name, firstYear, lastYear);

            var SRFinal = configSeriesRuleService.CreateSeriesRule(CCPlayoff, CHAMPIONSHIPSERIES, 4, FinalPool, 1, FinalPool, 2, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            leagueService.Save();

        }


        void SetupColourConfig(JodyAppContext db,
            ConfigTeamService configTeamService, ConfigDivisionService configDivisionService,
            ConfigScheduleRuleService scheduleRuleservice, ConfigCompetitionService configCompetitionService,
            ConfigSortingRuleService configSortingRuleService, LeagueService leagueService)
        {
            League ColourLeague;
            ConfigCompetition CCSeason;
            ConfigDivision CDLeague;
            var teamList = new List<ConfigTeam>();

            var skills = new int[] { 0, 0, 0 };
            var names = new string[] { "Red", "Blue", "Yellow" };

            //SimpleTestDataDriver driver = new SimpleTestDataDriver(db);

            //driver.DeleteAllData();


            ColourLeague = leagueService.CreateLeague("Colour League");
            CCSeason = configCompetitionService.CreateCompetition(ColourLeague, "Colour Season", ConfigCompetition.SEASON, null, 1, 1, null);

            int i = 0;
            names.ToList().ForEach(name =>
            {
                teamList.Add(configTeamService.CreateTeam(names[i], skills[i], null, ColourLeague, 1, null)); 
                i++;
            });

            CDLeague = configDivisionService.CreateDivision(ColourLeague, CCSeason, "Colour League", null, 1, 1, null, 1, null);
            CDLeague.Teams.AddRange(teamList);

            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(ColourLeague, CCSeason, "Schedule Rule 1", CDLeague, true, 5, 1, false, 1, null);

            configSortingRuleService.Save();

        }
        void SetupConfig(JodyAppContext db, 
            ConfigTeamService configTeamService, ConfigDivisionService configDivisionService, 
            ConfigScheduleRuleService scheduleRuleservice, ConfigCompetitionService configCompetitionService, 
            ConfigSortingRuleService configSortingRuleService, LeagueService leagueService)
        {
            League League;
            ConfigCompetition CCSeason;
            ConfigTeam CTEdmonton, CTCalgary, CTVancouver, CTVictoria, CTSeattle;
            ConfigTeam CTWinnipeg, CTSaskatoon, CTMinnesota, CTColorado, CTChicago;
            ConfigTeam CTDetroit, CTToronto, CTHamilton, CTOttawa, CTMontreal;
            ConfigTeam CTWashington, CTPittsburgh, CTPhiladelphia, CTNewYork, CTQuebecCity;
            ConfigTeam CTTampaBay, CTAtlanta, CTDallas, CTColumbus, CTNashville;
            ConfigDivision CDLeague, CDWest, CDEast, CDCentral, CDAtlantic, CDSouth;


            SimpleTestDataDriver driver = new SimpleTestDataDriver(db);

            driver.DeleteAllData();

            League = leagueService.CreateLeague(LEAGUENAME);
            CCSeason = configCompetitionService.CreateCompetition(League, REGULARSEASONNAME, ConfigCompetition.SEASON, null, 1, 1, null);

            CTEdmonton = configTeamService.CreateTeam("Edmonton", 5, null, League, 1, null);
            CTCalgary = configTeamService.CreateTeam("Calgary", 5, null, League, 1, null);
            CTVancouver = configTeamService.CreateTeam("Vancouver", 5, null, League, 1, null);
            CTVictoria = configTeamService.CreateTeam("Victoria", 5, null, League, 1, null);
            CTSeattle = configTeamService.CreateTeam("Seattle", 5, null, League, 1, null);
            CTWinnipeg = configTeamService.CreateTeam("Winnipeg", 5, null, League, 1, null);
            CTSaskatoon = configTeamService.CreateTeam("Saskatoon", 5, null, League, 1, null);
            CTMinnesota = configTeamService.CreateTeam("Minnesota", 5, null, League, 1, null);
            CTColorado = configTeamService.CreateTeam("Colorado", 5, null, League, 1, null);
            CTChicago = configTeamService.CreateTeam("Chicago", 5, null, League, 1, null);
            CTDetroit = configTeamService.CreateTeam("Detroit", 5, null, League, 1, null);
            CTToronto = configTeamService.CreateTeam("Toronto", 5, null, League, 1, null);
            CTHamilton = configTeamService.CreateTeam("Hamilton", 5, null, League, 1, null);
            CTMontreal = configTeamService.CreateTeam("Montreal", 5, null, League, 1, null);
            CTOttawa = configTeamService.CreateTeam("Ottawa", 5, null, League, 1, null);
            CTWashington = configTeamService.CreateTeam("Washington", 5, null, League, 1, null);
            CTPittsburgh = configTeamService.CreateTeam("Pittsburgh", 5, null, League, 1, null);
            CTPhiladelphia = configTeamService.CreateTeam("Philadelphia", 5, null, League, 1, null);
            CTQuebecCity = configTeamService.CreateTeam("Quebec City", 5, null, League, 1, null);
            CTNewYork = configTeamService.CreateTeam("New York", 5, null, League, 1, null);
            CTTampaBay = configTeamService.CreateTeam("Tampa Bay", 5, null, League, 1, null);
            CTAtlanta = configTeamService.CreateTeam("Atlanta", 5, null, League, 1, null);
            CTDallas = configTeamService.CreateTeam("Dallas", 5, null, League, 1, null);
            CTColumbus = configTeamService.CreateTeam("Columbus", 5, null, League, 1, null);
            CTNashville = configTeamService.CreateTeam("Nashville", 5, null, League, 1, null);

            var WestTeams = new List<ConfigTeam>() { CTEdmonton, CTCalgary, CTVancouver, CTVictoria, CTSeattle };
            var CentralTeams = new List<ConfigTeam>() { CTWinnipeg, CTSaskatoon, CTMinnesota, CTColorado, CTChicago };
            var EastTeams = new List<ConfigTeam>() { CTDetroit, CTToronto, CTHamilton, CTOttawa, CTMontreal };
            var AtlanticTeams = new List<ConfigTeam>() { CTWashington, CTPittsburgh, CTPhiladelphia, CTNewYork, CTQuebecCity };
            var SouthTeams = new List<ConfigTeam>() { CTAtlanta, CTDallas, CTNashville, CTColumbus, CTTampaBay };

            CDLeague = configDivisionService.CreateDivision(League, CCSeason, "League", null, 1, 1, null, 1, null);
            CDWest = configDivisionService.CreateDivision(League, CCSeason, WESTDIVISIONNAME, "West", 2, 2, CDLeague, 1, null);
            CDCentral = configDivisionService.CreateDivision(League, CCSeason, CENTRALDIVISIONNAME, "Central", 2, 3, CDLeague, 1, null);
            CDEast = configDivisionService.CreateDivision(League, CCSeason, EASTDIVISIONNAME, "East", 2, 3, CDLeague, 1, null);
            CDAtlantic = configDivisionService.CreateDivision(League, CCSeason, ATLANTICDIVISIONNAME, "Atlantic", 2, 4, CDLeague, 1, null);
            CDSouth = configDivisionService.CreateDivision(League, CCSeason, SOUTHDIVISIONNAME, "South", 2, 5, CDLeague, 1, null);

            CDCentral.Teams.AddRange(CentralTeams);
            CDWest.Teams.AddRange(WestTeams);
            CDEast.Teams.AddRange(EastTeams);
            CDAtlantic.Teams.AddRange(AtlanticTeams);
            CDSouth.Teams.AddRange(SouthTeams);

            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 1", CDLeague, true, 1, 1, false, 1, null);
            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 2", CDEast, true, 2, 1, false, 1, null);
            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 3", CDWest, true, 2, 1, false, 1, null);
            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 4", CDAtlantic, true, 2, 1, false, 1, null);
            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 5", CDCentral, true, 2, 1, false, 1, null);
            scheduleRuleservice.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 6", CDSouth, true, 2, 1, false, 1, null);


            configSortingRuleService.CreateSortingRule("Division Leaders 1", 1, CDLeague, CDEast, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configSortingRuleService.CreateSortingRule("Division Leaders 2", 1, CDLeague, CDWest, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configSortingRuleService.CreateSortingRule("Division Leaders 3", 1, CDLeague, CDCentral, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configSortingRuleService.CreateSortingRule("Division Leaders 4", 1, CDLeague, CDAtlantic, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configSortingRuleService.CreateSortingRule("Division Leaders 5", 1, CDLeague, CDSouth, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);

            configSortingRuleService.Save();

        }


    }
}
