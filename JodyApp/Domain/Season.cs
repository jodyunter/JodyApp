using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using JodyApp.Domain.Table;
using JodyApp.Domain.Schedule;
using JodyApp.Database;

namespace JodyApp.Domain
{
    public partial class Season:DomainObject, Competition
    {        
        public string Name { get; set; }  //may not be unique, name + year should be unique
        public int Year { get; set; }  //this is how we group everything together   
        public bool Started { get; set; }
        public bool Complete { get; set; }        
        public int StartingDay { get; set; }
        [NotMapped]
        public RecordTable Standings { get; set; }

        [InverseProperty("Season")]
        virtual public List<Team> TeamData { get; set; }

        virtual public List<ScheduleRule> ScheduleRules { get; set; }     
        
        virtual public List<Game> Games { get; set; }
                
        public League League { get; set; }

        public Season() { }
        public Season(League league, string name, int year, bool started, bool complete, int startingDay)
        {
            this.League = league;
            this.Name = name;
            this.Year = year;
            this.Started = started;
            this.Complete = complete;
            this.StartingDay = startingDay;
        }
        public void SetupStandings()
        {
            Standings = new RecordTable();
            TeamData.ForEach(team =>
           {
               Standings.Add(team);
           });
        }

        public void PlayGames(List<Game> games, Random random)
        {
            games.ForEach(g => PlayGame(g, random));
        }
        public void PlayGame(Game g, Random random)
        {
            g.Play(random);            
        }
        public void ProcessGame(Game g)
        {
            Standings.ProcessGame(g);
        }
        public List<Game> GetNextGames(JodyAppContext db)
        {
            return db.Games
                .Include("HomeTeam")
                .Include("AwayTeam")                
                .Where(g => g.Season.Id == this.Id && g.Complete == false).ToList();
        }

        public bool IsComplete(JodyAppContext db)
        {
            bool complete = true;

            if (db.Games.Where(g => g.Season.Id == this.Id && !g.Complete).ToList().Count > 0) complete = false;

            Complete = complete;

            return complete;
        }

        public void StartCompetition()
        {
            Started = true;
        }

    }
}
