﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Service;
using JodyApp.Domain.Table;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder.Jody;

using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table.Display;

namespace JodyApp.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            JodyAppContext db = new JodyAppContext();
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            driver.DeleteAllData();
            driver.InsertData();
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);


            Random random = new Random();

            //Array.Sort(teamList, StandingsSorter.SortByDivisionLevel_0);


            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORTY_BY_CONFERENCE));
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORT_BY_DIVISION));

            League league = db.Leagues.Where(l => l.Name == "Jody League").First();
            Season season = seasonService.CreateNewSeason(league, "My Season", 1);

            season.SetupStandings();

            List<Game> scheduleGames = scheduleService.CreateGamesFromRules(season.ScheduleRules);

            scheduleGames.ForEach(game =>
           {
               game.Play(random);
               season.Standings.ProcessGame(game);
           });

            seasonService.SortAllDivisions(season);
            db.SaveChanges();

            var div = db.Divisions.Where(d => d.Season.Id == season.Id && d.Name == "League").First();



            var teams = seasonService.GetTeamsInDivisionByRank(div);
            teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

            System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("League", teams.ToList<Team>()));

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }
    }
}
