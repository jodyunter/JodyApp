using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JodyApp.Domain;
using JodyApp.Domain.Table;

namespace JodyApp.Service.DataFolder
{
    public class DataService
    {
        private static DataService instance;
        static string BASE_DIR = "C:\\Users\\jody_unterschutz\\source\\repos\\JodyApp\\JodyApp.Service\\DataFolder\\";
        //string BASE_DIR = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service\\DataFolder\\";        
        string TeamFile = BASE_DIR + "TeamData.txt";
        string DivisionFile = BASE_DIR + "DivisionData.txt";

        List<Team> Teams { get; set; }
        List<Division> Divisions { get; set; }
        RecordTable Table { get; set; }

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
            Teams = new List<Team>();
            Divisions = new List<Division>();

            LoadData(TeamFile, Teams, PopulateTeam);
            LoadData(DivisionFile, Divisions, PopulateDivision);

            Table = new RecordTable();

            Teams.ForEach(team =>
            {
                Table.Standings.Add(team.Name, new RecordTableTeam(team));
            });
            
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
                Division parent = GetDivisionByName(input[PARENT]);

                division.Parent = parent;
            }

            for (int i = START_OF_TEAM_LIST; i < input.Length; i++)
            {
                //find team, add to division add division to team
                Team team = GetTeamByName(input[i]);
                division.Teams.Add(team);
                team.Division = division;
            }

            return division;

        }

        void LoadData<T>(string fileName, List<T> dataList, PopulateObject<T> populateObject)
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

        public List<Team> GetAllTeams()
        {
            return Teams;
        }

        Team GetTeamByName(string name)
        {
            Team result = null;

            Teams.ForEach(team =>
            {
                if (team.Name.Equals(name)) result = team;
            });

            return result;
        }

        Division GetDivisionByName(string name)
        {
            Division result = null;

            Divisions.ForEach(div =>
            {
                if (div.Name.Equals(name)) result = div;
            });

            return result;
        }


        public RecordTable GetStandings()
        {
            return Table;
        }

        public void Save(Team teamToSave)
        {
            Team oldTeam = null;

            Teams.ForEach(team =>
            {
                if (team.Id.Equals(teamToSave.Id))
                {
                    oldTeam = team;
                }
            });

            if (oldTeam == null)
            {
                throw new ApplicationException("Can't find team: " + teamToSave);
            } else
            {
                oldTeam = teamToSave;
            }
        }
    }
}
