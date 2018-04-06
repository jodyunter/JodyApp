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
using JodyApp.Domain.Schedule;

namespace JodyApp.Service.DataFolder
{
    public class DataService:BaseService
    {        
        private static DataService instance;        
        //static string BASE_DIR = "C:\\Users\\jody_unterschutz\\source\\repos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";
        static string BASE_DIR = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service.Test\\DataFolder\\";
        static string DEFAULT_FOLDER = "";
        string TeamFile = "TeamData.txt";
        string DivisionFile = "DivisionData.txt";
        string ScheduleFile = "ScheduleRule.txt";
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


        public static DataService Instance(JodyAppContext context, string dataFolder)
        {
            if (instance == null)
            {
                instance = new DataService(context, dataFolder);
            }
            return instance;

        }

        public void SetFileData(string baseFolder, string folder)
        {
            TeamFile = baseFolder + folder + TeamFile;
            DivisionFile = baseFolder + folder + DivisionFile;
            ScheduleFile = baseFolder + folder + ScheduleFile;
        }

        public void DeleteAllData()
        {
            string[] tables = { "ScheduleRules", "Seasons", "Teams", "TeamStatistics", "Divisions" };
            var objCtx = ((System.Data.Entity.Infrastructure.IObjectContextAdapter)db).ObjectContext;
            foreach (string table in tables)
            {
                objCtx.ExecuteStoreCommand("DELETE [" + table + "]");
            }

        }

        public DataService(JodyAppContext context, string testDataFolder) : base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);
            DeleteAllData();
            SetFileData(BASE_DIR, testDataFolder);

            LoadData(TeamFile, db.Teams, PopulateTeam);
            LoadData(DivisionFile, db.Divisions, PopulateDivision);
            //LoadData(ScheduleFile, db.ScheduleRules, PopulateScheduleRule);

            db.SaveChanges();

        }

        public DataService(JodyAppContext context):base(context)
        {
            teamService = new TeamService(context);
            divisionService = new DivisionService(context);            
            DeleteAllData();
            SetFileData(BASE_DIR, DEFAULT_FOLDER);

            LoadData(TeamFile, db.Teams, PopulateTeam);
            LoadData(DivisionFile, db.Divisions, PopulateDivision);
            //LoadData(ScheduleFile, db.ScheduleRules, PopulateScheduleRule);

            db.SaveChanges();
            
        }


        delegate T PopulateObject<T>(string[] input, List<T> dataList) where T:class;
        

        Team PopulateTeam(string[] input, List<Team> list)
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

        Division PopulateDivision(string[] input, List<Division> list)
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

                var query = from d in db.Divisions where d.Name.Equals(input[PARENT]) select d;

                foreach (Division d in list)
                {
                    if (d.Name.EndsWith(input[PARENT]))
                    {
                        parent = d;
                    }
                }



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

        ScheduleRule PopulateScheduleRule(string[] input, List<ScheduleRule> rules)
        {
            ScheduleRule rule = new ScheduleRule();
            const string DIVISION_TYPE = "Division";
            const string TEAM_TYPE = "Team";
            const string NONE_TYPE = "None";

            int HOMETYPE = 0;
            int HOMENAME = 1;
            int AWAYTYPE = 2;
            int AWAYNAME = 3;

            switch(input[HOMETYPE])
            {
                case DIVISION_TYPE:
                    string divisionName = input[HOMENAME];
                    rule.HomeDivision = divisionService.GetByName(divisionName);
                    break;
                case TEAM_TYPE:
                    string teamName = input[HOMENAME];
                    rule.HomeTeam = teamService.GetTeamByName(teamName);
                    break;
            }

            return rule;
        }

        void LoadData<T>(string fileName, DbSet<T> dataList, PopulateObject<T> populateObject) where T:class
        {
            StreamReader reader = new StreamReader(fileName);
            string line;

            List<T> list = new List<T>();

            line = reader.ReadLine(); //read header line!

            while ((line = reader.ReadLine()) != null)
            {
                string[] split = line.Split('\t');


                T obj = populateObject(split, list);

                list.Add(obj);
             
            }

            dataList.AddRange(list);

            db.SaveChanges();
        }

    }
}
