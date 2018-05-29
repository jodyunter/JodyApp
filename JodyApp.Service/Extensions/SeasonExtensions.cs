using JodyApp.Domain;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class SeasonService
    {

        public ListViewModel GetAllByLeagueId(int leagueId)
        {
            var modelList = new List<BaseViewModel>();

            db.Seasons.Where(s => s.League.Id == leagueId).ToList().ForEach(season =>
            {
                modelList.Add(new SeasonViewModel(season.Id, season.League.Name, season.Name, season.Year, season.Started, season.Complete, season.StartingDay, new List<StandingsRecordViewModel>()));
            });

            return new ListViewModel(modelList);
        }

        public SeasonViewModel DomainToDTO(Season season)
        {
            if (season == null) return null;
            return new SeasonViewModel(season.Id, season.League.Name, season.Name, season.Year, season.Complete, season.Started, season.StartingDay,null);
        }


        public override BaseViewModel GetModelById(int id)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override BaseViewModel Save(BaseViewModel mdoel)
        {
            throw new NotImplementedException();
        }
        public override ListViewModel GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
