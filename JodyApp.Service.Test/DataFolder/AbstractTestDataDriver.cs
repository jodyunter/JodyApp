using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using JodyApp.Domain.Table;
using JodyApp.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace JodyApp.Service.Test.DataFolder
{
    public abstract class AbstractTestDataDriver
    {
        protected Dictionary<string, League> leagues = new Dictionary<string, League>();
        protected Dictionary<string, ConfigCompetition> configCompetitions = new Dictionary<string, ConfigCompetition>();
        protected Dictionary<string, ConfigDivision> configDivisions = new Dictionary<string, ConfigDivision>();
        protected Dictionary<string, ConfigTeam> configTeams = new Dictionary<string, ConfigTeam>();
        protected Dictionary<string, ConfigSortingRule> configSortingRules = new Dictionary<string, ConfigSortingRule>();
        protected Dictionary<string, ConfigGroup> configGroups = new Dictionary<string, ConfigGroup>();
        protected Dictionary<string, ConfigGroupRule> configGroupRules = new Dictionary<string, ConfigGroupRule>();
        protected Dictionary<string, ConfigSeriesRule> configSeriesRules = new Dictionary<string, ConfigSeriesRule>();
        protected Dictionary<string, ConfigScheduleRule> configScheduleRules = new Dictionary<string, ConfigScheduleRule>();

        protected Dictionary<string, Season> seasons = new Dictionary<string, Season>();
        protected Dictionary<string, Playoff> playoffs = new Dictionary<string, Playoff>();
        protected Dictionary<string, Division> divisions = new Dictionary<string, Division>();
        protected Dictionary<string, Team> teams = new Dictionary<string, Team>();
        protected Dictionary<string, SortingRule> sortingRules = new Dictionary<string, SortingRule>();
        protected Dictionary<string, Group> groups = new Dictionary<string, Group>();
        protected Dictionary<string, GroupRule> groupRules = new Dictionary<string, GroupRule>();
        protected Dictionary<string, SeriesRule> seriesRules = new Dictionary<string, SeriesRule>();
        protected Dictionary<string, Series> series = new Dictionary<string, Series>();


        public JodyAppContext db;        

        public AbstractTestDataDriver()
        {
            db = new JodyAppContext(JodyAppContext.CURRENT_DATABASE);
        }

        public AbstractTestDataDriver(JodyAppContext db)
        {
            this.db = db;
        }
        abstract public void PrivateCreateTeams();
        abstract public void PrivateCreateDivisions();
        abstract public void PrivateCreateScheduleRules();
        abstract public void PrivateCreateLeagues();
        abstract public void PrivateCreateSortingRules();
        abstract public void PrivateCreateSeriesRules();
        abstract public void PrivateCreateGroups();
        abstract public void PrivateCreateGroupRules();
        abstract public void PrivateCreateConfigCompetitions();
        abstract public void PrivateCreatePlayoffs();
        abstract public void PrivateCreateSeasons();
        abstract public void PrivateCreateSeries();

        public abstract void PrivateCreateConfigSeriesRules();
        public abstract void PrivateCreateConfigGroupRules();
        public abstract void PrivateCreateConfigGroups();
        public abstract void PrivateCreateConfigSortingRules();
        public abstract void PrivateCreateConfigTeams();
        public abstract void PrivateCreateConfigDivisions();
        public abstract void PrivateCreateConfigPlayoffs();        

        public void Setup()
        {
            DeleteAllData();
            InsertData();
        }
        public void DeleteAllData()
        {
            string[] tables = {
                "ConfigGroupRules",
                "ConfigSOrtingRules",
                "ConfigSeriesRules",
                "ConfigGroups",
                "ConfigScheduleRules",
                "ConfigTeams",
                "ConfigDivisions",
                "ConfigCompetitions",
                "GroupRules",
                "SortingRules",
                "DivisionRanks",
                "Games",
                "Series",
                "SeriesRules",
                "Groups",                
                "Teams",
                "TeamStatistics",           
                "Divisions",
                "ReferenceCompetitions",
                "Playoffs",
                "Seasons",
                "Leagues" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }


        public Season CreateAndAddSeason(League league, string name, int order, int year, int startingDay)
        {
            Season season = new Season(league, name, year, true, true, startingDay);

            seasons.Add(name, season);

            return season;

        }
        
        public ConfigCompetition CreateAndAddConfigCompetition(League league, string name, int type, ConfigCompetition reference, int order, int? firstYear, int? lastYear)
        {
            ConfigCompetition competition = new ConfigCompetition(league, name, type, reference, firstYear, lastYear);

            configCompetitions.Add(name, competition);

            league.ReferenceCompetitions.Add(new ReferenceCompetition()
            {
                League = league,
                Competition = competition,
                Order = order
            });

            return competition;

        }
        
        public Playoff CreateAndAddPlayoff(League league, string name, int order, Season season)
        {
            Playoff playoff = new Playoff(league, name, 0, true, true, 0, season);

            playoffs.Add(name, playoff);

            league.ReferenceCompetitions.Add(new ReferenceCompetition()
            {
                League = league,
                Competition = null,
                Order = order
            });

            return playoff;
        }

        public Division CreateAndAddDivision(League league, Season season, string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules)
        {
            Division div = new Division(league, season, name, shortName, level, order, parent, sortingRules);
            divisions.Add(div.Name, div);
            return div;
        }

        public ConfigDivision CreateAndAddConfigDivision(League league, ConfigCompetition season, string name, string shortName, int level, int order, ConfigDivision parent, List<SortingRule> sortingRules, int? firstYear, int? lastYear)
        {
            ConfigDivision div = new ConfigDivision(league, season, name, shortName, level, order, parent, firstYear, lastYear);
            configDivisions.Add(div.Name, div);
            return div;
        }

        public Team CreateAndAddTeam(string name, int skill, Division division)
        {
            Team team = new Team(name, skill, division);
            teams.Add(team.Name, team);            
            return team;
        }

        public Team CreateAndAddTeam(string name, int skill, TeamStatistics stats, Division division)
        {
            Team team = new Team(name, skill, stats, division);
            teams.Add(team.Name, team);
            return team;
        }

        public ConfigTeam CreateAndAddConfigTeam(string name, int skill, ConfigDivision division, League league, int? firstYear, int? lastYear)
        {
            ConfigTeam team = new ConfigTeam(name, skill, division, league, firstYear, lastYear);
            configTeams.Add(team.Name, team);
            return team;
        }

        public ConfigScheduleRule CreateAndAddScheduleRule(ConfigScheduleRule newRule)
        {
            ConfigScheduleRule rule = new ConfigScheduleRule(newRule);
            configScheduleRules.Add(rule.Name, rule);
            return rule;
        }

        public ConfigScheduleRule CreateAndAddScheduleRule(League league, ConfigCompetition season, string name, 
                                int homeType, ConfigTeam homeTeam, ConfigDivision homeDivision,
                                int awayType, ConfigTeam awayTeam, ConfigDivision awayDivision,
                                bool homeAndAway, int rounds, int divisionLevel, int order, bool reverse)
        {
            ConfigScheduleRule rule = new ConfigScheduleRule(league, season, name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, homeAndAway, rounds, divisionLevel, order, reverse);
            configScheduleRules.Add(rule.Name, rule);
            return rule;
        }

        public SeriesRule CreateAndAddSeriesRule(Playoff p, string name, int round, Group homeTeamFromGroup, int homeTeamFromRank,
                                            Group awayTeamFromGroup, int awayTeamFromRank, int seriesType,
                                            int gamesNeeded, bool canTie, string homeGames)
        {
            SeriesRule rule = new SeriesRule(p, name, round, homeTeamFromGroup, homeTeamFromRank, awayTeamFromGroup, awayTeamFromRank, seriesType, gamesNeeded, canTie, homeGames);
            seriesRules.Add(rule.Name, rule);
            return rule;
            
        }

        public ConfigSeriesRule CreateAndAddConfigSeriesRule(ConfigCompetition p, string name, int round, ConfigGroup homeTeamFromGroup, int homeTeamFromRank,
                                    ConfigGroup awayTeamFromGroup, int awayTeamFromRank, int seriesType,
                                    int gamesNeeded, bool canTie, string homeGames, int? firstYear, int? lastYear)
        {
            ConfigSeriesRule rule = new ConfigSeriesRule(p, name, round, homeTeamFromGroup, homeTeamFromRank, awayTeamFromGroup, awayTeamFromRank, seriesType, gamesNeeded, canTie, homeGames, firstYear, lastYear);
            configSeriesRules.Add(rule.Name, rule);
            return rule;

        }

        public SortingRule CreateAndAddSortingRule(Division division, String name, int groupNumber, Division divToGetTeamsFrom, string positionsToUse, int divisionLevel, int ruleType)
        {
            SortingRule rule = new SortingRule()
            {
                Division = division,
                Name = name,
                GroupNumber = groupNumber,
                DivisionToGetTeamsFrom = divToGetTeamsFrom,
                PositionsToUse = positionsToUse,
                DivisionLevel = divisionLevel,
                Type = ruleType
            };

            sortingRules.Add(rule.Name, rule);
            return rule;
        }

        public ConfigSortingRule CreateAndAddConfigSortingRule(ConfigDivision division, String name, int groupNumber, ConfigDivision divToGetTeamsFrom, string positionsToUse, int divisionLevel, int ruleType, int? firstYear, int? lastYear)
        {
            ConfigSortingRule rule = new ConfigSortingRule()
            {
                Division = division,
                Name = name,
                GroupNumber = groupNumber,
                DivisionToGetTeamsFrom = divToGetTeamsFrom,
                PositionsToUse = positionsToUse,
                DivisionLevel = divisionLevel,
                Type = ruleType,                
                FirstYear = firstYear,
                LastYear = lastYear
            };

            configSortingRules.Add(rule.Name, rule);
            return rule;
        }

        public League CreateAndAddLeague(String name)
        {
            League l = new League() { Name = name };

            leagues.Add(l.Name, l);

            return l;
        }

        public Group CreateAndAddGroup(Playoff p, string name, Division sortByDivision)
        {
            Group group = new Group(name, p, sortByDivision, new List<GroupRule>());
            groups.Add(name, group);
            return group;

        }

        public ConfigGroup CreateAndAddConfigGroup(ConfigCompetition p, string name, ConfigDivision sortByDivision, int? firstYear, int? lastYear)
        {
            ConfigGroup group = new ConfigGroup(name, p, new List<ConfigGroupRule>(), sortByDivision, firstYear, lastYear);
            configGroups.Add(name, group);
            return group;

        }
        public GroupRule CreateAndAddGroupRule(Group g, string name, int ruleType, Division fromDivision, 
                                                String seriesName, int fromStartValue, int fromEndValue, Team fromTeam)
        {
            GroupRule rule = new GroupRule(g, name, ruleType, fromDivision, seriesName, fromStartValue, fromEndValue, fromTeam);

            groupRules.Add(groupRules.Keys.Count.ToString(), rule);            
            return rule;
        }

        public GroupRule CreateAndAddGroupRule(GroupRule rule)
        {
            groupRules.Add(groupRules.Keys.Count.ToString(), rule);

            return rule;
        }

        public ConfigGroupRule CreateAndAddConfigGroupRule(ConfigGroupRule rule)
        {
            configGroupRules.Add(groupRules.Keys.Count.ToString(), rule);

            return rule;
        }

        //public GroupRule(League league, Playoff playoff, int ruleType, Division sortByDivision, Division fromDivision, String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, bool isHomeTeam, string groupIdentifier)

        public virtual void InsertData()
        {
            CreateObjects(PrivateCreateLeagues, leagues);
            CreateObjects(PrivateCreateConfigCompetitions, db.ConfigCompetitions, seasons);                   
            CreateObjects(PrivateCreateConfigDivisions, configDivisions);
            CreateObjects(PrivateCreateConfigTeams, configTeams);
            CreateObjects(PrivateCreateConfigSortingRules, configSortingRules); 
            CreateObjects(PrivateCreateConfigGroups, configGroups);
            CreateObjects(PrivateCreateConfigGroupRules, configGroupRules);
            CreateObjects(PrivateCreateConfigSeriesRules, configSeriesRules);
            CreateObjects(PrivateCreateScheduleRules, configScheduleRules);



            CreateObjects(PrivateCreateSeasons, db.Seasons, seasons);
            CreateObjects(PrivateCreatePlayoffs, db.Playoffs, playoffs);
            CreateObjects(PrivateCreateDivisions, db.Divisions, divisions);
            CreateObjects(PrivateCreateTeams, db.Teams, teams);
            CreateObjects(PrivateCreateSortingRules, db.SortingRules, sortingRules); 
            CreateObjects(PrivateCreateGroups, db.Groups, groups);
            CreateObjects(PrivateCreateGroupRules, db.GroupRules, groupRules); 
            CreateObjects(PrivateCreateSeriesRules, db.SeriesRules, seriesRules);
            CreateObjects(PrivateCreateSeries, db.Series, series);

            db.SaveChanges();

        }

        public void CreateObjects<T>(Action method, DbSet m, Dictionary<string, T> collection)
        {            
            method();

            if (m != null)
                m.AddRange(collection.Values);                        
        }

        public void CreateObjects<T>(Action method, Dictionary<string, T> collection)
        {
            CreateObjects<T>(method, null, collection);
        }


        public virtual void UpdateData()
        {

        }
    }
}
