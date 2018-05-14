using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class ConfigService : BaseService
    {
        public override void Initialize(JodyAppContext db)
        {
            this.db = db;
        }

        public List<ConfigTeam> GetTeams(int currentYear)
        {
            return db.ConfigTeams.Where(team =>
            team.FirstYear != null &&
            team.FirstYear <= currentYear &&
            (team.LastYear == null || team.LastYear >= currentYear)
            ).ToList();
        }

        public List<ConfigDivision> GetDivisions(ConfigSeason season, int currentYear)
        {
            return db.ConfigDivisions.Where(division =>
            division.FirstYear != null &&
            division.FirstYear <= currentYear &&
            (division.LastYear == null || division.LastYear >= currentYear)
            ).ToList();
        }
    }
}
