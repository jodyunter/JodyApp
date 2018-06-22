using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.CompetitionServices
{
    public class SeriesService : BaseService<Series>
    {
        public override DbSet<Series> Entities => db.Series;

        public SeriesService(JodyAppContext db) : base(db) { }

        public static BaseViewModel StaticDomainToDTO(DomainObject obj)
        {
            var series = (Series)obj;

            var gameList = new List<GameViewModel>();

            series.Games.ForEach(game =>
           {
            gameList.Add(ScheduleService.GameToDTO(series.Playoff, game));
           });

            
            return new SeriesViewModel((int)obj.Id, series.Name, series.Playoff.Name, series.Playoff.Year,
                series.GetWinner().Name, series.GetTeamWins(series.GetWinner()), series.GetLoser().Name, series.GetTeamWins(series.GetLoser()), series.Round, gameList);
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            return StaticDomainToDTO(obj);
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            throw new NotImplementedException();
        }

        public ListViewModel GetByPlayoffId(int playoffId)
        {
            return CreateListViewModelFromList(db.Series.Where(s => s.Playoff.Id == playoffId).ToList<DomainObject>(), DomainToDTO);
        }

        public ListViewModel GetBySeriesName(string seriesName)
        {
            return CreateListViewModelFromList(db.Series.Where(s => s.Name.Equals(seriesName)).ToList<DomainObject>(), DomainToDTO);
        }

        public ListViewModel GetByTeam(int configTeamId)
        {
            return CreateListViewModelFromList(db.Series.Where(s => s.HomeTeam.Parent.Id == configTeamId || s.AwayTeam.Parent.Id == configTeamId).ToList<DomainObject>(), StaticDomainToDTO);
        }
        public ListViewModel GetSeriesNames()
        {
            var stringList = db.Series.Select(s => s.Name).Distinct().ToList();

            var resultList = new List<ReferenceViewModel>();

            int count = 0;
            stringList.ForEach(s =>
            {
                resultList.Add(new ReferenceViewModel(count, s));
            });

            return new ListViewModel(resultList.ToList<BaseViewModel>());
        }
    }
}
