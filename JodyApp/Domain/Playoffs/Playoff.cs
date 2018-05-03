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
            return Series.Where(s => s.Round == round).ToList();
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

            return groupMap;
        }

        public void AddTeamsToGroup(GroupRule rule, List<Team> teamsInGroup)
        {            
            switch (rule.RuleType)
            {
                case GroupRule.FROM_TEAM:
                    teamsInGroup.Add(rule.FromTeam);
                    break;
                case GroupRule.FROM_SERIES:
                    switch (rule.FromStartValue)
                    {
                        case GroupRule.SERIES_WINNER:
                            teamsInGroup.Add(rule.FromSeries.GetWinner());
                            break;
                        case GroupRule.SERIES_LOSER:
                            teamsInGroup.Add(rule.FromSeries.GetLoser());
                            break;
                        default:
                            throw new ApplicationException("Bad Option in Group Rule From Series");
                    }
                    break;
                case GroupRule.FROM_DIVISION:
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
        
    }
}
