using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JodyApp.Domain;
using JodyApp.Database;
using JodyApp.Domain.Season;
using System.Data.Entity;
using JodyApp.Domain.Table;

namespace JodyApp.Service
{
    public class DivisionService:BaseService
    {        

        public DivisionService(JodyAppContext context):base(context)
        {            
        }

        //this could cause trouble with seasons
        virtual public List<Division> GetDivisionsByParent(Division parent)
        {
            var divs = db.Divisions.Where(div => div.Parent.Name == parent.Name);
            
            return divs.ToList<Division>();
        }

        virtual public List<Team> GetAllTeamsInDivision(Division division)
        {

            List<Team> teams = new List<Team>();
            if (division.Teams != null) teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));                                
            });

            return teams;
        }
        virtual public Division GetByName(String Name)
        {            
            var query = from d in db.Divisions where d.Name.Equals(Name) select d;
            
            Division division = null;
            foreach (var d in query)
            {
                division = d;
            }

            return division;
        }   
        
        virtual public List<Division> GetDivisionsByLevel(int level)
        {
            var query = db.Divisions.Where(d => d.Level == level);

            return query.ToList<Division>();

        }

        public List<Division> GetDivisionsByLevel(Division d)
        {
            return GetDivisionsByLevel(d.Level);
        }

        public List<Division> GetDivisionsByParent(SeasonDivision parent)
        {            
            var divs = db.SeasonDivisions.Where(div => div.Parent.Name == parent.Name);

            return divs.ToList<Division>();
        }

        public List<Team> GetAllTeamsInDivision(SeasonDivision division)
        {            
            List<Team> teams = new List<Team>();
            if (division.Teams != null) teams.AddRange(division.Teams);

            GetDivisionsByParent(division).ForEach(div =>
            {
                teams.AddRange(GetAllTeamsInDivision(div));
            });

            return teams;
        }
        public Division GetByName(String name, Season season)
        {
            var division = db.SeasonDivisions.Include("Season").Where(d => d.Name.Equals(name) && d.Season.Id == season.Id);

            return division.First();
        }

        public List<Division> GetDivisionsByLevel(int level, Season season)
        {
            var query = db.SeasonDivisions.Include("Season").Where(d => d.Level == level && d.Season.Id == season.Id);

            return query.ToList<Division>();
        }

        public List<Division> GetDivisionsByLevel(SeasonDivision division)
        {
            return GetDivisionsByLevel(division.Level, division.Season);
        }

        //this will return the list of teams, but more importantly will setup the division rankings
        public List<RecordTableTeam> SortByDivision(RecordTableDivision division)
        {            
            //decision to make.  Do we organize a higher teir based on lower tier rank without explicitly saying so?
            var ruleGroupings = new SortedDictionary<int, List<RecordTableTeam>>();

            //get a list of all teams in division, all must be sorted somehow
            var editableTeamList = this.GetAllTeamsInDivision(division);

            //go through each rule and sort the divisions required by the rule
            division.SortingRules.ForEach(rule => {
                this.SortByDivision(rule.DivisionToGetTeamsFrom);

                //add an empty list if there isn't one for that rule group
                if (!ruleGroupings.ContainsKey(rule.GroupNumber)) ruleGroupings.Add(rule.GroupNumber, new List<RecordTableTeam>());
                
                
                rule.PositionsToUse.Split(',').Select(int.Parse).ToList<int>().ForEach(position => 
                {
                    //add the team at the given rank to the grouping
                    RecordTableTeam teamToAdd = (RecordTableTeam)rule.DivisionToGetTeamsFrom.Rankings.Single(d => d.Rank == (position + 1)).Team;
                    ruleGroupings[rule.GroupNumber].Add(teamToAdd);
                    editableTeamList.Remove(teamToAdd);
                });
                
            });

            //now we have grouped all special groups and have a list of leftovers.
            int rank = 1;

            //dictionary of String, DivisionRank
            var rankingDictionary = division.Rankings.ToDictionary(r => r.Team.Name, r => r);
            var resultList = new List<RecordTableTeam>();

            for (int i = 0; i < ruleGroupings.Count; i++)
            {
                ruleGroupings[i].Sort();
                ruleGroupings[i].ForEach(team => {
                    if (!rankingDictionary.ContainsKey(team.Name))
                    {
                        var newDivRank = new DivisionRank() { Division = division, Team = team, Rank = rank };
                        rank++;
                        division.Rankings.Add(newDivRank);
                        rankingDictionary.Add(team.Name, newDivRank);
                        resultList.Add(team);
                    }
                });
            }

            editableTeamList.ForEach(unGroupedTeam => {
                if (!rankingDictionary.ContainsKey(unGroupedTeam.Name))
                {
                    var newDivRank = new DivisionRank() { Division = division, Team = unGroupedTeam, Rank = rank };
                    rank++;
                    division.Rankings.Add(newDivRank);
                    rankingDictionary.Add(unGroupedTeam.Name, newDivRank);
                    resultList.Add((RecordTableTeam)unGroupedTeam);
                }
            });
            
            return resultList;
        }

    }
}

