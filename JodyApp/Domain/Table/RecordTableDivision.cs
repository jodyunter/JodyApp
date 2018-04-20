using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Database;

namespace JodyApp.Domain.Table
{
    public class RecordTableDivision:Division
    {
        public RecordTableDivision() { }
        public RecordTableDivision(League league, string name, string shortName, int level, int order, Division parent, List<SortingRule> sortingRules) : base(league, name, shortName, level, order, parent)
        {
            if (sortingRules == null) sortingRules = new List<SortingRule>();
            SortingRules = sortingRules;
        }                

        public override void SetRank(int rank, RecordTableTeam team)
        {
            if (Rankings == null) Rankings = new List<DivisionRank>();

            if (Rankings.Any(r => r.Team.Name.Equals(team.Name)))
            {
                Rankings.Find(r => r.Team.Name.Equals(team.Name)).Rank = rank;
            }
            else
            {
                Rankings.Add(new RecordTableDivisionRank() { Division = this, Team = team, Rank = rank });
            }
        }
    }
}
