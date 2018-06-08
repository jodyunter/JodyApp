using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.ConfigServices
{
    public class ConfigCompetitionService:BaseService
    {
        public ConfigCompetitionService(JodyAppContext db) : base(db) { }

        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            throw new NotImplementedException();
        }

        public override ListViewModel GetAll()
        {
            return CreateListViewModelFromList(db.ConfigCompetitions.ToList<DomainObject>(), DomainToDTO);
        }

        public override DomainObject GetById(int? id)
        {
            if (id == null) return null;

            return db.ConfigCompetitions.Where(c => c.Id == id).FirstOrDefault();
        }

        public override BaseViewModel GetModelById(int id)
        {
            return DomainToDTO(GetById(id));
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            var m = (ConfigCompetitionViewModel)model;

            var competition = (ConfigCompetition)GetById(m.Id);
            var leagueService = new LeagueService(db);

            var refComp = (ConfigCompetition)GetById(m.ReferenceCompetition.Id);
            var league = (League)leagueService.GetById(m.League.Id);

            var compType = 0;
            switch (m.CompetitionType)
            {
                case ConfigCompetitionViewModel.SEASON:
                    compType = ConfigCompetition.SEASON;
                    break;
                case ConfigCompetitionViewModel.PLAYOFF:
                    compType = ConfigCompetition.PLAYOFF;
                    break;
                default:
                    throw new Exception("Bad competition type.");
            }

            if (competition == null)
            { 
                competition = new ConfigCompetition(null, league, m.Name, compType, refComp, m.Order, m.FirstYear, m.LastYear);
                db.ConfigCompetitions.Add(competition);
            }
            else
            {
                var newCompetition = new ConfigCompetition(m.Id, league, m.Name, compType, refComp, m.Order, m.FirstYear, m.LastYear);                
                db.Entry(competition).CurrentValues.SetValues(newCompetition);

                newCompetition.League = league;
                newCompetition.Reference = refComp;
            }

            db.SaveChanges();

            return DomainToDTO(competition);
        }
    }
}
