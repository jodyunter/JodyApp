using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using JodyApp.Domain;

namespace JodyApp.Service.DataFolder
{
    public class DataService
    {
        private static DataService instance;

        string TeamFile = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service\\DataFolder\\TeamData.txt";
        string DivisionFile = "D:\\Visual Studio Projects\\gitrepos\\JodyApp\\JodyApp.Service\\DataFolder\\DivisionData.txt";

        List<Team> Teams { get; set; }
        List<Division> Divisions { get; set; }

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
        }


        delegate T PopulateObject<T>(string[] input);

        Team PopulateTeam(string[] input)
        {
            Team team = new Team
            {
                Name = input[0],
                Skill = int.Parse(input[1])
            };

            return team;
        }

        Division PopulateDivision(string[] input)
        {
            Division division = new Division
            {
                Name = input[0],
                Teams = new List<Team>(),
            };

            //get parent
            if (input[1].Length != 0)
            {
                //get division by name
                //divisions must be in order of hierarchy for this to work
                Division parent = GetDivisionByName(input[1]);

                division.Parent = parent;
            }

            for (int i = 2; i < input.Length; i++)
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

    }
}
