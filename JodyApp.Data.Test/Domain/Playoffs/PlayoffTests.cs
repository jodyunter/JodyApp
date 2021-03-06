﻿using System;
using System.Collections.Generic;
using JodyApp.Domain;
using JodyApp.Domain.Playoffs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using System.Linq;

namespace JodyApp.Data.Test.Domain.Playoffs
{
    [TestClass]
    public class PlayoffTests
    {
        League league = new League() { Name = "My Test League" };
        Playoff playoff = new Playoff() { Name = "My Playoff", Year = 1, Started = true, Complete = false, CurrentRound = 1, StartingDay = 125, Series = new List<Series>()};


        public Series CreateWinningSeries()
        {
            Team homeTeam = new Team() { Name = "Team 1", Skill = 5, Id = 12 };
            Team awayTeam = new Team() { Name = "Team 2", Skill = 5, Id = 55 };
            SeriesRule seriesRule = new SeriesRule(playoff, "Series 1", 1, null, SeriesRule.SERIES_WINNER, null, SeriesRule.SERIES_LOSER, SeriesRule.TYPE_BEST_OF, 1, false, "1");
            var games = new List<Game>();
            
            Series series = new Series(playoff, seriesRule, homeTeam, 0, awayTeam, 1, games, "Series 1");
            games.Add(new Game(null, series, homeTeam, awayTeam, 1, 1, 2, 5, false, 0, true));

            return series;

        }

        public Series CreateLosingSeries()
        {
            Team homeTeam = new Team() { Name = "Team 3", Skill = 5, Id = 11 };
            Team awayTeam = new Team() { Name = "Team 4", Skill = 5, Id = 56 };
            SeriesRule seriesRule = new SeriesRule(playoff, "Series 2", 1, null, SeriesRule.SERIES_WINNER, null, SeriesRule.SERIES_LOSER, SeriesRule.TYPE_BEST_OF, 1, false, "1");
            var games = new List<Game>();
            
            Series series = new Series(playoff, seriesRule, homeTeam, 0, awayTeam, 1, games, "Series 2");
            games.Add(new Game(null, series, homeTeam, awayTeam, 1, 1, 2, 5, false, 0, true));

            return series;

        }

        public Team CreateForByTeam(string teamName, int id)
        {
            return new Team() { Name =teamName, Skill = 5, Id = id, Playoff = playoff };
        }

        public Division CreateDivision()
        {
            Division div = new Division() { League = league, Name = "Division 1", Level = 1, Parent = null };
            var teams = new List<Team>()
            {
                new Team() { Name = "Team 20", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 21", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 22", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 23", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 24", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 25", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 26", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 27", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 28", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 29", Skill = 5, Playoff = playoff, Division = div},
                new Team() { Name = "Team 30", Skill = 5, Playoff = playoff, Division = div}
            };

            div.Rankings = new List<DivisionRank>()
            {
                new DivisionRank() { Division = div, Team = teams[0], Rank = 1 },
                new DivisionRank() { Division = div, Team = teams[1], Rank = 7 },
                new DivisionRank() { Division = div, Team = teams[2], Rank = 5 },
                new DivisionRank() { Division = div, Team = teams[3], Rank = 6 },
                new DivisionRank() { Division = div, Team = teams[4], Rank = 4 },
                new DivisionRank() { Division = div, Team = teams[5], Rank = 3 },
                new DivisionRank() { Division = div, Team = teams[6], Rank = 2 },
                new DivisionRank() { Division = div, Team = teams[7], Rank = 8 },
                new DivisionRank() { Division = div, Team = teams[8], Rank = 9 },
                new DivisionRank() { Division = div, Team = teams[9], Rank = 10 },
                new DivisionRank() { Division = div, Team = teams[10], Rank = 11 }
            };            

            return div;
        }
        [TestMethod]
        public void ShouldGetTeamsByTeam()
        {
            Team team = CreateForByTeam("My Team Name", 12);
            Group group = new Group("Team Group", playoff, null, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromTeam(group, "Team Rule", team);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);

            AreEqual(1, teamList.Count);
            AreEqual("My Team Name", teamList[0].Name);            
        }
        [TestMethod]
        public void ShouldGetTeamsBySeriesWinner()
        {
            var series = CreateWinningSeries();
            playoff.Series.Add(series);
            IsTrue(series.Complete);

            Group group = new Group("Series Group", playoff, null, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromSeriesWinner(group, "Series Grp Rule 1",series.Name);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);
            AreEqual(1, teamList.Count);
            AreEqual("Team 2", teamList[0].Name);

        }
        [TestMethod]
        public void ShouldGetTeamsBySeriesLoser()
        {
            var series = CreateLosingSeries();
            playoff.Series.Add(series);
            IsTrue(series.Complete);
            Group group = new Group("Series Group", playoff, null, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromSeriesLoser(group, "Series Grp Rule 1", series.Name);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);
            AreEqual(1, teamList.Count);
            AreEqual("Team 3", teamList[0].Name);
        }
        [TestMethod]
        public void ShouldGetTeamByDivisionSingleTeam()
        {
            Division division = CreateDivision();
            Group group = new Group("Series Group", playoff, division, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromDivision(group, "Serise Grp Rule 1", division, 7, 7);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);
            AreEqual(1, teamList.Count);
            IsNull(teamList.Where(t => t.Name == "Team 26").FirstOrDefault());
            IsNull(teamList.Where(t => t.Name == "Team 22").FirstOrDefault());                        
            IsNull(teamList.Where(t => t.Name == "Team 20").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 21").FirstOrDefault());

        }
        [TestMethod]
        public void ShouldGetTeamByDivisionMulitpleTeams()
        {
            Division division = CreateDivision();
            Group group = new Group("Division Group", playoff, division, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromDivision(group, "division grp rule 1", division, 2, 5);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);
            AreEqual(4, teamList.Count);
            IsNotNull(teamList.Where(t => t.Name == "Team 26").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 22").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 24").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 25").FirstOrDefault());
            IsNull(teamList.Where(t => t.Name == "Team 21").FirstOrDefault());            
        }
        [TestMethod]
        public void ShouldGetByAllTypesAtOnce()
        {
            var division = CreateDivision();
            var series = CreateLosingSeries();
            playoff.Series.Add(series);            
            Group group2 = new Group("Division Group", playoff, null, new List<GroupRule>());
            GroupRule rule1 = GroupRule.CreateFromDivision(group2, "division grp rule 1", division, 2, 5);
            GroupRule rule2 = GroupRule.CreateFromDivision(group2, "division grp rule 2", division, 8, 8);
            GroupRule rule3 = GroupRule.CreateFromSeriesLoser(group2, "series grp rule 1", series.Name);          

            playoff.Groups = new List<Group>() { group2 }; 

            var result = playoff.SetupGroups();

            AreEqual(1, result.Count);
            IsNotNull(result[group2.Name]);
            var teamList = result[group2.Name];
            AreEqual(6, teamList.Count);

            IsNotNull(teamList.Where(t => t.Name == "Team 26").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 22").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 24").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 25").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 27").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 3").FirstOrDefault());
            IsNull(teamList.Where(t => t.Name == "Team 21").FirstOrDefault());
        }

        [TestMethod]
        public void ShouldGetMultipleGroups()
        {
            var division = CreateDivision();
            var series = CreateLosingSeries();            
            playoff.Series.Add(series);
            Group group1 = new Group("Series Group", playoff, division, new List<GroupRule>());
            Group group2 = new Group("Division Group", playoff, null, new List<GroupRule>());
            GroupRule rule1 = GroupRule.CreateFromDivision(group2, "division grp rule 1", division, 2, 5);
            GroupRule rule2 = GroupRule.CreateFromDivision(group2, "division grp rule 2", division, 8, 8);
            GroupRule rule3 = GroupRule.CreateFromSeriesLoser(group1, "series grp rule 1", series.Name);

            playoff.Groups = new List<Group>() { group1, group2 };

            var result = playoff.SetupGroups();

            AreEqual(2, result.Count);
            IsNotNull(result[group1.Name]);
            IsNotNull(result[group2.Name]);
            var teamList = result[group2.Name];
            AreEqual(5, teamList.Count);

            IsNotNull(teamList.Where(t => t.Name == "Team 26").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 22").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 24").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 25").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 27").FirstOrDefault());            
            IsNull(teamList.Where(t => t.Name == "Team 21").FirstOrDefault());

            teamList = result[group1.Name];
            AreEqual(1, teamList.Count);
            IsNotNull(teamList.Where(t => t.Name == "Team 3").FirstOrDefault());
        }

        [TestMethod]
        public void ShouldValidateGroupSorting()
        {
            var division = CreateDivision();
            var series = CreateLosingSeries();
            playoff.Series.Add(series);            
            Group group2 = new Group("Division Group", playoff, division, new List<GroupRule>());
            GroupRule rule1 = GroupRule.CreateFromDivision(group2, "division grp rule 1", division, 2, 5);
            GroupRule rule2 = GroupRule.CreateFromDivision(group2, "division grp rule 2", division, 8, 8);
            GroupRule rule3 = GroupRule.CreateFromSeriesLoser(group2, "series grp rule 1", series.Name);

            playoff.Groups = new List<Group>() { group2 };

            var result = playoff.SetupGroups();

            AreEqual(1, result.Count);
            IsNotNull(result[group2.Name]);
            var teamList = result[group2.Name];
            AreEqual(6, teamList.Count);
            AreEqual("Team 26", teamList[0].Name);
            AreEqual("Team 25", teamList[1].Name);
            AreEqual("Team 24", teamList[2].Name);
            AreEqual("Team 22", teamList[3].Name);
            AreEqual("Team 27", teamList[4].Name);
            AreEqual("Team 3", teamList[5].Name);


        }
        [TestMethod]
        public void ShouldGetFromLastPlace()
        {
            Division division = CreateDivision();
            Group group = new Group("Division Group", playoff, division, new List<GroupRule>());
            GroupRule rule = GroupRule.CreateFromDivisionBottom(group, "division grp rule 1", division, 2, 5);

            var teamList = new List<Team>();
            playoff.AddTeamsToGroup(rule, teamList);
            AreEqual(4, teamList.Count);
            IsNotNull(teamList.Where(t => t.Name == "Team 29").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 28").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 27").FirstOrDefault());
            IsNotNull(teamList.Where(t => t.Name == "Team 21").FirstOrDefault());
            IsNull(teamList.Where(t => t.Name == "Team 30").FirstOrDefault());

        }
        [TestMethod]
        public void ShouldSetSeriesTeamsFromSeriesWinnerAndLoserRule()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldSetseriesTeamFromDivisionRankings()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void ShouldSetSeriesTeamsFromGroupingWithTeam()
        {
            throw new NotImplementedException();
        }
        [TestMethod]
        public void ShouldSortPropertyMultiDivisionalGroup()
        {
            throw new NotImplementedException();
            //make sure that the rule added first is NOT first place
        }
    }
}
