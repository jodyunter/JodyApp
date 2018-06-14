using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.CompetitionServices
{
    public class StandingsService:BaseService<DomainObject>
    {
        public SeasonService SeasonService { get; set; }        

        public override DbSet<DomainObject> Entities => throw new NotImplementedException();

        public StandingsService(JodyAppContext db) : base(db) { SeasonService = new SeasonService(db);}
        
        public ListViewModel GetModelsByTeam(int configTeamId)
        {
            return CreateListViewModelFromList(db.Teams.Where(t => t.Parent.Id == configTeamId).ToList<DomainObject>(), DomainToDTO);
        }
        public ListViewModel GetModelsBySeasonIdAndDivisionId(int seasonId, int divisionId)
        {            
            var division = db.Divisions.Where(d => d.Id == divisionId).FirstOrDefault();

            var recordModels = new List<BaseViewModel>();
            
            var teams = SeasonService.GetTeamsInDivisionByRank(division);
            
            teams.ForEach(team =>
            {
                var model = DomainToDTO(team, division);
                recordModels.Add(model);                
            });

            return new ListViewModel(recordModels);

        }

        public ListViewModel GetModelBySeasonAndDivisionLevel(int seasonId, int divisionLevel)
        {            
            var divisions = db.Divisions.Where(d => d.Level == divisionLevel && d.Season.Id == seasonId).OrderBy(d => d.Order);
            var recordModels = new List<BaseViewModel>();

            divisions.ToList().ForEach(division =>
            {
                var teams = SeasonService.GetTeamsInDivisionByRank(division);
                                            
                teams.ForEach(team =>
                {
                    var model = DomainToDTO(team, division);
                    recordModels.Add(model);                    
                });

            });
            return new ListViewModel(recordModels);
        }        

        public BaseViewModel DomainToDTO(Team team, Division divisionToListBy)
        {
            var model = new StandingsRecordViewModel(team.Stats.Rank, team.Season.League.Name, divisionToListBy.Name, team.Name, team.Stats.Wins, team.Stats.Loses, team.Stats.Ties, team.Stats.Points, team.Stats.GamesPlayed, team.Stats.GoalsFor, team.Stats.GoalsAgainst, team.Stats.GoalDifference);

            return model;
        }
        //we can get it by team but we have to have cases where we change items too
        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var team = (Team)obj;

            var model = new StandingsRecordViewModel(team.Stats.Rank, team.Season.League.Name, team.Division.Name, team.Name, team.Stats.Wins, team.Stats.Loses, team.Stats.Ties, team.Stats.Points, team.Stats.GamesPlayed, team.Stats.GoalsFor, team.Stats.GoalsAgainst, team.Stats.GoalDifference);

            return model;

        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }

    }
}
