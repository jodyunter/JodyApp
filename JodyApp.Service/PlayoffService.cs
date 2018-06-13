using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.Domain.Playoffs;
using JodyApp.Service.ConfigServices;
using JodyApp.ViewModel;

namespace JodyApp.Service
{
    public class PlayoffService : BaseService<Playoff>
    {

        ConfigGroupRuleService ConfigGroupRuleService { get; set; }
        ConfigSeriesRuleService ConfigSeriesRuleService { get; set; }

        public override DbSet<Playoff> Entities => db.Playoffs;

        public PlayoffService(JodyAppContext db) :base(db)
        {
            ConfigGroupRuleService = new ConfigGroupRuleService(db);
            ConfigSeriesRuleService = new ConfigSeriesRuleService(db);
        }

        public Playoff CreateNewPlayoff(ConfigCompetition referencePlayoff, int year)
        {
            return CreateNewPlayoff(referencePlayoff, year, false);
        }
        public Playoff CreateNewPlayoff(ConfigCompetition referencePlayoff, int year, bool test)
        {
            Playoff playoff = new Playoff();
            playoff.League = referencePlayoff.League;
            playoff.Year = year;
            playoff.Name = referencePlayoff.Name;

            //change to a service call, we currently assume it is always a season
            Season season = db.Seasons.Where(s => s.Year == year && referencePlayoff.Reference.Name == s.Name).FirstOrDefault();
            playoff.Season = season;

            List<SeriesRule> newSeriesRules = new List<SeriesRule>();
            List<Group> newGroups = new List<Group>();            
            List<Series> newSeries = new List<Series>();

            var activeConfigGroups = ConfigGroupRuleService.GetGroups(referencePlayoff).Where(grp => grp.IsActive(year)).ToList();                                

            activeConfigGroups.ForEach(group =>            
            {
                Group newGroup = new Group(group.Name, playoff,
                    group.SortByDivision != null ? season.Divisions.Where(d => d.Name == group.SortByDivision.Name).FirstOrDefault() : null,
                    new List<GroupRule>());
                
                group.GroupRules.Where(gr => gr.IsActive(year)).ToList().ForEach(groupRule =>
                {
                    GroupRule newGroupRule = new GroupRule(groupRule,
                        groupRule.FromDivision != null ? season.Divisions.Where(d => d.Name == groupRule.FromDivision.Name).FirstOrDefault() : null,
                        groupRule.FromTeam != null ? season.TeamData.Where(team => team.Name == groupRule.FromTeam.Name).FirstOrDefault() : null,
                        newGroup);

                    newGroup.GroupRules.Add(newGroupRule);                    
                });
           
                newGroups.Add(newGroup);                
            });
            db.Groups.AddRange(newGroups);


            var activeConfigSeriesRules = ConfigSeriesRuleService.GetSeriesRules(referencePlayoff).Where(series => series.IsActive(year)).ToList();

            activeConfigSeriesRules.ForEach(seriesRule =>            
            {
                SeriesRule newRule = new SeriesRule(playoff, seriesRule.Name, seriesRule.Round,
                    newGroups.Where(ng => ng.Name == seriesRule.HomeTeamFromGroup.Name).First(), seriesRule.HomeTeamFromRank,
                    newGroups.Where(ng => ng.Name == seriesRule.AwayTeamFromGroup.Name).First(), seriesRule.AwayTeamFromRank,
                    seriesRule.SeriesType, seriesRule.GamesNeeded, seriesRule.CanTie, seriesRule.HomeGames);
                newSeriesRules.Add(newRule);
            });

            //setup series   
            newSeriesRules.ForEach(seriesRule =>
            {
                Series series = new Series(seriesRule.Playoff, seriesRule, null, 0, null, 0, new List<Game>(), seriesRule.Name);
                newSeries.Add(series);
            });

            playoff.Series = newSeries;
            playoff.Groups = newGroups;

            db.SeriesRules.AddRange(newSeriesRules);            
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
            return db.Series.Where(s => s.Name == name && s.Playoff.Year == year).FirstOrDefault();
        }

        public List<Series> GetSeries(string name)
        {            
            return db.Series.Where(s => s.Name == name).ToList();
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var playoff = (Playoff)obj;            
            return new PlayoffViewModel(playoff.Id, playoff.League.Id, playoff.League.Name, playoff.Name, playoff.Year, "Season", playoff.Started, playoff.Complete, playoff.StartingDay);
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

        public ListViewModel GetSeriesModelsByPlayoffId(int playoffId)
        {
            var seriesService = new SeriesService(db);
            return seriesService.CreateListViewModelFromList(db.Series.Where(s => s.Playoff.Id == playoffId).ToList<DomainObject>(), seriesService.DomainToDTO);
        }

    }

}
