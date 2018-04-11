﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Service;
using JodyApp.Console.Display;

using JodyApp.Domain.Table;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Service.Test.DataFolder;
using JodyApp.Domain.Season;
using JodyApp.Domain.Schedule;

namespace JodyApp.Console
{
    class Program
    {
        
        static void Main(string[] args)
        {
            JodyAppContext db = new JodyAppContext();
            BaseTestDataDriver.DeleteAllData(db);
            BaseTestDataDriver.InsertData(db);
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);

            int ROUNDS_TO_PLAY = 10;

            Random random = new Random();

            //Array.Sort(teamList, StandingsSorter.SortByDivisionLevel_0);
            
                        
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORTY_BY_CONFERENCE));
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORT_BY_DIVISION));


            Season season = seasonService.CreateNewSeason("My Season", 1);


            season.SetupStandings();

            List<ScheduleGame> scheduleGames = scheduleService.CreateGamesFromRules(season.ScheduleRules);

            scheduleGames.ForEach(game =>
           {
               game.Play(random);
               season.Standings.ProcessGame(game);
           });
            System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(season.Standings, StandingsSorter.SORT_BY_LEAGUE));

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

        }
    }
}
