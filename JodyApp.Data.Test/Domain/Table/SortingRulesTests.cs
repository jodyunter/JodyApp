using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Microsoft.VisualStudio.TestTools.UnitTesting.Assert;
using JodyApp.Domain.Table;
using JodyApp.Domain;
using System.Collections.Generic;
using JodyApp.Domain.Schedule;
using JodyApp.Domain.Table.Display;

namespace JodyApp.Data.Test.Domain.Table
{
    [TestClass]
    public class SortingRulesTests
    {
        Dictionary<string, RecordTableDivision> divisions = new Dictionary<string, RecordTableDivision>();
        Dictionary<string, RecordTableTeam> teams = new Dictionary<string, RecordTableTeam>();
        RecordTable table = new RecordTable();

        [TestInitialize]
        public void Setup()
        {
            var league = new RecordTableDivision(null, "League", "League", 0, 0, null, new List<SortingRule>());

            divisions.Add("League", league);

            for (int i = 0; i < 2; i++)
            {
                divisions.Add("Conference " + i, new RecordTableDivision(null, "Conference " + i, null, 1, i + 1, league, new List<SortingRule>()));
            }

            for (int i = 0; i < 6; i++)
            {
                divisions.Add("Division " + i, new RecordTableDivision(null, "Division " + i, null, 2, i + 1, divisions["Conference " + (i % 2)], new List<SortingRule>()));
            }

            for (int i = 0; i < 30; i++)
            {
                RecordTableDivision d = divisions["Division " + (i % 6)];
                teams.Add("Team " + i, new RecordTableTeam("Team " + i, 5, new TeamStatistics(), d));                
                d.Teams.Add(teams["Team " + i]);
            }

            table.Standings = teams;
            table.TableName = "My Testing Table";

            Random random = new Random(1234566);

            foreach (Game g in Scheduler.ScheduleGames(table.GetSortedListByLeague().ToArray(), true))
            {
                g.Play(random);
                table.ProcessGame(g);
            }
        }

        [TestMethod]
        public void ShouldSortLevel0NoRule()
        {
            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["League"]);

            AreEqual("Team 6", teamList[0].Name);
            AreEqual("Team 22", teamList[12].Name);
        }

        [TestMethod]
        public void ShouldSortLevel2NoRule()
        {
            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["Division 2"]);

            AreEqual("Team 26", teamList[0].Name);
            AreEqual("Team 14", teamList[4].Name);
        }

        [TestMethod]
        public void ShouldSortLevel1NoRule()
        {
            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["Conference 1"]);

            AreEqual("Team 3", teamList[0].Name);
            AreEqual("Team 19", teamList[4].Name);
        }

        [TestMethod]
        public void ShouldSortSimpleRule()
        {
            var league = divisions["League"];

            var basicRule = new SortingRule()
            {
                Name = "Division 5 Special",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 5"],
                PositionsToUse = "1,3,5"
            };

            league.SortingRules.Add(basicRule);

            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["League"]);

            AreEqual("Team 17", teamList[0].Name);
            AreEqual("Team 5", teamList[1].Name);
            AreEqual("Team 11", teamList[2].Name);

            var rankings = divisions["League"].Rankings;
            rankings.Sort();
            AreEqual(1, divisions["League"].Rankings[0].Rank);
            AreEqual("Team 5", divisions["League"].Rankings[1].Team.Name);
            AreEqual(30, divisions["League"].Rankings[29].Rank);
        }

        [TestMethod]
        public void ShouldSortTwoDivisionRulesSingleGroup()
        {
            var league = divisions["League"];

            var rule1 = new SortingRule()
            {
                Name = "Division 5 Special",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 5"],
                PositionsToUse = "1,3,5"
            };

            var rule2 = new SortingRule()
            {
                Name = "Division 2 Special",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 2"],
                PositionsToUse = "5"
            };

            league.SortingRules.Add(rule1);
            league.SortingRules.Add(rule2);

            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["League"]);

            
            AreEqual("Team 17", teamList[0].Name);
            AreEqual("Team 5", teamList[1].Name);
            AreEqual("Team 14", teamList[2].Name);
            AreEqual("Team 11", teamList[3].Name);
        }

        [TestMethod]
        public void ShouldSortByAllDivisionLeadersThenSeconds()
        {
            var league = divisions["League"];

            for (int i = 0; i < 6; i++)
            {
                var rule1 = new SortingRule()
                {
                    Name = "Rule " + i,
                    GroupNumber = 0,
                    DivisionToGetTeamsFrom = divisions["Division " + i],
                    PositionsToUse = "1"
                };

                var rule2 = new SortingRule()
                {
                    Name = "Rule " + (i + 6),
                    GroupNumber = 1,
                    DivisionToGetTeamsFrom = divisions["Division " + i],
                    PositionsToUse = "2"
                };

                league.SortingRules.Add(rule1);
                league.SortingRules.Add(rule2);

                
            }

            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["League"]);

            AreEqual("Team 6", teamList[0].Name);            
            AreEqual("Team 26", teamList[1].Name);
            AreEqual("Team 3", teamList[2].Name);
            AreEqual("Team 7", teamList[3].Name);
            AreEqual("Team 16", teamList[4].Name);
            AreEqual("Team 17", teamList[5].Name);
            AreEqual("Team 21", teamList[6].Name);
            AreEqual("Team 19", teamList[7].Name);
            AreEqual("Team 20", teamList[8].Name);
            AreEqual("Team 0", teamList[9].Name);
            AreEqual("Team 22", teamList[10].Name);
            AreEqual("Team 29", teamList[11].Name);

        }

        [TestMethod]
        public void ShouldSortByDivisionLeadersForConference()
        {
            var conf1 = divisions["Conference 0"];
            var conf2 = divisions["Conference 1"];

            conf1.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 4",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 4"],
                PositionsToUse = "1"
            });
            conf1.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 2",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 2"],
                PositionsToUse = "1"
            });
            conf1.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 0",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 0"],
                PositionsToUse = "1"
            });

            conf2.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 3",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 3"],
                PositionsToUse = "1"
            });
            conf2.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 1",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 1"],
                PositionsToUse = "1"
            });
            conf2.SortingRules.Add(new SortingRule()
            {
                Name = "Conf Div 5",
                GroupNumber = 0,
                DivisionToGetTeamsFrom = divisions["Division 5"],
                PositionsToUse = "1"
            });

            var teamList = StandingsSorter.SortByRules(table.SortIntoDivisions(), divisions["Conference 1"]);

            AreEqual("Team 3", teamList[0].Name);
            AreEqual("Team 7", teamList[1].Name);
            AreEqual("Team 17", teamList[2].Name);
            AreEqual("Team 21", teamList[3].Name);
            
        }

        static string SHOULDSORTNORMALLY_EXPECTED =
    @"League
R    Name               W    L    T  Pts   GP   GF   GA   GD            Div
1    Team 6            28   17   13   69   58  140  133    7     Division 0
2    Team 26           30   20    8   68   58  145  134   11     Division 2
3    Team 3            28   20   10   66   58  159  140   19     Division 3
4    Team 21           27   21   10   64   58  157  138   19     Division 3
5    Team 7            25   19   14   64   58  141  129   12     Division 1
6    Team 27           24   18   16   64   58  159  137   22     Division 3
7    Team 19           27   22    9   63   58  135  121   14     Division 1
8    Team 20           25   20   13   63   58  154  136   18     Division 2
9    Team 9            27   23    8   62   58  151  146    5     Division 3
10   Team 0            26   22   10   62   58  147  138    9     Division 0
11   Team 8            23   19   16   62   58  162  139   23     Division 2
12   Team 16           26   23    9   61   58  152  148    4     Division 4
13   Team 22           24   21   13   61   58  140  139    1     Division 4
14   Team 4            26   24    8   60   58  164  153   11     Division 4
15   Team 13           26   24    8   60   58  152  149    3     Division 1
16   Team 25           25   23   10   60   58  149  148    1     Division 1
17   Team 18           25   24    9   59   58  136  126   10     Division 0
18   Team 17           25   24    9   59   58  150  145    5     Division 5
19   Team 29           26   27    5   57   58  156  153    3     Division 5
20   Team 1            25   26    7   57   58  146  155   -9     Division 1
21   Team 5            24   25    9   57   58  151  145    6     Division 5
22   Team 28           20   23   15   55   58  139  136    3     Division 4
23   Team 2            23   29    6   52   58  136  161  -25     Division 2
24   Team 24           20   27   11   51   58  137  165  -28     Division 0
25   Team 15           17   24   17   51   58  135  150  -15     Division 3
26   Team 10           19   27   12   50   58  147  158  -11     Division 4
27   Team 23           20   29    9   49   58  141  166  -25     Division 5
28   Team 14           18   28   12   48   58  126  151  -25     Division 2
29   Team 11           18   31    9   45   58  139  171  -32     Division 5
30   Team 12           17   34    7   41   58  123  159  -36     Division 0
";
        
    }
}
