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

        public Playoff CreateNewPlayoff(Playoff referencePlayoff, int year)
        {
            return CreateNewPlayoff(referencePlayoff, year, false);
        }
        public Playoff CreateNewPlayoff(Playoff referencePlayoff, int year, bool test)
        {
            Playoff playoff = new Playoff();
            playoff.League = referencePlayoff.League;
            playoff.Year = year;
            playoff.Name = referencePlayoff.Name;

            //change to a service call
            Season season = db.Seasons.Where(s => s.Year == year && referencePlayoff.Season.Name == s.Name).FirstOrDefault();
            playoff.Season = season;

            List<SeriesRule> newSeriesRules = new List<SeriesRule>();
            List<GroupRule> newGroupRules = new List<GroupRule>();
            List<Series> newSeries = new List<Series>();

            GetSeriesRulesByReference(referencePlayoff).ForEach(seriesRule =>
            {
                SeriesRule newRule = new SeriesRule(seriesRule, playoff);
                newSeriesRules.Add(newRule);

            });

            GetGroupRulesByReference(referencePlayoff).ForEach(groupRule =>
            {
                var newRule = CreateNewGroupRule(groupRule, referencePlayoff.League, season, playoff);
                newGroupRules.Add(newRule);           
            });

            //setup series   
            newSeriesRules.ForEach(seriesRule =>
            {
                Series series = new Series(seriesRule.Playoff, seriesRule, null, 0, null, 0, new List<Game>(), seriesRule.Name);
                newSeries.Add(series);
            });

            playoff.Series = newSeries;
            playoff.GroupRules = newGroupRules;

            db.SeriesRules.AddRange(newSeriesRules);
            db.GroupRules.AddRange(newGroupRules);
            db.Series.AddRange(newSeries);
            
            season.TeamData.ForEach(team =>
            {
                playoff.GetOrSetupPlayoffTeam(team);
            });
            //setup playoff teams
            db.Teams.AddRange(playoff.PlayoffTeams);
            if (!test)
            {
                db.SaveChanges();
            }            

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
        public List<SeriesRule> GetSeriesRulesByReference(Playoff playoff)
        {
            return SeriesRule.GetByReference(db, playoff);
        }

        public List<GroupRule> GetGroupRulesByReference(Playoff playoff)
        {
            return GroupRule.GetByReference(db, playoff);
        }

        public List<Game> PlayRound(Playoff p, Random random)
        {
            var pGames = new List<Game>();

            if (!p.Started)
            {
                p.NextRound();
                db.SaveChanges();
            }            
            while (!p.IsRoundComplete(p.CurrentRound))
            {
                pGames.AddRange(p.GetNextGamesForRound(p.CurrentRound, 0));

                pGames.Where(g => !g.Complete).ToList().ForEach(game =>
                {
                    game.Play(random);
                    p.ProcessGame(game);
                });
            }            

            p.NextRound();
            db.SaveChanges();

            return pGames;
        }

        public Series GetSeriesByYear(string name, int year)
        {
            return db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == name && s.Playoff.Year == year).FirstOrDefault();
        }

        public List<Series> GetSeries(string name)
        {            
            return db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == name).ToList();
        }
       

    }

}
