using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.Test.DataFolder
{
    public abstract class AbstractTestDataDriver
    {
        JodyAppContext db;
        String LeagueName = "Abstract League Name";

        public AbstractTestDataDriver(JodyAppContext db)
        {
            this.db = db;
        }
        abstract public void PrivateCreateTeams( Dictionary<string, Team> teams, Dictionary<string, Division> divs);
        abstract public void PrivateCreateDivisions(Dictionary<string, League> leagues, Dictionary<string, Division> divs);
        abstract public void PrivateCreateScheduleRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, Team> teams, Dictionary<string, ScheduleRule> rules);
        abstract public void PrivateCreateLeagues(Dictionary<string, League> leagues);
        public abstract void PrivateCreateSortingRules(Dictionary<string, Division> divs, Dictionary<string, SortingRule> rules);
        public abstract void PrivateCreateSeriesRules(Dictionary<string, League> leagues, Dictionary<string, SeriesRule> rules);
        public abstract void PrivateCreateGroupRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, GroupRule> rules);
        public void DeleteAllData()
        {
            string[] tables = {"GroupRules", "Games", "SortingRules", "DivisionRanks", "ScheduleRules","Series", "Teams", "TeamStatistics", "SeriesRules", "Divisions", "Seasons", "Playoffs", "Leagues"};
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public Division CreateAndAddDivision(League league, string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules, Dictionary<string, Division> map)
        {
            Division div = new Division(league, name, shortName, level, order, parent, sortingRules);
            map.Add(div.Name, div);
            return div;
        }

        public Team CreateAndAddTeam(string name, int skill, Division division, Dictionary<string, Team> map)
        {
            Team team = new Team(name, skill, division);
            map.Add(team.Name, team);
            division.Teams.Add(team);
            return team;
        }        
        
        public ScheduleRule CreateAndAddScheduleRule(ScheduleRule newRule, Dictionary<string, ScheduleRule> map)
        {
            ScheduleRule rule = new ScheduleRule(newRule);
            map.Add(rule.Name, rule);
            return rule;
        }

        public ScheduleRule CreateAndAddScheduleRule(League league, string name, 
                                int homeType, Team homeTeam, Division homeDivision,
                                int awayType, Team awayTeam, Division awayDivision,
                                bool homeAndAway, int rounds, int divisionLevel, int order,
                                    Dictionary<string, ScheduleRule> map)
        {
            ScheduleRule rule = new ScheduleRule(league, name, homeType, homeTeam, homeDivision, awayType, awayTeam, awayDivision, homeAndAway, rounds, divisionLevel, order);
            map.Add(rule.Name, rule);
            return rule;
        }

        public SeriesRule CreateAndAddSeriesRule(League league, string name, int round, string homeTeamFromGroup, int homeTeamFromRank,
                                            string awayTeamFromGroup, int awayTeamFromRank, int seriesType,
                                            int gamesNeeded, bool canTie, string homeGames, Dictionary<string, SeriesRule> map)
        {
            SeriesRule rule = new SeriesRule(league, null, name, round, homeTeamFromGroup, homeTeamFromRank, awayTeamFromGroup, awayTeamFromRank, seriesType, gamesNeeded, canTie, homeGames);
            map.Add(rule.Name, rule);
            return rule;
            
        }

        public SortingRule CreateAndAddSortingRule(Division division, String name, int groupNumber, Division divToGetTeamsFrom, string positionsToUse, int divisionLevel, int ruleType, Dictionary<string, SortingRule> map)
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

            map.Add(rule.Name, rule);
            return rule;
        }

        public League CreateAndAddLeague(String name, Dictionary<string, League> leagues)
        {
            League l = new League() { Name = name };

            leagues.Add(l.Name, l);

            return l;
        }

        public GroupRule CreateAndAddGroupRule(League league, int ruleType, Division sortByDivision, Division fromDivision, 
                                                String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, bool isHomeTeam, 
                                                string groupIdentifier, Dictionary<string, GroupRule> map)
        {
            GroupRule rule = new GroupRule(league, null, ruleType, sortByDivision, fromDivision, seriesName, fromStartValue, fromEndValue, fromTeam, isHomeTeam, groupIdentifier);

            map.Add(map.Keys.Count.ToString(), rule);            
            return rule;
        }

        public GroupRule CreateAndAddGroupRule(GroupRule rule, Dictionary<string, GroupRule> map)
        {
            map.Add(map.Keys.Count.ToString(), rule);

            return rule;
        }

        public Dictionary<string, League> CreateLeagues()
        {
            var leagues = new Dictionary<string, League>();
            PrivateCreateLeagues(leagues);

            db.Leagues.AddRange(leagues.Values);
            db.SaveChanges();

            return leagues;

                
        }

        public Dictionary<string, Division> CreateDivisions(Dictionary<string, League> leagues)
        {
            var divs = new Dictionary<string, Division>();

            PrivateCreateDivisions(leagues, divs);

            db.Divisions.AddRange(divs.Values);
            db.SaveChanges();

            return divs;

        }


        public Dictionary<string, Team> CreateTeams(Dictionary<string, Division> divs)
        {
            var teams = new Dictionary<string, Team>();

            PrivateCreateTeams(teams, divs);

            db.Teams.AddRange(teams.Values);
            db.SaveChanges();

            return teams;
        }

        public Dictionary<string, ScheduleRule> CreateRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs, Dictionary<string, Team> teams)
        {
            var rules = new Dictionary<string, ScheduleRule>();

            PrivateCreateScheduleRules(leagues, divs, teams, rules);
            
            db.ScheduleRules.AddRange(rules.Values);
            db.SaveChanges();
            return rules;

        }

        public Dictionary<string, SortingRule> CreateSortingRules(Dictionary<string, Division> divs)
        {
            var rules = new Dictionary<string, SortingRule>();

            PrivateCreateSortingRules(divs, rules);

            db.SortingRules.AddRange(rules.Values);
            db.SaveChanges();

            return rules;
        }

        public Dictionary<string, SeriesRule> CreateSeriesRules(Dictionary<string, League> leagues)
        {
            var rules = new Dictionary<string, SeriesRule>();

            PrivateCreateSeriesRules(leagues, rules);

            db.SeriesRules.AddRange(rules.Values);
            db.SaveChanges();

            return rules;
        }

        public Dictionary<string, GroupRule> CreateGroupRules(Dictionary<string, League> leagues, Dictionary<string, Division> divs)
        {
            var rules = new Dictionary<string, GroupRule>();

            PrivateCreateGroupRules(leagues, divs, rules);

            db.GroupRules.AddRange(rules.Values);
            db.SaveChanges();

            return rules;
        }        



        //public GroupRule(League league, Playoff playoff, int ruleType, Division sortByDivision, Division fromDivision, String seriesName, int fromStartValue, int fromEndValue, Team fromTeam, bool isHomeTeam, string groupIdentifier)

        public void InsertData()
        {
            Dictionary<string, League> leagues = CreateLeagues();
            //create divisions
            Dictionary<string, Division> divs = CreateDivisions(leagues);
            Dictionary<string, Team> teams = CreateTeams(divs);
            Dictionary<string, ScheduleRule> scheduleRules = CreateRules(leagues, divs, teams);
            Dictionary<string, SortingRule> sortingRules = CreateSortingRules(divs);
            Dictionary<string, SeriesRule> seriesRules = CreateSeriesRules(leagues);
            Dictionary<string, GroupRule> groupRules = CreateGroupRules(leagues, divs);
        }
    }
}
