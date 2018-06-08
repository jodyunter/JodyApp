﻿using JodyApp.Database;
using JodyApp.Domain.Config;
using JodyApp.Domain;
using JodyApp.Service;
using JodyApp.Service.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain.Table;
using JodyApp.ConsoleApp.App;
using JodyApp.ConsoleApp.Views;

namespace JodyApp.ConsoleApp.Commands
{
    public class SetupCommands
    {
        static string LeagueName = "Jody's League";
        static string RegularSeasonName = "Regular Season";
        static string PlayoffName = "Playoffs";
        static string CentralDivisionName = "Central Division";
        static string WestDivisionName = "West Division";
        static string EastDivisionName = "East Division";
        static string AtlanticDivisionName = "Atlantic Division";
        static string SouthDivisionName = "South Division";
        static string ChampionshipSeriesName = "Championship Series";

        public SetupCommands() { }

        public SetupCommands(ApplicationContext context) { }

        [Command]
        public BaseView Setup(ApplicationContext context)
        {
            var configService = new ConfigService(context.DbContext);

            SetupConfig(context.DbContext, configService, (LeagueService)context.ServiceLibraries["League"]);

            SetupPlayoff(LeagueName, PlayoffName, ConfigCompetition.PLAYOFF, RegularSeasonName, 2, 1, null, configService, (LeagueService)context.ServiceLibraries["League"]);

            return new MessageView("Setup Done");
        }


        [Command]
        public BaseView SetupSeason(ApplicationContext context)
        {
            var configService = new ConfigService(context.DbContext);

            SetupConfig(context.DbContext, configService, (LeagueService)context.ServiceLibraries["League"]);

            //SetupPlayoff(LeagueName, PlayoffName, ConfigCompetition.PLAYOFF, RegularSeasonName, 2, 1, null, configService, (LeagueService)context.ServiceLibraries["League"]);

            return new MessageView("Setup Done");
        }
        [Command]
        public BaseView SetupPlayoff(ApplicationContext context)
        {
            var configService = new ConfigService(context.DbContext);

            //SetupConfig(context.DbContext, configService, (LeagueService)context.ServiceLibraries["League"]);

            SetupPlayoff(LeagueName, PlayoffName, ConfigCompetition.PLAYOFF, RegularSeasonName, 2, 1, null, configService, (LeagueService)context.ServiceLibraries["League"]);

            return new MessageView("Setup Done");
        }

        void SetupPlayoff(string leagueName, string name, int type, string referenceName, int order, int? firstYear, int? lastYear, ConfigService configService, LeagueService leagueService)
        {
            var League = leagueService.GetByName(leagueName);
            var CCReference = configService.GetCompetitionByName(League, referenceName);
            var CCPlayoff = configService.CreateCompetition(League, name, type, CCReference, order, firstYear, lastYear);

            var CDLeague = configService.GetDivisionByName(League, "League");
            var CDCentral = configService.GetDivisionByName(League, CentralDivisionName);
            var CDWest = configService.GetDivisionByName(League, WestDivisionName);
            var CDEast = configService.GetDivisionByName(League, EastDivisionName);
            var CDSouth = configService.GetDivisionByName(League, SouthDivisionName);

            var GPool = configService.CreateGroup("Playoff Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromDivision(GPool, "Playoff Pool Rule 1", CDLeague, 1, 12, firstYear, lastYear);

            var SRQ1 = configService.CreateSeriesRule(CCPlayoff, "Q-1", 1, GPool, 5, GPool, 12, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ2 = configService.CreateSeriesRule(CCPlayoff, "Q-2", 1, GPool, 6, GPool, 11, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ3 = configService.CreateSeriesRule(CCPlayoff, "Q-3", 1, GPool, 7, GPool, 10, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQ4 = configService.CreateSeriesRule(CCPlayoff, "Q-4", 1, GPool, 8, GPool, 9, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var GRound1Winners = configService.CreateGroup("Round 1 Winners", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 1", "Q-1", firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 2", "Q-2", firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 3", "Q-3", firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(GRound1Winners, "Round 1 Winners 4", "Q-4", firstYear, lastYear);

            var SRQF1 = configService.CreateSeriesRule(CCPlayoff, "QF-1", 2, GPool, 1, GRound1Winners, 4, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF2 = configService.CreateSeriesRule(CCPlayoff, "QF-2", 2, GPool, 2, GRound1Winners, 3, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF3 = configService.CreateSeriesRule(CCPlayoff, "QF-3", 2, GPool, 3, GRound1Winners, 2, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRQF4 = configService.CreateSeriesRule(CCPlayoff, "QF-4", 2, GPool, 4, GRound1Winners, 1, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var SemiFinalPool = configService.CreateGroup("Semi Final Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP1", SRQF1.Name, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP2", SRQF2.Name, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP3", SRQF3.Name, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(SemiFinalPool, "SFP4", SRQF4.Name, firstYear, lastYear);

            var SRSF1 = configService.CreateSeriesRule(CCPlayoff, "SF-1", 3, SemiFinalPool, 1, SemiFinalPool, 4, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);
            var SRSF2 = configService.CreateSeriesRule(CCPlayoff, "SF-2", 3, SemiFinalPool, 2, SemiFinalPool, 3, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            var FinalPool = configService.CreateGroup("Final Pool", CCPlayoff, new List<ConfigGroupRule>(), CDLeague, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(FinalPool, "Final Pool 1", SRSF1.Name, firstYear, lastYear);
            configService.CreateGroupRuleFromSeriesWinner(FinalPool, "Final Pool 2", SRSF2.Name, firstYear, lastYear);

            var SRFinal = configService.CreateSeriesRule(CCPlayoff, ChampionshipSeriesName, 4, FinalPool, 1, FinalPool, 2, ConfigSeriesRule.TYPE_BEST_OF, 4, false, null, firstYear, lastYear);

            configService.Save();

        }
        
        void SetupConfig(JodyAppContext db, ConfigService configService, LeagueService leagueService)
        {
            League League;
            ConfigCompetition CCSeason;
            ConfigTeam CTEdmonton, CTCalgary, CTVancouver, CTVictoria, CTSeattle;
            ConfigTeam CTWinnipeg, CTSaskatoon, CTMinnesota, CTColorado, CTChicago;
            ConfigTeam CTDetroit, CTToronto, CTHamilton, CTOttawa, CTMontreal;
            ConfigTeam CTWashington, CTPittsburgh, CTPhiladelphia, CTNewYork, CTQuebecCity;
            ConfigTeam CTTampaBay, CTAtlanta, CTDallas, CTColumbus, CTNashville;
            ConfigDivision CDLeague, CDWest, CDEast, CDCentral, CDAtlantic, CDSouth;


            SimpleTestDataDriver driver = new SimpleTestDataDriver();

            driver.DeleteAllData();

            League = leagueService.CreateLeague(LeagueName);
            CCSeason = configService.CreateCompetition(League, RegularSeasonName, ConfigCompetition.SEASON, null, 1, 1, null);

            CTEdmonton = configService.CreateTeam("Edmonton", 5, null, League, 1, null);
            CTCalgary = configService.CreateTeam("Calgary", 5, null, League, 1, null);
            CTVancouver = configService.CreateTeam("Vancouver", 5, null, League, 1, null);
            CTVictoria = configService.CreateTeam("Victoria", 5, null, League, 1, null);
            CTSeattle = configService.CreateTeam("Seattle", 5, null, League, 1, null);
            CTWinnipeg = configService.CreateTeam("Winnipeg", 5, null, League, 1, null);
            CTSaskatoon = configService.CreateTeam("Saskatoon", 5, null, League, 1, null);
            CTMinnesota = configService.CreateTeam("Minnesota", 5, null, League, 1, null);
            CTColorado = configService.CreateTeam("Colorado", 5, null, League, 1, null);
            CTChicago = configService.CreateTeam("Chicago", 5, null, League, 1, null);
            CTDetroit = configService.CreateTeam("Detroit", 5, null, League, 1, null);
            CTToronto = configService.CreateTeam("Toronto", 5, null, League, 1, null);
            CTHamilton = configService.CreateTeam("Hamilton", 5, null, League, 1, null);
            CTMontreal = configService.CreateTeam("Montreal", 5, null, League, 1, null);
            CTOttawa = configService.CreateTeam("Ottawa", 5, null, League, 1, null);
            CTWashington = configService.CreateTeam("Washington", 5, null, League, 1, null);
            CTPittsburgh = configService.CreateTeam("Pittsburgh", 5, null, League, 1, null);
            CTPhiladelphia = configService.CreateTeam("Philadelphia", 5, null, League, 1, null);
            CTQuebecCity = configService.CreateTeam("Quebec City", 5, null, League, 1, null);
            CTNewYork = configService.CreateTeam("New York", 5, null, League, 1, null);
            CTTampaBay = configService.CreateTeam("Tampa Bay", 5, null, League, 1, null);
            CTAtlanta = configService.CreateTeam("Atlanta", 5, null, League, 1, null);
            CTDallas = configService.CreateTeam("Dallas", 5, null, League, 1, null);
            CTColumbus = configService.CreateTeam("Columbus", 5, null, League, 1, null);
            CTNashville = configService.CreateTeam("Nashville", 5, null, League, 1, null);

            var WestTeams = new List<ConfigTeam>() { CTEdmonton, CTCalgary, CTVancouver, CTVictoria, CTSeattle };
            var CentralTeams = new List<ConfigTeam>() { CTWinnipeg, CTSaskatoon, CTMinnesota, CTColorado, CTChicago };
            var EastTeams = new List<ConfigTeam>() { CTDetroit, CTToronto, CTHamilton, CTOttawa, CTMontreal };
            var AtlanticTeams = new List<ConfigTeam>() { CTWashington, CTPittsburgh, CTPhiladelphia, CTNewYork, CTQuebecCity };
            var SouthTeams = new List<ConfigTeam>() { CTAtlanta, CTDallas, CTNashville, CTColumbus, CTTampaBay };

            CDLeague = configService.CreateDivision(League, CCSeason, "League", null, 1, 1, null, 1, null);
            CDWest = configService.CreateDivision(League, CCSeason, WestDivisionName, "West", 2, 2, CDLeague, 1, null);
            CDCentral = configService.CreateDivision(League, CCSeason, CentralDivisionName, "Central", 2, 3, CDLeague, 1, null);
            CDEast = configService.CreateDivision(League, CCSeason, EastDivisionName, "East", 2, 3, CDLeague, 1, null);
            CDAtlantic = configService.CreateDivision(League, CCSeason, AtlanticDivisionName, "Atlantic", 2, 4, CDLeague, 1, null);
            CDSouth = configService.CreateDivision(League, CCSeason, SouthDivisionName, "South", 2, 5, CDLeague, 1, null);

            CDCentral.Teams.AddRange(CentralTeams);
            CDWest.Teams.AddRange(WestTeams);
            CDEast.Teams.AddRange(EastTeams);
            CDAtlantic.Teams.AddRange(AtlanticTeams);
            CDSouth.Teams.AddRange(SouthTeams);

            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 1", CDLeague, true, 1, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 2", CDEast, true, 2, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 3", CDWest, true, 2, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 4", CDAtlantic, true, 2, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 5", CDCentral, true, 2, 1, false, 1, null);
            configService.CreateScheduleRuleByDivisionVsSelf(League, CCSeason, "Schedule Rule 6", CDSouth, true, 2, 1, false, 1, null);


            configService.CreateSortingRule("Division Leaders 1", 1, CDLeague, CDEast, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configService.CreateSortingRule("Division Leaders 2", 1, CDLeague, CDWest, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configService.CreateSortingRule("Division Leaders 3", 1, CDLeague, CDCentral, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configService.CreateSortingRule("Division Leaders 4", 1, CDLeague, CDAtlantic, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);
            configService.CreateSortingRule("Division Leaders 5", 1, CDLeague, CDSouth, "1,2,3", 0, SortingRule.SINGLE_DIVISION, 1, null);

            configService.Save();

        }

    }
}
