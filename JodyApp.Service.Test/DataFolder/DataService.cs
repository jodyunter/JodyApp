using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JodyApp.Domain;
using JodyApp.Domain.Table;
using JodyApp.Database;
using System.Data.Entity;

namespace JodyApp.Service.DataFolder
{
    public class DataService:BaseService
    {        
        private static DataService instance;        
        static string BASE_DIR = "C:\\Users\\jody_unterschutz\\source\\repos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";
        //string BASE_DIR = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";        
        string TeamFile = BASE_DIR + "TeamData.txt";
        string DivisionFile = BASE_DIR + "DivisionData.txt";
        TeamService teamService = null;
        DivisionService divisionService = null;

        public static DataService Instance(JodyAppContext context)
        {
            if (instance == null)
            {
                instance = new DataService(context);
            }
            return instance;
            
        }

        public void DeleteAllData()
        {
            string[] tables = { "Seasons" ,"Teams", "TeamStatistics", "Divisions"};
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public DataService(JodyAppContext context):base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);
            DeleteAllData();

            LoadData(TeamFile, db.Teams, PopulateTeam);
            LoadData(DivisionFile, db.Divisions, PopulateDivision);

            db.SaveChanges();
            
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

            db.SaveChanges();
        }

    }
}
