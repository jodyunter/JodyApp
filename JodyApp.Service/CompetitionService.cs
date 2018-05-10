using JodyApp.Database;
using JodyApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Service
{
    public class CompetitionService:BaseService
    {
        public CompetitionService(JodyAppContext db) : base(db) { }
        
        public Competition GetReferenceCompetitionByName(League league, string name)
        {
            var list = new List<Competition>();

            list.AddRange(league.ReferenceCompetitions.Where(rc => rc.Playoff != null).Select(rc => rc.Playoff));
            list.AddRange(league.ReferenceCompetitions.Where(rc => rc.Season != null).Select(rc => rc.Season));

            return list.Where(comp => comp.Name == name).FirstOrDefault();
        }
    }
}
