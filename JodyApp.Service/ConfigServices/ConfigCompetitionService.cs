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
    public class ConfigCompetitionService:BaseService<ConfigCompetition>
    {
        public override DbSet<ConfigCompetition> Entities => db.ConfigCompetitions;

        public ConfigCompetitionService(JodyAppContext db) : base(db) { }

        private string GetCompetitionTypeString(int type)
        {
            switch(type)
            {
                case ConfigCompetition.SEASON:
                    return ConfigCompetitionViewModel.SEASON;
                case ConfigCompetition.PLAYOFF:
                    return ConfigCompetitionViewModel.PLAYOFF;
                default:
                    throw new ApplicationException("Bad type in Get Competition TypeString");
            }
        }

        private int GetCompetitionTypeNumber(string type)
        {
            switch (type)
            {
                case ConfigCompetitionViewModel.SEASON:
                    return ConfigCompetition.SEASON;
                case ConfigCompetitionViewModel.PLAYOFF:
                    return ConfigCompetition.PLAYOFF;
                default:
                    throw new ApplicationException("Bad type in Get Competition TypeString");
            }
        }
        public override BaseViewModel DomainToDTO(DomainObject obj)
        {
            var c = (ConfigCompetition)obj;


            var m = new ConfigCompetitionViewModel(c.Id, c.Name,
                c.League == null ? null : c.League.Id,
                c.League == null ? null : c.League.Name,
                GetCompetitionTypeString(c.Type),
                c.Reference == null ? null : c.Reference.Id,
                c.Reference == null ? null : c.Reference.Name,
                c.Order,
                c.FirstYear,
                c.LastYear);

            return m;
                
        }

        public override BaseViewModel Save(BaseViewModel model)
        {
            var m = (ConfigCompetitionViewModel)model;

            var competition = (ConfigCompetition)GetById(m.Id);
            var leagueService = new LeagueService(db);

            var refComp = (ConfigCompetition)GetById(m.ReferenceCompetition.Id);
            var league = (League)leagueService.GetById(m.League.Id);

            var compType = GetCompetitionTypeNumber(m.CompetitionType);

            if (competition == null)
            { 
                competition = CreateCompetition(league, m.Name, compType, refComp, m.Order, m.FirstYear, m.LastYear);                
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

        //should go to the save?
        public ConfigCompetition CreateCompetition(League league, string name, int type, ConfigCompetition reference, int order, int? firstYear, int? lastYear)
        {
            var newComp = new ConfigCompetition(null, league, name, type, reference, order, firstYear, lastYear);
            db.ConfigCompetitions.Add(newComp);

            return newComp;
        }


        public ConfigCompetition GetCompetitionByName(League league, string name)
        {
            return db.ConfigCompetitions.Where(c => c.League.Id == league.Id && c.Name == name).FirstOrDefault();
        }

        public List<ConfigCompetition> GetCompetitionsByLeague(League league, int year)
        {
            return db.ConfigCompetitions.Where(c =>
                    c.League.Id == league.Id &&
                    c.FirstYear != null && c.FirstYear <= year &&
                    (c.LastYear == null || c.LastYear >= year)).ToList();
        }



    }
}
