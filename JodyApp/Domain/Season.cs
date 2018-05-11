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
    public class Season:DomainObject, Competition
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

        virtual public List<Division> Divisions { get; set; }

        virtual public List<ScheduleRule> ScheduleRules { get; set; }

        virtual public List<Game> Games { get; set; }

        virtual public League League { get; set; }

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
            games.ForEach(g => { PlayGame(g, random); ProcessGame(g); });
        }
        public void PlayGame(Game g, Random random)
        {
            g.Play(random);            
        }
        public void ProcessGame(Game g)
        {
            Standings.ProcessGame(g);
        }
        public List<Game> GetNextGames(int lastGameNumber)
        {
            return Games.Where(g => !g.Complete).ToList();
        }

        public bool IsComplete()
        {
            bool complete = true;

            complete = GetNextGames(-1).Count == 0;

            Complete = complete;

            return complete;
        }

        public void StartCompetition()
        {
            Started = true;
        }

    }
}
