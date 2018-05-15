namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConfigCompetitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.Int(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        League_Id = c.Int(),
                        Reference_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Reference_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Reference_Id);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CurrentYear = c.Int(nullable: false),
                        SeasonName = c.String(),
                        PlayoffName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReferenceCompetitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Competition_Id = c.Int(),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Competition_Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .Index(t => t.Competition_Id)
                .Index(t => t.League_Id);
            
            CreateTable(
                "dbo.ConfigDivisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Level = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        Competition_Id = c.Int(),
                        League_Id = c.Int(),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Competition_Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.Parent_Id)
                .Index(t => t.Competition_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.ConfigScheduleRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeType = c.Int(nullable: false),
                        AwayType = c.Int(nullable: false),
                        PlayHomeAway = c.Boolean(nullable: false),
                        Rounds = c.Int(nullable: false),
                        DivisionLevel = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Name = c.String(),
                        Reverse = c.Boolean(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        AwayDivision_Id = c.Int(),
                        AwayTeam_Id = c.Int(),
                        HomeDivision_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        League_Id = c.Int(),
                        Season_Id = c.Int(),
                        ConfigDivision_Id = c.Int(),
                        Season_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.AwayDivision_Id)
                .ForeignKey("dbo.ConfigTeams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.HomeDivision_Id)
                .ForeignKey("dbo.ConfigTeams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Season_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.ConfigDivision_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id1)
                .Index(t => t.AwayDivision_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeDivision_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.ConfigDivision_Id)
                .Index(t => t.Season_Id1);
            
            CreateTable(
                "dbo.ConfigTeams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Skill = c.Int(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        Division_Id = c.Int(),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.Division_Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .Index(t => t.Division_Id)
                .Index(t => t.League_Id);
            
            CreateTable(
                "dbo.ConfigSortingRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupNumber = c.Int(nullable: false),
                        PositionsToUse = c.String(),
                        DivisionLevel = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        Division_Id = c.Int(),
                        DivisionToGetTeamsFrom_Id = c.Int(),
                        ConfigDivision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.Division_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.DivisionToGetTeamsFrom_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.ConfigDivision_Id)
                .Index(t => t.Division_Id)
                .Index(t => t.DivisionToGetTeamsFrom_Id)
                .Index(t => t.ConfigDivision_Id);
            
            CreateTable(
                "dbo.ConfigGroupRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        RuleType = c.Int(nullable: false),
                        FromSeries = c.String(),
                        FromStartValue = c.Int(nullable: false),
                        FromEndValue = c.Int(nullable: false),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        FromDivision_Id = c.Int(),
                        FromTeam_Id = c.Int(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.FromDivision_Id)
                .ForeignKey("dbo.ConfigTeams", t => t.FromTeam_Id)
                .ForeignKey("dbo.ConfigGroups", t => t.Group_Id)
                .Index(t => t.FromDivision_Id)
                .Index(t => t.FromTeam_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.ConfigGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        Playoff_Id = c.Int(),
                        SortByDivision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Playoff_Id)
                .ForeignKey("dbo.ConfigDivisions", t => t.SortByDivision_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.SortByDivision_Id);
            
            CreateTable(
                "dbo.ConfigSeriesRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Round = c.Int(nullable: false),
                        HomeTeamFromRank = c.Int(nullable: false),
                        AwayTeamFromRank = c.Int(nullable: false),
                        SeriesType = c.Int(nullable: false),
                        GamesNeeded = c.Int(nullable: false),
                        CanTie = c.Boolean(nullable: false),
                        HomeGames = c.String(),
                        FirstYear = c.Int(),
                        LastYear = c.Int(),
                        AwayTeamFromGroup_Id = c.Int(),
                        HomeTeamFromGroup_Id = c.Int(),
                        Playoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConfigGroups", t => t.AwayTeamFromGroup_Id)
                .ForeignKey("dbo.ConfigGroups", t => t.HomeTeamFromGroup_Id)
                .ForeignKey("dbo.ConfigCompetitions", t => t.Playoff_Id)
                .Index(t => t.AwayTeamFromGroup_Id)
                .Index(t => t.HomeTeamFromGroup_Id)
                .Index(t => t.Playoff_Id);
            
            CreateTable(
                "dbo.DivisionRanks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Division_Id = c.Int(),
                        Team_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .ForeignKey("dbo.Teams", t => t.Team_Id)
                .Index(t => t.Division_Id)
                .Index(t => t.Team_Id);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Level = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        League_Id = c.Int(),
                        Parent_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Divisions", t => t.Parent_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                        Started = c.Boolean(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        StartingDay = c.Int(nullable: false),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .Index(t => t.League_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        GameNumber = c.Int(nullable: false),
                        HomeScore = c.Int(nullable: false),
                        AwayScore = c.Int(nullable: false),
                        CanTie = c.Boolean(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        MaxOverTimePeriods = c.Int(nullable: false),
                        Series_Id = c.Int(),
                        AwayTeam_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Series_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Skill = c.Int(nullable: false),
                        EliminatedFromPlayoff = c.Boolean(nullable: false),
                        Division_Id = c.Int(),
                        Parent_Id = c.Int(),
                        Playoff_Id = c.Int(),
                        Stats_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .ForeignKey("dbo.ConfigTeams", t => t.Parent_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.TeamStatistics", t => t.Stats_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Division_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.Stats_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.Playoffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        StartingDay = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Started = c.Boolean(nullable: false),
                        Name = c.String(),
                        CurrentRound = c.Int(nullable: false),
                        League_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Playoff_Id = c.Int(),
                        SortByDivision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.Divisions", t => t.SortByDivision_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.SortByDivision_Id);
            
            CreateTable(
                "dbo.GroupRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RuleType = c.Int(nullable: false),
                        FromSeries = c.String(),
                        FromStartValue = c.Int(nullable: false),
                        FromEndValue = c.Int(nullable: false),
                        Name = c.String(),
                        FromDivision_Id = c.Int(),
                        FromTeam_Id = c.Int(),
                        Group_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.FromDivision_Id)
                .ForeignKey("dbo.Teams", t => t.FromTeam_Id)
                .ForeignKey("dbo.Groups", t => t.Group_Id)
                .Index(t => t.FromDivision_Id)
                .Index(t => t.FromTeam_Id)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Series",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        Name = c.String(),
                        HomeWins = c.Int(nullable: false),
                        AwayWins = c.Int(nullable: false),
                        AwayTeam_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        Playoff_Id = c.Int(),
                        Rule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.SeriesRules", t => t.Rule_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.Rule_Id);
            
            CreateTable(
                "dbo.SeriesRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Round = c.Int(nullable: false),
                        HomeTeamFromRank = c.Int(nullable: false),
                        AwayTeamFromRank = c.Int(nullable: false),
                        SeriesType = c.Int(nullable: false),
                        GamesNeeded = c.Int(nullable: false),
                        CanTie = c.Boolean(nullable: false),
                        HomeGames = c.String(),
                        AwayTeamFromGroup_Id = c.Int(),
                        HomeTeamFromGroup_Id = c.Int(),
                        Playoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Groups", t => t.AwayTeamFromGroup_Id)
                .ForeignKey("dbo.Groups", t => t.HomeTeamFromGroup_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .Index(t => t.AwayTeamFromGroup_Id)
                .Index(t => t.HomeTeamFromGroup_Id)
                .Index(t => t.Playoff_Id);
            
            CreateTable(
                "dbo.TeamStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Wins = c.Int(nullable: false),
                        Loses = c.Int(nullable: false),
                        Ties = c.Int(nullable: false),
                        GoalsFor = c.Int(nullable: false),
                        GoalsAgainst = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SortingRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupNumber = c.Int(nullable: false),
                        PositionsToUse = c.String(),
                        DivisionLevel = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        DivisionToGetTeamsFrom_Id = c.Int(),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionToGetTeamsFrom_Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.DivisionToGetTeamsFrom_Id)
                .Index(t => t.Division_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DivisionRanks", "Team_Id", "dbo.Teams");
            DropForeignKey("dbo.SortingRules", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.SortingRules", "DivisionToGetTeamsFrom_Id", "dbo.Divisions");
            DropForeignKey("dbo.Teams", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.ConfigScheduleRules", "Season_Id1", "dbo.Seasons");
            DropForeignKey("dbo.Seasons", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.Games", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Games", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Stats_Id", "dbo.TeamStatistics");
            DropForeignKey("dbo.Series", "Rule_Id", "dbo.SeriesRules");
            DropForeignKey("dbo.SeriesRules", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromGroup_Id", "dbo.Groups");
            DropForeignKey("dbo.SeriesRules", "AwayTeamFromGroup_Id", "dbo.Groups");
            DropForeignKey("dbo.Series", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.Series", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.Series", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Playoffs", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Teams", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.Playoffs", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.Groups", "SortByDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.Groups", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.GroupRules", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.GroupRules", "FromTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.GroupRules", "FromDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.Teams", "Parent_Id", "dbo.ConfigTeams");
            DropForeignKey("dbo.Teams", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.DivisionRanks", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "Parent_Id", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ConfigSeriesRules", "Playoff_Id", "dbo.ConfigCompetitions");
            DropForeignKey("dbo.ConfigSeriesRules", "HomeTeamFromGroup_Id", "dbo.ConfigGroups");
            DropForeignKey("dbo.ConfigSeriesRules", "AwayTeamFromGroup_Id", "dbo.ConfigGroups");
            DropForeignKey("dbo.ConfigGroups", "SortByDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigGroups", "Playoff_Id", "dbo.ConfigCompetitions");
            DropForeignKey("dbo.ConfigGroupRules", "Group_Id", "dbo.ConfigGroups");
            DropForeignKey("dbo.ConfigGroupRules", "FromTeam_Id", "dbo.ConfigTeams");
            DropForeignKey("dbo.ConfigGroupRules", "FromDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigSortingRules", "ConfigDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigSortingRules", "DivisionToGetTeamsFrom_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigSortingRules", "Division_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigScheduleRules", "ConfigDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigScheduleRules", "Season_Id", "dbo.ConfigCompetitions");
            DropForeignKey("dbo.ConfigScheduleRules", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ConfigScheduleRules", "HomeTeam_Id", "dbo.ConfigTeams");
            DropForeignKey("dbo.ConfigScheduleRules", "HomeDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigScheduleRules", "AwayTeam_Id", "dbo.ConfigTeams");
            DropForeignKey("dbo.ConfigTeams", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ConfigTeams", "Division_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigScheduleRules", "AwayDivision_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigDivisions", "Parent_Id", "dbo.ConfigDivisions");
            DropForeignKey("dbo.ConfigDivisions", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ConfigDivisions", "Competition_Id", "dbo.ConfigCompetitions");
            DropForeignKey("dbo.ConfigCompetitions", "Reference_Id", "dbo.ConfigCompetitions");
            DropForeignKey("dbo.ConfigCompetitions", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ReferenceCompetitions", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.ReferenceCompetitions", "Competition_Id", "dbo.ConfigCompetitions");
            DropIndex("dbo.SortingRules", new[] { "Division_Id" });
            DropIndex("dbo.SortingRules", new[] { "DivisionToGetTeamsFrom_Id" });
            DropIndex("dbo.SeriesRules", new[] { "Playoff_Id" });
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromGroup_Id" });
            DropIndex("dbo.SeriesRules", new[] { "AwayTeamFromGroup_Id" });
            DropIndex("dbo.Series", new[] { "Rule_Id" });
            DropIndex("dbo.Series", new[] { "Playoff_Id" });
            DropIndex("dbo.Series", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Series", new[] { "AwayTeam_Id" });
            DropIndex("dbo.GroupRules", new[] { "Group_Id" });
            DropIndex("dbo.GroupRules", new[] { "FromTeam_Id" });
            DropIndex("dbo.GroupRules", new[] { "FromDivision_Id" });
            DropIndex("dbo.Groups", new[] { "SortByDivision_Id" });
            DropIndex("dbo.Groups", new[] { "Playoff_Id" });
            DropIndex("dbo.Playoffs", new[] { "Season_Id" });
            DropIndex("dbo.Playoffs", new[] { "League_Id" });
            DropIndex("dbo.Teams", new[] { "Season_Id" });
            DropIndex("dbo.Teams", new[] { "Stats_Id" });
            DropIndex("dbo.Teams", new[] { "Playoff_Id" });
            DropIndex("dbo.Teams", new[] { "Parent_Id" });
            DropIndex("dbo.Teams", new[] { "Division_Id" });
            DropIndex("dbo.Games", new[] { "Season_Id" });
            DropIndex("dbo.Games", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Games", new[] { "AwayTeam_Id" });
            DropIndex("dbo.Games", new[] { "Series_Id" });
            DropIndex("dbo.Seasons", new[] { "League_Id" });
            DropIndex("dbo.Divisions", new[] { "Season_Id" });
            DropIndex("dbo.Divisions", new[] { "Parent_Id" });
            DropIndex("dbo.Divisions", new[] { "League_Id" });
            DropIndex("dbo.DivisionRanks", new[] { "Team_Id" });
            DropIndex("dbo.DivisionRanks", new[] { "Division_Id" });
            DropIndex("dbo.ConfigSeriesRules", new[] { "Playoff_Id" });
            DropIndex("dbo.ConfigSeriesRules", new[] { "HomeTeamFromGroup_Id" });
            DropIndex("dbo.ConfigSeriesRules", new[] { "AwayTeamFromGroup_Id" });
            DropIndex("dbo.ConfigGroups", new[] { "SortByDivision_Id" });
            DropIndex("dbo.ConfigGroups", new[] { "Playoff_Id" });
            DropIndex("dbo.ConfigGroupRules", new[] { "Group_Id" });
            DropIndex("dbo.ConfigGroupRules", new[] { "FromTeam_Id" });
            DropIndex("dbo.ConfigGroupRules", new[] { "FromDivision_Id" });
            DropIndex("dbo.ConfigSortingRules", new[] { "ConfigDivision_Id" });
            DropIndex("dbo.ConfigSortingRules", new[] { "DivisionToGetTeamsFrom_Id" });
            DropIndex("dbo.ConfigSortingRules", new[] { "Division_Id" });
            DropIndex("dbo.ConfigTeams", new[] { "League_Id" });
            DropIndex("dbo.ConfigTeams", new[] { "Division_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "Season_Id1" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "ConfigDivision_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "Season_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "League_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "HomeTeam_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "HomeDivision_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "AwayTeam_Id" });
            DropIndex("dbo.ConfigScheduleRules", new[] { "AwayDivision_Id" });
            DropIndex("dbo.ConfigDivisions", new[] { "Parent_Id" });
            DropIndex("dbo.ConfigDivisions", new[] { "League_Id" });
            DropIndex("dbo.ConfigDivisions", new[] { "Competition_Id" });
            DropIndex("dbo.ReferenceCompetitions", new[] { "League_Id" });
            DropIndex("dbo.ReferenceCompetitions", new[] { "Competition_Id" });
            DropIndex("dbo.ConfigCompetitions", new[] { "Reference_Id" });
            DropIndex("dbo.ConfigCompetitions", new[] { "League_Id" });
            DropTable("dbo.SortingRules");
            DropTable("dbo.TeamStatistics");
            DropTable("dbo.SeriesRules");
            DropTable("dbo.Series");
            DropTable("dbo.GroupRules");
            DropTable("dbo.Groups");
            DropTable("dbo.Playoffs");
            DropTable("dbo.Teams");
            DropTable("dbo.Games");
            DropTable("dbo.Seasons");
            DropTable("dbo.Divisions");
            DropTable("dbo.DivisionRanks");
            DropTable("dbo.ConfigSeriesRules");
            DropTable("dbo.ConfigGroups");
            DropTable("dbo.ConfigGroupRules");
            DropTable("dbo.ConfigSortingRules");
            DropTable("dbo.ConfigTeams");
            DropTable("dbo.ConfigScheduleRules");
            DropTable("dbo.ConfigDivisions");
            DropTable("dbo.ReferenceCompetitions");
            DropTable("dbo.Leagues");
            DropTable("dbo.ConfigCompetitions");
        }
    }
}
