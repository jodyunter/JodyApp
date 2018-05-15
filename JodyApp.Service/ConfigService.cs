using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Config;

namespace JodyApp.Service
{
    public class ConfigService : BaseService
    {

        public ConfigService():base() { Initialize(null); }
        public ConfigService(JodyAppContext db) : base(db) { Initialize(db); }
        public override void Initialize(JodyAppContext db)
        {
            this.db = db;
        }

        public List<ConfigTeam> GetTeams(League league, int currentYear)
        {
            return db.ConfigTeams.Where(team =>            
            team.League.Id == league.Id && 
            team.FirstYear != null &&
            team.FirstYear <= currentYear &&
            (team.LastYear == null || team.LastYear >= currentYear)
            ).ToList();
        }

        public List<ConfigDivision> GetDivisions(ConfigCompetition season, int currentYear)
        {
            return db.ConfigDivisions.Where(division =>
            division.FirstYear != null &&
            division.FirstYear <= currentYear &&
            (division.LastYear == null || division.LastYear >= currentYear)
            ).ToList();
        }

        public void SetNewSkills(League league, Random random)
        {
            GetTeams(league, league.CurrentYear).ForEach(team =>
            {
                int num = random.Next(0, 9);
                if (num < 2) team.Skill -= 1;
                if (num > 7) team.Skill += 1;
                if (team.Skill > 10) team.Skill = 10;
                if (team.Skill < 1) team.Skill = 1;
            });
        }

        public void ChangeDivision(ConfigTeam team, string newDivisionName)
        {
            //not good enough for division            
            team.Division = db.ConfigDivisions.Where(d => d.Name == newDivisionName).FirstOrDefault();

            db.SaveChanges();
        }
    }
}
