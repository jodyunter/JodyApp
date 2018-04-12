using System.Data.Entity;
using JodyApp.Domain;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Config;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {        
        public JodyAppContext() : base("Data Source=localhost;Initial Catalog=jody;Integrated Security=True") { }
        //public JodyAppContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jody;Integrated Security=True") { }

        //Base configuration items, use a seperate concreat class so that we can query it easier
        public DbSet<BaseTeam> Teams { get; set; }        
        public DbSet<BaseDivision> Divisions { get; set; }
        public DbSet<BaseScheduleRule> ScheduleRules { get; set; }

        //Season Configuration Items
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonDivision> SeasonDivisions { get; set; }
        public DbSet<SeasonScheduleRule> SeasonScheduleRules { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public DbSet<TeamStatistics> TeamStatistics { get; set; }

    }
}
