using System.Data.Entity;
using JodyApp.Domain;

using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {        
        public JodyAppContext() : base("Data Source=localhost;Initial Catalog=jody;Integrated Security=True") { }
        //public JodyAppContext() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jody;Integrated Security=True") { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }          
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ScheduleRule> ScheduleRules { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<SortingRule> SortingRules { get; set; }
        public DbSet<DivisionRank> DivisionRanks { get; set; }                                
        public DbSet<Season> Seasons { get; set; }

        public DbSet<TeamStatistics> TeamStatistics { get; set; }        
        public DbSet<Series> PlayoffSeries { get; set; }
        public DbSet<Playoff> Playoffs { get; set; }
        public DbSet<GroupRule> GroupRule { get; set; }

    }
}
