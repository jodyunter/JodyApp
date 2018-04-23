﻿using System.Data.Entity;
using JodyApp.Domain;

using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table;

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
        public DbSet<Game> ScheduleGames { get; set; }
        public DbSet<SortingRule> SortingRules { get; set; }
        public DbSet<DivisionRank> DivisionRanks { get; set; }                                
        public DbSet<Season> Seasons { get; set; }

        public DbSet<TeamStatistics> TeamStatistics { get; set; }        

    }
}
