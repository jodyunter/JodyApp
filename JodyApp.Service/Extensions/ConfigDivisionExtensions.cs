using JodyApp.Domain;
using JodyApp.Domain.Config;
using JodyApp.ViewModel;
using JodyApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public partial class ConfigService
    {

        public ListViewModel GetAllDivisions()
        {
            var divisions = new List<BaseViewModel>();

            db.ConfigDivisions.ToList().ForEach(d =>
            {
                divisions.Add(DomainToDTO(d));
            });

            return new ListViewModel(divisions);
        }
        public ConfigDivisionViewModel GetDivisionModelById(int id)
        {
            return DomainToDTO(GetDivisionById(id));
        }


        public ConfigDivisionViewModel DomainToDTO(ConfigDivision division)
        {
            if (division == null) return null;

            var teamViewModelList = new List<ConfigTeamViewModel>();

            division.Teams.ForEach(t =>
            {
                teamViewModelList.Add(DomainToDTO(t));
            });
            

            return new ConfigDivisionViewModel(division.Id,
                division.League == null ? "None" : division.League.Name,
                division.Name, division.ShortName,
                division.Parent == null ? "None" : division.Parent.Name,
                division.Level, division.Order, teamViewModelList);
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

        public override DomainObject GetById(int? id)
        {
            throw new NotImplementedException();
        }
    }
}
