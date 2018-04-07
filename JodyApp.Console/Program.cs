using System;
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
            int ROUNDS_TO_PLAY = 10;

            Random random = new Random();
            System.Console.WriteLine("TEST ME OUT");

            List<Team> teams = new List<Team>();
            teams = teamService.GetAllTeams();
            RecordTable table = new RecordTable();
            teams.ForEach(team =>
            {
                table.Standings.Add(team.Name, new RecordTableTeam(team));
            });

            table.TableName = "Standings";

            RecordTableTeam[] teamList = table.Standings.Values.ToArray<RecordTableTeam>();

            for (int k = 0; k < ROUNDS_TO_PLAY; k++) {
                for (int i = 0; i < teamList.Length - 1; i++)
                {
                    RecordTableTeam homeTeam = teamList[i];

                    for (int j = i + 1; j < teamList.Length; j++)
                    {
                        RecordTableTeam awayTeam = teamList[j];

                        Game game = new Game
                        {
                            HomeTeam = homeTeam,
                            AwayTeam = awayTeam

                        };

                        game.Play(random);
                        //todo need to simplify this
                        table.ProcessGame(game);
                    }


                }
            }

            //Array.Sort(teamList, StandingsSorter.SortByDivisionLevel_0);
            
            
            System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORT_BY_LEAGUE));
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORTY_BY_CONFERENCE));
            //System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(table, StandingsSorter.SORT_BY_DIVISION));


            Season season = seasonService.CreateNewSeason("My Season", 1);


            RecordTable seasonTable = season.Standings;
            seasonTable = new RecordTable();

            season.TeamData.ForEach(team =>
            {
                seasonTable.Standings.Add(team.Name, team);
            });

            List<ScheduleGame> scheduleGames = new List<ScheduleGame>();

            season.ScheduleRules.ForEach(rule =>
            {
                
            });
            System.Console.WriteLine(RecordTableDisplay.PrintRecordTable(seasonTable, StandingsSorter.SORT_BY_LEAGUE));

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

        }
    }
}
