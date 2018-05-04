using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Service
{
    public class PlayoffService:BaseService
    {
        DivisionService divisionService;

        public PlayoffService(JodyAppContext db):base(db)
        {
            divisionService = new DivisionService(db);
        }

        public Playoff CreateNewPlayoff(League league, Season season, string name, int year)
        {
            Playoff playoff = new Playoff();
            playoff.League = league;
            playoff.Year = year;
            playoff.Name = name;

            List<SeriesRule> newSeriesRules = new List<SeriesRule>();
            List<GroupRule> newGroupRules = new List<GroupRule>();
            List<Series> newSeries = new List<Series>();

            GetSeriesRulesByLeague(league).ForEach(seriesRule =>
            {
                SeriesRule newRule = new SeriesRule(seriesRule, playoff);
                newSeriesRules.Add(newRule);

            });

            GetGroupRulesByLeague(league).ForEach(groupRule =>
            {
                var newRule = CreateNewGroupRule(groupRule, league, season, playoff);
                newGroupRules.Add(newRule);           
            });

            //setup series   
            newSeriesRules.ForEach(seriesRule =>
            {
                Series series = new Series(seriesRule.Playoff, seriesRule, null, null, new List<Game>(), seriesRule.Name);
                newSeries.Add(series);
            });

            playoff.Series = newSeries;
            playoff.GroupRules = newGroupRules;

            db.SeriesRules.AddRange(newSeriesRules);
            db.GroupRules.AddRange(newGroupRules);
            db.Series.AddRange(newSeries);
            db.SaveChanges();

            return playoff;

        }

        public GroupRule CreateNewGroupRule(GroupRule groupRule, League league, Season season, Playoff playoff)
        {
            Division sortByDiv = null;
            Division fromDiv = null;
            Team team = null;

            if (groupRule.SortByDivision != null) sortByDiv = divisionService.GetByLeagueAndSeasonAndName(league, season, groupRule.SortByDivision.Name);
            if (groupRule.FromDivision != null) fromDiv = divisionService.GetByLeagueAndSeasonAndName(league, season, groupRule.FromDivision.Name);

            if (groupRule.FromTeam != null)
            {
                team = groupRule.FromTeam.Parent == null ? groupRule.FromTeam : groupRule.FromTeam.Parent;
            }

            GroupRule newRule = new GroupRule(groupRule, sortByDiv, fromDiv, team, playoff);

            return newRule;
        }
        public List<SeriesRule> GetSeriesRulesByLeague(League league)
        {
            return SeriesRule.GetByLeague(db, league, null);
        }

        public List<GroupRule> GetGroupRulesByLeague(League league)
        {
            return GroupRule.GetByLeague(db, league, null);
        }
    }
}
