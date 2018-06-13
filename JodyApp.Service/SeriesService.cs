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

namespace JodyApp.Service
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
            gameList.Add(CompetitionService.GameToDTO(series.Playoff, game));
           });

            return new SeriesViewModel((int)obj.Id, series.Name, series.Playoff.Name, series.Playoff.Year,
                series.HomeTeam.Name, series.HomeWins, series.AwayTeam.Name, series.AwayWins, series.Round, gameList);
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
    }
}
