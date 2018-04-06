using System.Data.Entity;
using JodyApp.Domain;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {        
        //public JodyAppContext() : base("Data Source=localhost;Initial Catalog=jody;Integrated Security=True") { }
        public JodyAppContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jody;Integrated Security=True") { }

        public DbSet<Team> Teams { get; set; }        
        public DbSet<Division> Divisions { get; set; }
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonDivision> SeasonDivisions { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TeamStatistics> TeamStatistics { get; set; }
        public DbSet<ScheduleRule> ScheduleRules { get; set; }


    }
}
