using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public class Playoff : DomainObject, Competition
    {
        public int Year { get; set; }
        public int StartingDay { get; set; }
        public bool Complete { get; set; }
        public bool Started { get; set; }
        public League League { get; set; }
        public string Name { get; set; }

        public int CurrentRound { get; set; }
        virtual public List<Series> Series { get; set; }
        virtual public List<GroupRule> GroupRules { get; set; }

        public List<Series> GetSeriesForRound(int round) {
            return Series.Where(s => s.Rule.Round == round).ToList();
        }        

        public Series GetSeriesByName(string name)
        {
            return Series.Where(s => s.Name == name).First();
        }

        public void SetupSeriesForRound(int round)
        {
            //setup groups
            var seriesForRound = GetSeriesForRound(round);

            var groups = SetupGroups();

            seriesForRound.ForEach(s =>
            {
                
            }
            );
        }

        public Dictionary<String, List<Team>> SetupGroups()
        {
            var groupMap = new Dictionary<String, List<Team>>();

            GroupRules.ForEach(rule => 
            {
                if (!groupMap.ContainsKey(rule.GroupIdentifier)) groupMap.Add(rule.GroupIdentifier, new List<Team>());
                AddTeamsToGroup(rule, groupMap[rule.GroupIdentifier]);                
            }
            );

            //loop through the group rules and verify that where there is no division to sort by the teams are in the proper order
            //hometeams should be first
            groupMap.Keys.ToList().ForEach(key =>
            {
                var teamList = groupMap[key];
                var groupRule = GroupRules.Where(gr => gr.GroupIdentifier == key).First();
                var division = groupRule.SortByDivision;
                teamList = teamList.OrderBy(t => division.GetRank(t)).ToList();
                
            });
            return groupMap;
        }

        private static void AddTeam(GroupRule rule, List<Team> teamsInGroup, Team team)
        {
            if (rule.SortByDivision == null)
            {
                if (teamsInGroup.Count > 0)
                {
                    if (rule.IsHomeTeam)
                    {
                        teamsInGroup.Insert(0, team);
                    }
                    else
                    {
                        teamsInGroup.Insert(1, team);
                    }
                }
                else
                    teamsInGroup.Add(team);
            }
            else
                teamsInGroup.Add(team);            
        }
        public void AddTeamsToGroup(GroupRule rule, List<Team> teamsInGroup)
        {            
            switch (rule.RuleType)
            {
                case GroupRule.FROM_TEAM:
                    AddTeam(rule, teamsInGroup, rule.FromTeam);                        
                    break;
                case GroupRule.FROM_SERIES:
                    switch (rule.FromStartValue)
                    {
                        case GroupRule.SERIES_WINNER:
                            AddTeam(rule, teamsInGroup, rule.FromSeries.GetWinner());
                            break;
                        case GroupRule.SERIES_LOSER:
                            AddTeam(rule, teamsInGroup, rule.FromSeries.GetLoser());
                            break;
                        default:
                            throw new ApplicationException("Bad Option in Group Rule From Series");
                    }
                    break;
                case GroupRule.FROM_DIVISION:
                    //current assumption is from division MUST have sort by division
                    int startingRank = rule.FromStartValue;
                    int endingRank = rule.FromEndValue;

                    for (int i = startingRank; i <= endingRank; i++)
                    {
                        teamsInGroup.Add(rule.FromDivision.GetByRank(i));
                    }
                    break;
                default:
                    throw new ApplicationException("Bad option in GroupRule Rule Type");
            }            
        }
        
        public void SetTeamsForSeries(Dictionary<string, List<Team>> groupings, Series series)
        {
            SeriesRule seriesRule = series.Rule;
            var homeTeamList = groupings[seriesRule.HomeTeamFromGroup];
            var awayTeamList = groupings[seriesRule.AwayTeamFromGroup];

            var homeTeam = homeTeamList[seriesRule.HomeTeamFromRank - 1];
            var awayTeam = awayTeamList[seriesRule.AwayTeamFromRank - 1];

            series.HomeTeam = homeTeam;
            series.AwayTeam = awayTeam;
            
        }
    }
}
