namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ShortName = c.String(),
                        Level = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Parent_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Parent_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Season_Id);
            
            CreateTable(
                "dbo.ScheduleRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeType = c.Int(nullable: false),
                        AwayType = c.Int(nullable: false),
                        PlayHomeAway = c.Boolean(nullable: false),
                        Rounds = c.Int(nullable: false),
                        DivisionLevel = c.Int(nullable: false),
                        Name = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AwayDivision_Id = c.Int(),
                        Season_Id = c.Int(),
                        AwayTeam_Id = c.Int(),
                        HomeDivision_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.AwayDivision_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Divisions", t => t.HomeDivision_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.AwayDivision_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeDivision_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Skill = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Division_Id = c.Int(),
                        Stats_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .ForeignKey("dbo.TeamStatistics", t => t.Stats_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.Division_Id)
                .Index(t => t.Stats_Id)
                .Index(t => t.Season_Id);
            
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
                "dbo.Seasons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SortingRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupNumber = c.Int(nullable: false),
                        PositionsToUse = c.String(),
                        DivisionToGetTeamsFrom_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionToGetTeamsFrom_Id)
                .Index(t => t.DivisionToGetTeamsFrom_Id);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        HomeScore = c.Int(nullable: false),
                        AwayScore = c.Int(nullable: false),
                        CanTie = c.Boolean(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        MaxOverTimePeriods = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        AwayTeam_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Season_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Games", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.SortingRules", "DivisionToGetTeamsFrom_Id", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.ScheduleRules", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.ScheduleRules", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.ScheduleRules", "HomeDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.ScheduleRules", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Teams", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.ScheduleRules", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Teams", "Stats_Id", "dbo.TeamStatistics");
            DropForeignKey("dbo.Teams", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.ScheduleRules", "AwayDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.Divisions", "Parent_Id", "dbo.Divisions");
            DropIndex("dbo.Games", new[] { "Season_Id" });
            DropIndex("dbo.Games", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Games", new[] { "AwayTeam_Id" });
            DropIndex("dbo.SortingRules", new[] { "DivisionToGetTeamsFrom_Id" });
            DropIndex("dbo.Teams", new[] { "Season_Id" });
            DropIndex("dbo.Teams", new[] { "Stats_Id" });
            DropIndex("dbo.Teams", new[] { "Division_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "Division_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "HomeTeam_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "HomeDivision_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "AwayTeam_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "Season_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "AwayDivision_Id" });
            DropIndex("dbo.Divisions", new[] { "Season_Id" });
            DropIndex("dbo.Divisions", new[] { "Parent_Id" });
            DropTable("dbo.Games");
            DropTable("dbo.Leagues");
            DropTable("dbo.SortingRules");
            DropTable("dbo.Seasons");
            DropTable("dbo.TeamStatistics");
            DropTable("dbo.Teams");
            DropTable("dbo.ScheduleRules");
            DropTable("dbo.Divisions");
        }
    }
}
