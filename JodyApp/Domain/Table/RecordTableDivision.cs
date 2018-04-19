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
        public RecordTableDivision(string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules) : base(name, shortName, level, order, parent)
        {
            if (sortingRules == null) sortingRules = new List<SortingRule>();
            SortingRules = sortingRules;
        }
         

        public List<SortingRule> SortingRules { get; set; }
        public List<DivisionRank> Rankings { get; set; }

        public void SetRank(int rank, RecordTableTeam team)
        {
            if (Rankings == null) Rankings = new List<DivisionRank>();

            if (Rankings.Any(r => r.Team.Name.Equals(team.Name)))
            {
                Rankings.Find(r => r.Team.Name.Equals(team.Name)).Rank = rank;
            }
            else
            {
                Rankings.Add(new DivisionRank() { Division = this, Team = team, Rank = rank });
            }
        }
    }
}
