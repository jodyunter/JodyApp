using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class StandingsService:BaseService<DomainObject>
    {
        public SeasonService SeasonService { get; set; }

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public StandingsService(JodyAppContext db) : base(db) { SeasonService = new SeasonService(db); }
        public StandingsService(JodyAppContext db, SeasonService seasonService) : base(db) { SeasonService = seasonService; }

        public ListViewModel GetBySeasonAndDivisionLevel(Season season, int divisionLevel)
        {
            SeasonService.SortAllDivisions(season);
            var divisions = season.Divisions.Where(d => d.Level == divisionLevel).OrderBy(d => d.Order);
            var recordModels = new List<BaseViewModel>();

            divisions.ToList().ForEach(division =>
            {
                var teams = SeasonService.GetTeamsInDivisionByRank(division);
                
                int i = 1;
                teams.ForEach(team =>
                {
                    var model = new StandingsRecordViewModel(i, season.League.Name, division.Name, team.Name, team.Stats.Wins, team.Stats.Loses, team.Stats.Ties, team.Stats.Points, team.Stats.GamesPlayed, team.Stats.GoalsFor, team.Stats.GoalsAgainst, team.Stats.GoalDifference);
                    recordModels.Add(model);
                    i++;
                });

            });
            return new ListViewModel(recordModels);
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

    }
}
