using JodyApp.Database;
using JodyApp.Domain.Table;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JodyApp.Domain.Playoffs
{
    public partial class Playoff : DomainObject, Competition
    {
        public int Year { get; set; }
        public int StartingDay { get; set; }
        public bool Complete { get; set; }
        public bool Started { get; set; }
        virtual public League League { get; set; }
        public string Name { get; set; }

        public int CurrentRound { get; set; }
        virtual public List<Series> Series { get; set; }
        virtual public List<GroupRule> GroupRules { get; set; }
        virtual public List<Team> PlayoffTeams { get; set; }

        virtual public Season Season { get; set; }
        public Playoff() { CurrentRound = 0;  Series = new List<Series>(); }

        public Playoff(League league, string name, int year, bool started, bool complete, int startingDay, Season season):base()
        {
            this.League = league;
            this.Name = name;
            this.Year = year;
            this.Started = started;
            this.Complete = complete;
            this.StartingDay = startingDay;
            this.Season = season;
        }
        public Team GetPlayoffTeamByName(string name)
        {
            if (PlayoffTeams == null) PlayoffTeams = new List<Team>();
            return PlayoffTeams.Where(pt => pt.Name == name).FirstOrDefault();
        }
        public List<Series> GetSeriesForRound(int round) {
            return Series.Where(s => s.Rule.Round == round).ToList();
        }        

        public Series GetSeriesByName(string name)
        {
            return Series.Where(s => s.Name == name).First();
        }

        public bool IsComplete()
        {
            bool complete = true;

            if (Series == null) complete = false;
            else
                Series.ForEach(series => { complete = complete && series.Complete; });

            Complete = complete;

            return complete;
        }

        public void StartCompetition()
        {
            if (!Complete)
            {
                Started = true;
                NextRound();
            }
            else throw new Exception("Trying to start playoff when already compelted");
        }

        public void NextRound()
        {
            if (Started && IsRoundComplete(CurrentRound))
            {
                CurrentRound++;
                SetupSeriesForRound(CurrentRound);
            }
        }
        public void SetupSeriesForRound(int round)
        {                        

            var groups = SetupGroups();
            GetSeriesForRound(round).ForEach(series =>
            {
                SetTeamsForSeries(groups, series);
            });
            
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
                
                if (division != null)
                {
                    teamList = teamList.OrderBy(t => division.GetRank(t)).ToList();
                }

                groupMap[key] = teamList;

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
                            AddTeam(rule, teamsInGroup, GetSeriesByName(rule.SeriesName).GetWinner());
                            break;
                        case GroupRule.SERIES_LOSER:
                            AddTeam(rule, teamsInGroup, GetSeriesByName(rule.SeriesName).GetLoser());
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
                case GroupRule.FROM_DIVISION_BOTTOM:
                    var reversedList = rule.FromDivision.Rankings.OrderByDescending(m => m.Rank).ToList();
                    int bottomRank = rule.FromStartValue;
                    int topRank = rule.FromEndValue;
                    for (int i = bottomRank; i <= topRank; i++)
                    {
                        teamsInGroup.Add(reversedList[i-1].Team);
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

            var homeTeam = GetOrSetupPlayoffTeam(homeTeamList[seriesRule.HomeTeamFromRank - 1]);
            var awayTeam = GetOrSetupPlayoffTeam(awayTeamList[seriesRule.AwayTeamFromRank - 1]);            
            
            series.HomeTeam = homeTeam;
            series.AwayTeam = awayTeam;
            
        }

        public Team GetOrSetupPlayoffTeam(Team team)
        {
            var playoffTeam = GetPlayoffTeamByName(team.Name);

            if (playoffTeam == null)
            {
                playoffTeam = new Team(team.Parent != null ? team.Parent : team, this);
                this.PlayoffTeams.Add(playoffTeam);

            }

            return playoffTeam;
        }
        //todo need a method to setup playoff teams when they aren't yet pat of the playoffs

        public List<Game> GetNextGamesForRound(int round, int lastGameNumber)
        {
            var newGames = new List<Game>();

            GetSeriesForRound(round).ForEach(series =>
            {
                series.CreateNeededGames(lastGameNumber, newGames);
            });

            return newGames;
        }

        public bool IsRoundComplete(int round)
        {
            bool result = true;

            GetSeriesForRound(round).ForEach(series =>
            {
                result = result && series.Complete;
            });

            return result;
        }       
        
        public void PlayGames(List<Game> games, Random random)
        {
            games.ForEach(g => { PlayGame(g, random); });
        }

        public void PlayGame(Game g, Random random)
        {
            g.Play(random);
            ProcessGame(g);
        }

        public void ProcessGame(Game g)
        {
            var series = Series.Where(s => s.Name == g.Series.Name).FirstOrDefault();

            if (series == null) throw new ApplicationException("Trying to process a playoff series game with the wrong series");

            series.ProcessGame(g);
        }

        public List<Game> GetNextGames(int lastGameNumber)
        {
            if (IsRoundComplete(CurrentRound)) NextRound();

            GetNextGamesForRound(CurrentRound, lastGameNumber);

            var unPlayedGames = new List<Game>();

            Series.ForEach(s =>
            {
                unPlayedGames.AddRange(s.Games.Where(g => !g.Complete).ToList());
            });

            return unPlayedGames;
        }


    }
}
