using System.Data.Entity;
using JodyApp.Domain;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Config;
using JodyApp.Domain.Table;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {        
        public JodyAppContext() : base("Data Source=localhost;Initial Catalog=jody;Integrated Security=True") { }
        //public JodyAppContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jody;Integrated Security=True") { }

        //Base configuration items, use a seperate concreat class so that we can query it easier
        public DbSet<League> Leagues { get; set; }
        public DbSet<ConfigTeam> Teams { get; set; }        
        public DbSet<ConfigDivision> Divisions { get; set; }
        public DbSet<ConfigScheduleRule> ScheduleRules { get; set; }
        public DbSet<ScheduleGame> ScheduleGames { get; set; }
        public DbSet<SortingRule> SortingRules { get; set; }
        public DbSet<DivisionRank> DivisionRanks { get; set; }
        //Season Configuration Items
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonDivision> SeasonDivisions { get; set; }
        public DbSet<SeasonScheduleRule> SeasonScheduleRules { get; set; }
        public DbSet<SeasonGame> SeasonGames { get; set; }
        public DbSet<Season> Seasons { get; set; }

        public DbSet<TeamStatistics> TeamStatistics { get; set; }        

    }
}
