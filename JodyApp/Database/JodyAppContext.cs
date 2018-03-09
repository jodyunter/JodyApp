using System.Data.Entity;
using JodyApp.Domain;
using JodyApp.Domain.Season;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {
        public JodyAppContext() : base("Data Source=localhost;Initial Catalog=jody;Integrated Security=True") { }

        public DbSet<Team> Teams { get; set; }        
        public DbSet<Division> Divisions { get; set; }
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonDivision> SeasonDivisions { get; set; }
        public DbSet<Season> Seasons { get; set; }


    }
}
