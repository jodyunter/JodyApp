using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class StandingsSorter
    {
        public static int SORT_BY_LEAGUE = 0;
        public static int SORT_BY_DIVISION = 2;
        public static int SORTY_BY_CONFERENCE = 1;

        private int DivisionLevel { get; set; }     

        public StandingsSorter() { }
        
        
        //we have two things to consider        
       
        public static SortedDictionary<Division, List<RecordTableTeam>> SortByDivisionLevel(RecordTable table, int divisionLevel)
        {

            List<RecordTableTeam> list = table.Standings.Values.ToList<RecordTableTeam>();

            SortedDictionary<Division, List<RecordTableTeam>> sortedStandings = new SortedDictionary<Division, List<RecordTableTeam>>();

            list.ForEach(team =>
            {
                SortIntoDivisions(team, divisionLevel, sortedStandings);
            });

            foreach(KeyValuePair<Division, List<RecordTableTeam>> entry in sortedStandings)
            {
                //need to apply any sorting rules here
                entry.Value.Sort();
                int rank = 1;
                entry.Value.ForEach(team =>
                {
                    team.Stats.Rank = rank;
                    rank++;
                });                
            }

            return sortedStandings;
        }

        private static void SortIntoDivisions(RecordTableTeam team, int divisionLevel, SortedDictionary<Division, List<RecordTableTeam>> teamList)
        {
            Division a0 = team.Division;

            while (a0.Level != divisionLevel)
            {
                a0 = a0.Parent;
            }

            AddToDictionary(a0, team, teamList);
        }

        private static void AddToDictionary(Division d, RecordTableTeam team, SortedDictionary<Division, List<RecordTableTeam>> teamList)
        {
            
            if (!teamList.ContainsKey(d))
            {
                teamList.Add(d, new List<RecordTableTeam>());                
            }

            teamList[d].Add(team);
            
        }

        public static List<RecordTableTeam> SortByRule(Division divisionToSort, List<SortingRule> rules, List<RecordTableTeam> teamsInDivisionNoEdit)
        {
            //this assumes the groups are ordered 0,1,2,3 etc in order starting at 0 and incrementing by 1
            Dictionary<int, List<RecordTableTeam>> groups = new Dictionary<int, List<RecordTableTeam>>();
            List<RecordTableTeam> teamList = new List<RecordTableTeam>();
            teamList.AddRange(teamsInDivisionNoEdit);

            rules.ForEach(rule =>
            {
                if (!groups.ContainsKey(rule.GroupNumber))
                {
                    groups.Add(rule.GroupNumber, new List<RecordTableTeam>());
                }

                teamsInDivisionNoEdit.ForEach(team => {
                    List<RecordTableTeam> teamsInDivision = new List<RecordTableTeam>();

                    if (team.Division.Name.Equals(rule.DivisionToGetTeamsFrom.Name))
                    {
                        teamsInDivision.Add(team);    
                    }
                });
            });
            
            List<RecordTableTeam> result = new List<RecordTableTeam>();

            for (int i = 0; i < groups.Count; i++)
            {
                groups[i].Sort();
                result.AddRange(groups[i]);
            }

            return result;
        }
    }
}
