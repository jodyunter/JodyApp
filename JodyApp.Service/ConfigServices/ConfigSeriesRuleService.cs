using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigSeriesRuleService:BaseService<ConfigSeriesRule>
    {
        public override DbSet<ConfigSeriesRule> Entities => db.ConfigSeriesRules;

        public ConfigSeriesRuleService(JodyAppContext db) : base(db) { }        
        public ConfigSeriesRule CreateSeriesRule(ConfigCompetition playoff, string name, int round,
                                ConfigGroup homeTeamFromGroup, int homeTeamFromRank,
                                ConfigGroup awayTeamFromGroup, int awayTeamFromRank,
                                int seriesType, int gamesNeeded, bool canTie, string homeGames, int? firstYear, int? lastYear)
        {
            var rule = new ConfigSeriesRule(playoff, name, round, homeTeamFromGroup, homeTeamFromRank,
                                    awayTeamFromGroup, awayTeamFromRank, seriesType, gamesNeeded,
                                    canTie, homeGames, firstYear, lastYear);

            db.ConfigSeriesRules.Add(rule);
            return rule;
        }

        public List<ConfigSeriesRule> GetSeriesRules(ConfigCompetition playoff)
        {
            return db.ConfigSeriesRules.Where(series => series.Playoff.Id == playoff.Id).ToList();
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }
    }
}
