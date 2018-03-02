using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;

namespace JodyApp.Domain.Table
{
    public class RecordTable
    {
        public String TableName = "";

        public Dictionary<string, RecordTableTeam> Standings { get; set; }

        public RecordTable()
        {
            Standings = new Dictionary<string, RecordTableTeam>();
        }
  
        public List<RecordTableTeam> GetSortedList()
        {
            List<RecordTableTeam> teams = Standings.Values.ToList<RecordTableTeam>();
            teams.Sort();
            teams.Reverse();

            int rank = 1;

            teams.ForEach(team =>
            {
                team.Stats.Rank = rank;
                rank++;
            });

            return teams;
        }

    }
}
