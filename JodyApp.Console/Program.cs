using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Console.Setup;
using JodyApp.Console.Display;

using JodyApp.Domain.Table;
using JodyApp.Domain;

namespace JodyApp.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            System.Console.WriteLine("TEST ME OUT");

            RecordTable table = LoadData.GetStandings();

            RecordTableTeam[] teamList = table.Standings.Values.ToArray<RecordTableTeam>();

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
                    table.ProcessGame(game);
                }


            }

            List<RecordTableTeam> teams = table.Standings.Values.ToList<RecordTableTeam>();
            teams.Sort();
            teams.Reverse();

            System.Console.WriteLine(RecordTableDisplay.GetRecordTableRowHeader());
            foreach (RecordTableTeam team in teams)
            {
                System.Console.WriteLine(RecordTableDisplay.GetRecordTableRow(team));
            }

            System.Console.WriteLine("Press ENTER to end program.");
            System.Console.ReadLine();

        }
    }
}
