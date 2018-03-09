using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using System.Data.Entity;

namespace JodyApp.Service.DataFolder
{
    public class DataService
    {
        private JodyApp.Database.JodyAppContext db = new Database.JodyAppContext();
        private static DataService instance;        
        static string BASE_DIR = "C:\\Users\\jody_unterschutz\\source\\repos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";
        //string BASE_DIR = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";        
        string TeamFile = BASE_DIR + "TeamData.txt";
        string DivisionFile = BASE_DIR + "DivisionData.txt";
        TeamService teamService = new TeamService();
        DivisionService divisionService = new DivisionService();

        public static DataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataService();
                }
                return instance;
            }
        }

        public DataService()
        {

            LoadData(TeamFile, db.Teams, PopulateTeam);
            LoadData(DivisionFile, db.Divisions, PopulateDivision);
            
        }


        delegate T PopulateObject<T>(string[] input);

        Team PopulateTeam(string[] input)
        {
            int NAME = 0;
            int SKILL = 1;

            Team team = new Team
            {
                Name = input[NAME],
                Skill = int.Parse(input[SKILL])
            };

            return team;
        }

        Division PopulateDivision(string[] input)
        {
            int NAME = 0;
            int LEVEL = 2;
            int ORDER = 3;
            int PARENT = 1;
            int START_OF_TEAM_LIST = 4;

            Division division = new Division
            {
                Name = input[NAME],
                Level = int.Parse(input[LEVEL]),
                Order = int.Parse(input[ORDER]),
                Teams = new List<Team>(),
            };

            //get parent
            if (input[PARENT].Length != 0)
            {
                //get division by name
                //divisions must be in order of hierarchy for this to work
                Division parent = divisionService.GetByName(input[PARENT]);

                division.Parent = parent;
            }

            for (int i = START_OF_TEAM_LIST; i < input.Length; i++)
            {
                //find team, add to division add division to team
                Team team = teamService.GetTeamByName(input[i]);
                division.Teams.Add(team);
                team.Division = division;
            }

            return division;

        }

        void LoadData<T>(string fileName, DbSet<T> dataList, PopulateObject<T> populateObject) where T:class
        {
            StreamReader teamReader = new StreamReader(fileName);
            string line;

            line = teamReader.ReadLine(); //read header line!

            while ((line = teamReader.ReadLine()) != null)
            {
                string[] split = line.Split('\t');

                dataList.Add(populateObject(split));                
            }
        }

    }
}
