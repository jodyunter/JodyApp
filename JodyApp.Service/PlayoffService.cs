using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;

namespace JodyApp.Service
{
    public class PlayoffService
    {
        public PlayoffService(JodyAppContext db)
        {

        }

        public Playoff CreateNewPlayoff(League league, string name, int year)
        {
            Playoff playoff = new Playoff();
            playoff.League = league;
            playoff.Year = year;
            playoff.Name = name;

            //step one create and setup the rules properly.

            return null;

        }
    }
}
