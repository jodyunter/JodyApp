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
       
        public static SortedDictionary<Division, List<Team>> SortByDivisionLevel(RecordTable table, int divisionLevel)
        {

            List<Team> list = table.Standings.Values.ToList<Team>();

            SortedDictionary<Division, List<Team>> sortedStandings = new SortedDictionary<Division, List<Team>>();

            list.ForEach(team =>
            {
                SortIntoDivisions(team, divisionLevel, sortedStandings);
            });

            foreach(KeyValuePair<Division, List<Team>> entry in sortedStandings)
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

        private static void SortIntoDivisions(Team team, int divisionLevel, SortedDictionary<Division, List<Team>> teamList)
        {
            Division a0 = team.Division;

            while (a0.Level != divisionLevel)
            {
                a0 = a0.Parent;
            }

            AddToDictionary(a0, team, teamList);
        }

        private static void AddToDictionary(Division d, Team team, SortedDictionary<Division, List<Team>> teamList)
        {
            
            if (!teamList.ContainsKey(d))
            {
                teamList.Add(d, new List<Team>());                
            }

            teamList[d].Add(team);
            
        }

        //todo we need to remove this method because we need service calls to properly sort the divisions
        public static List<Team> SortByRules(Dictionary<Division,List<Team>> teamsByDivision, Division division)
        {
            var result = new List<Team>();
            var editableTeams = new List<Team>();
            editableTeams.AddRange(teamsByDivision[division]);

            var ruleGroupings = new SortedDictionary<int, List<Team>>();

            if (division.SortingRules == null || division.SortingRules.Count == 0)
            {
                editableTeams.Sort();
                result.AddRange(editableTeams);
            }
            else
            {
                division.SortingRules.ForEach(rule =>
                {
                    //ensure the division we are getting teams from is sorted
                    var sortedTeams = SortByRules(teamsByDivision, rule.DivisionToGetTeamsFrom);

                    if (!ruleGroupings.ContainsKey(rule.GroupNumber)) ruleGroupings.Add(rule.GroupNumber, new List<Team>());

                    //if no positions are specified by this rule, add all of the sorted teams.
                    if (rule.PositionsToUse == null)
                    {
                        ruleGroupings[rule.GroupNumber].AddRange(sortedTeams);
                        sortedTeams.ForEach(team => editableTeams.Remove(team));                        
                    }
                    else
                    {
                        rule.PositionsToUse.Split(',').Select(int.Parse).ToList<int>().ForEach(i =>
                        {
                            var team = sortedTeams[i - 1];
                            ruleGroupings[rule.GroupNumber].Add(team);
                            editableTeams.Remove(team);
                        });
                    }
                    
                });


                for (int i = 0; i < ruleGroupings.Count; i++)
                {
                    ruleGroupings[i].Sort();
                    result.AddRange(ruleGroupings[i]);
                }
                editableTeams.Sort();
                result.AddRange(editableTeams);
                
                
            }
            
            int rank = 1;
            for (int i = 0; i < result.Count; i++) { division.SetRank(rank, result[i]); result[i].Stats.Rank = rank; rank++; }

            return result;
        }

        public static List<Team> SortByRanking(Division division)
        {
            if (division.Rankings == null) throw new ApplicationException("Must have rankings done prior to calling this method");

            division.Rankings.Sort();
            List<Team> teams = division.Rankings.Select(i => (Team)i.Team).ToList<Team>();

            return teams;
        }
    }
    
}
