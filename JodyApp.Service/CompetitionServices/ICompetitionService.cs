using JodyApp.Domain;
using JodyApp.Domain.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service.CompetitionServices
{
    public interface ICompetitionService
    {
        DomainObject GetById(int? id);
        List<DomainObject> GetByLeagueId(int leagueId);

        Competition CreateCompetition(ConfigCompetition reference, int year);
    }
}
