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

        //todo we need to remove this method because we need service calls to properly sort the divisions
        public static List<RecordTableTeam> SortByRules(Dictionary<RecordTableDivision,List<RecordTableTeam>> teamsByDivision, RecordTableDivision division)
        {
            var editableTeams = new List<RecordTableTeam>();
            editableTeams.AddRange(teamsByDivision[division]);

            var ruleGroupings = new SortedDictionary<int, List<RecordTableTeam>>();

            if (division.SortingRules == null || division.SortingRules.Count == 0)
            {
                editableTeams.Sort();
                return editableTeams;
            }
            else
            {
                division.SortingRules.ForEach(rule =>
                {
                    //ensure the division we are getting teams from is sorted
                    var sortedTeams = SortByRules(teamsByDivision, rule.DivisionToGetTeamsFrom);

                    rule.PositionsToUse.Split(',').Select(int.Parse).ToList<int>().ForEach(i =>
                    {
                        var team = sortedTeams[i];
                        if (!ruleGroupings.ContainsKey(rule.GroupNumber)) ruleGroupings.Add(rule.GroupNumber, new List<RecordTableTeam>());
                        ruleGroupings[rule.GroupNumber].Add(team);
                        editableTeams.Remove(team);
                    });
                    
                });

                var result = new List<RecordTableTeam>();

                for (int i = 0; i < ruleGroupings.Count; i++)
                {
                    ruleGroupings[i].Sort();
                    result.AddRange(ruleGroupings[i]);
                }

                editableTeams.Sort();
                //map of <TeamName, Ranking>                
                int rank = 0;
                editableTeams.ForEach(team => { division.SetRank(rank, team); });

                result.AddRange(editableTeams);

                return result;
                
            }
            
        }
    }
    
}
