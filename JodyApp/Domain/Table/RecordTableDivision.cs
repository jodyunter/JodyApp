using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Table
{
    public class RecordTableDivision:Division
    {
        public RecordTableDivision() { }
        public RecordTableDivision(string name, string shortName, int level, int order, Division parent) : base(name, shortName, level, order, parent) { }        

        public List<SortingRule> SortingRules { get; set; }
        
        //assumption is these are the teams in the division
        public void Sort(List<RecordTableTeam> teamsToSort)
        {
            if (SortingRules == null || SortingRules.Count == 0)
            {
                teamsToSort.Sort();
            }
            else
            {
                //first sort teams into their divisions, but this isn't good enough, because they only have
                //lowest level division, how do we get team sin higher level?
                //use is team in division because it loops up to the parent
                var teamsInDivisions = teamsToSort
                                        .GroupBy(team => (RecordTableDivision)team.Division)
                                        .ToDictionary(
                                            group => group.Key,
                                            group => group.ToList<RecordTableTeam>());
                
                //next sort each division with this method
                teamsInDivisions.ToList().ForEach(pair =>
                {
                    pair.Key.Sort(pair.Value);
                });

                var teamsToSortEditable = new List<RecordTableTeam>();

                teamsToSortEditable.AddRange(teamsToSort);

                var groups = new Dictionary<int, List<RecordTableTeam>>();

                SortingRules.ForEach(rule =>
                {
                    var positions = rule.PositionsToUse.Split(',').Select(int.Parse).ToList<int>();
                    positions.ForEach(i =>
                   {
                       var team = teamsInDivisions[rule.DivisionToGetTeamsFrom][i];

                       if (!(groups.ContainsKey(rule.GroupNumber))) groups.Add(rule.GroupNumber, new List<RecordTableTeam>());

                       groups[rule.GroupNumber].Add(team);
                       teamsToSortEditable.Remove(team);
                   });
                }
                );

                groups.ToList().ForEach(pair =>
                {
                    //sort each group based on normal sorting rules for RecordTableTeams
                    pair.Value.Sort();
                }
                );

                //sort the groups in the proper order
                groups = groups.OrderBy(o => o.Key).ToDictionary(o => o.Key, o => o.Value);
                //sort the left over teams
                teamsToSortEditable.Sort();

                var results = new List<RecordTableTeam>();

                groups.ToList().ForEach(pair => {
                    results.AddRange(pair.Value);
                });

                results.AddRange(teamsToSortEditable);

                teamsToSort = results;
            }
        }

        private void SortDivisions(Dictionary<RecordTableDivision, List<RecordTableTeam>> map)
        {

        }
    }
}
