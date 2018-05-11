using System.Data.Entity;
using JodyApp.Domain;

using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Database
{
    public class JodyAppContext:DbContext
    {        

        public const string HOME_PROD_CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jody;Integrated Security=True";
        public const string HOME_TEST_CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=jodytest;Integrated Security=True";
        public const string WORK_TEST_CONNECTION_STRING = "Data Source=localhost;Initial Catalog=JodyTest;Integrated Security=True";
        public const string WORK_PROD_CONNECTION_STRING = "Data Source=localhost;Initial Catalog=JodyTest;Integrated Security=True";

        public static string DATASOURCE_FORMAT = "Data Source={0};Initial Catalog={1};Integrated Security=True";
        public const string HOME_DATA_SOURCE= "(localdb)\\MSSQLLocalDB";
        public const string WORK_DATA_SOURCE = "localhost";
        public const string PROD = "Jody";
        public const string TEST = "JodyTest";
        public const int HOME_PROD = 0;
        public const int HOME_TEST = 1;
        public const int WORK_PROD = 2;
        public const int WORK_TEST = 3;

        public static int CURRENT_DATABASE = WORK_TEST;

        public static string GetDataSource(int location) 
        {
            switch(location)
            {
                case HOME_PROD:
                    return string.Format(DATASOURCE_FORMAT, HOME_DATA_SOURCE, PROD);                    
                case HOME_TEST:
                    return string.Format(DATASOURCE_FORMAT, HOME_DATA_SOURCE, TEST);                    
                case WORK_PROD:
                    return string.Format(DATASOURCE_FORMAT, WORK_DATA_SOURCE, TEST);
                case WORK_TEST:
                    return string.Format(DATASOURCE_FORMAT, WORK_DATA_SOURCE, TEST);
                default:
                    return GetDataSource(WORK_PROD);
            }            
        }
        //public JodyAppContext() : base("Data Source=localhost;Initial Catalog=JodyTest;Integrated Security=True") { }        
        public JodyAppContext() : this(CURRENT_DATABASE) { }
        public JodyAppContext(int type) : base(GetDataSource(type)) { }

        public JodyAppContext(string ConnectionString) : base(ConnectionString) { }

        public DbSet<Team> Teams { get; set; }
        public DbSet<League> Leagues { get; set; }          
        public DbSet<Division> Divisions { get; set; }
        public DbSet<ScheduleRule> ScheduleRules { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<SortingRule> SortingRules { get; set; }
        public DbSet<DivisionRank> DivisionRanks { get; set; }                                
        public DbSet<Season> Seasons { get; set; }

        public DbSet<TeamStatistics> TeamStatistics { get; set; }        
        public DbSet<SeriesRule> SeriesRules { get; set; }
        public DbSet<Series> Series { get; set; }
        public DbSet<Playoff> Playoffs { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRule> GroupRules { get; set; }
        public DbSet<ReferenceCompetition> ReferenceCompetitions { get; set; }

    }
}
