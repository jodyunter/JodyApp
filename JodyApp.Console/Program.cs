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
using JodyApp.Domain.Playoffs;
using JodyApp.Domain.Playoffs.Display;

namespace JodyApp.Console
{
    class Program
    {

        static void Main(string[] args)
        {
            JodyAppContext db = new JodyAppContext(JodyAppContext.HOME_TEST) ;
            JodyTestDataDriver driver = new JodyTestDataDriver(db);
            driver.DeleteAllData();            
            TeamService teamService = new TeamService(db);
            SeasonService seasonService = new SeasonService(db);
            ScheduleService scheduleService = new ScheduleService(db);
            DivisionService divisionService = new DivisionService(db);
            PlayoffService playoffService = new PlayoffService(db);


            int year = 0;
            if (db.Seasons.ToList().Count <= 0)
            {
                year = 0;
                driver.InsertData();
            } 
            else
            {
                year = db.Seasons.Max(s => s.Year);
            }

            year++;

            Random random = new Random();            

            League league = db.Leagues.Where(l => l.Name == "Jody League").First();
            Season season = seasonService.CreateNewSeason(league, "My Season", year);            
            var nextGames = seasonService.GetNextGames(season);
            seasonService.PlayGames(season, nextGames, random);            
            seasonService.SortAllDivisions(season);            
            db.SaveChanges();

            var div = db.Divisions.Where(d => d.Season.Id == season.Id && d.Name == "League").First();



            var teams = seasonService.GetTeamsInDivisionByRank(div);
            teams.Sort((a, b) => a.Stats.Rank.CompareTo(b.Stats.Rank));

            System.Console.WriteLine(RecordTableDisplay.PrintDivisionStandings("League", teams.ToList<Team>()));
            
            Playoff p = playoffService.CreateNewPlayoff(league, season, "Playoff", year);

            var pGames = new List<Game>();
            while (!p.Complete)
            {
                int currentRound = p.CurrentRound;
                currentRound = currentRound == 0 ? 1 : currentRound;
                playoffService.PlayRound(p, random);
                System.Console.WriteLine("Round " + currentRound + ":");
                p.GetSeriesForRound(currentRound).ForEach(series => System.Console.WriteLine(PlayoffDisplay.PrintSeriesSummary(series)));
            }
            

            db.SaveChanges();

            System.Console.WriteLine("\n");
            System.Console.WriteLine("Champion List");
            db.Series.Include("HomeTeam").Include("AwayTeam").Include("Games").Include("Playoff").Where(s => s.Name == "Final").OrderByDescending(s => s.Playoff.Year).ToList().ForEach(series =>
            {
                System.Console.WriteLine(series.Playoff.Year + " : " + series.GetWinner().Name);
            });


            teamService.SetNewSkills(random);

            db.SaveChanges();

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

            

        }

    }
}
