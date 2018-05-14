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
        protected Dictionary<string, League> leagues;
        protected Dictionary<string, ConfigSeason> configSeasons;
        protected Dictionary<string, ConfigPlayoff> configPlayoffs;
        protected Dictionary<string, ConfigDivision> configDivisions;
        protected Dictionary<string, ConfigTeam> configTeams;
        protected Dictionary<string, ConfigSortingRule> configSortingRules;
        protected Dictionary<string, ConfigGroup> configGroups;
        protected Dictionary<string, ConfigGroupRule> configGroupRules;
        protected Dictionary<string, ConfigSeriesRule> configSeriesRules;
        protected Dictionary<string, ConfigScheduleRule> configScheduleRules;

        protected Dictionary<string, Season> seasons;
        protected Dictionary<string, Playoff> playoffs;
        protected Dictionary<string, Division> divisions;
        protected Dictionary<string, Team> teams;
        protected Dictionary<string, SortingRule> sortingRules;
        protected Dictionary<string, Group> groups;
        protected Dictionary<string, GroupRule> groupRules;
        protected Dictionary<string, SeriesRule> seriesRules;
        protected Dictionary<string, Series> series;


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
        abstract public void PrivateCreateSeasons();
        abstract public void PrivateCreatePlayoffs();
        abstract public void PrivateCreateSeries();

        public abstract void PrivateCreateConfigSeriesRules();
        public abstract void PrivateCreateConfigGroupRules();
        public abstract void PrivateCreateConfigGroups();
        public abstract void PrivateCreateConfigSortingRules();
        public abstract void PrivateCreateConfigTeams();
        public abstract void PrivateCreateConfigDivisions();
        public abstract void PrivateCreateConfigPlayoffs();
        public abstract void PrivateCreateConfigSeasons();

        public void DeleteAllData()
        {
            string[] tables = {
                "GroupRules",
                "SortingRules",
                "DivisionRanks",
                "Games",
                "Series",
                "SeriesRules",
                "Groups",
                "ScheduleRules",
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
            
            league.ReferenceCompetitions.Add(new ReferenceCompetition()
            {
                League = league,
                Season = null,
                Playoff = null,
                Order = order
            });

            return season;

        }


        public ConfigSeason CreateAndAddConfigSeason(League league, string name, int? firstYear, int? lastYear, int order)
        {
            ConfigSeason season = new ConfigSeason(league, name, firstYear, lastYear);

            configSeasons.Add(name, season);

            league.ReferenceCompetitions.Add(new ReferenceCompetition()
            {
                League = league,
                Season = season,
                Playoff = null,
                Order = order
            });

            return season;

        }



        public Playoff CreateAndAddPlayoff(League league, string name, int order, Season season)
        {
            Playoff playoff = new Playoff(league, name, 0, true, true, 0, season);

            playoffs.Add(name, playoff);

            league.ReferenceCompetitions.Add(new ReferenceCompetition()
            {
                League = league,
                Season = null,
                Playoff = playoff,
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

        public ConfigDivision CreateAndAddConfigDivision(League league, ConfigSeason season, string name, string shortName, int level, int order, ConfigDivision parent, List<SortingRule> sortingRules, int? firstYear, int? lastYear)
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

        public ConfigTeam CreateAndAddConfigTeam(string name, int skill, int? firstYear, int? lastYear, ConfigDivision division)
        {
            ConfigTeam team = new ConfigTeam(name, skill, division, firstYear, lastYear);
            configTeams.Add(team.Name, team);
            return team;
        }

        public ConfigScheduleRule CreateAndAddScheduleRule(ConfigScheduleRule newRule)
        {
            ConfigScheduleRule rule = new ConfigScheduleRule(newRule);
            configScheduleRules.Add(rule.Name, rule);
            return rule;
        }

        public ConfigScheduleRule CreateAndAddScheduleRule(League league, ConfigSeason season, string name, 
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



        //public GroupRule(League league, Playoff playoff, int ruleType, Division sortByDivision, Division fromDivision, String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, bool isHomeTeam, string groupIdentifier)

        public virtual void InsertData()
        {
            leagues = CreateObjects<League>(PrivateCreateLeagues, db.Leagues);
            configSeasons = CreateObjects<ConfigSeason>(PrivateCreateConfigSeasons, db.ConfigSeasons);
            configPlayoffs = CreateObjects<ConfigPlayoff>(PrivateCreateConfigPlayoffs, db.ConfigPlayoffs);
            configDivisions = CreateObjects<ConfigDivision>(PrivateCreateConfigDivisions, db.ConfigDivisions);
            configTeams = CreateObjects<ConfigTeam>(PrivateCreateConfigTeams, db.ConfigTeams);
            configSortingRules = CreateObjects<ConfigSortingRule>(PrivateCreateConfigSortingRules, db.ConfigSortingRules); 
            configGroups = CreateObjects<ConfigGroup>(PrivateCreateConfigGroups, db.ConfigGroups);
            configGroupRules = CreateObjects<ConfigGroupRule>(PrivateCreateConfigGroupRules, db.ConfigGroupRules);
            configSeriesRules = CreateObjects<ConfigSeriesRule>(PrivateCreateConfigSeriesRules, db.ConfigSeriesRules);
            configScheduleRules = CreateObjects<ConfigScheduleRule>(PrivateCreateScheduleRules, db.ScheduleRules);


            seasons = CreateObjects<Season>(PrivateCreateSeasons, db.Seasons);
            playoffs = CreateObjects<Playoff>(PrivateCreatePlayoffs, db.Playoffs);
            divisions = CreateObjects<Division>(PrivateCreateDivisions, db.Divisions);
            teams = CreateObjects<Team>(PrivateCreateTeams, db.Teams);
            sortingRules = CreateObjects<SortingRule>(PrivateCreateSortingRules, db.SortingRules); 
            groups = CreateObjects<Group>(PrivateCreateGroups, db.Groups);
            groupRules = CreateObjects<GroupRule>(PrivateCreateGroupRules, db.GroupRules); 
            seriesRules = CreateObjects<SeriesRule>(PrivateCreateSeriesRules, db.SeriesRules);
            series = CreateObjects<Series>(PrivateCreateSeries, db.Series);

        }

        public Dictionary<string, T> CreateObjects<T>(Action method, DbSet m)
        {
            var collection = new Dictionary<string, T>();

            method();

            m.AddRange(collection.Values);

            return collection;
        }


        public virtual void UpdateData()
        {

        }
    }
}
