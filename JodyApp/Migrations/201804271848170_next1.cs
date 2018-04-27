namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.PlayoffSeries", newName: "Series");
            RenameColumn(table: "dbo.Games", name: "PlayoffSeries_Id", newName: "Series_Id");
            RenameIndex(table: "dbo.Games", name: "IX_PlayoffSeries_Id", newName: "IX_Series_Id");
            CreateTable(
                "dbo.RoundRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Round = c.Int(nullable: false),
                        League_Id = c.Int(),
                        Playoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Playoff_Id);
            
            CreateTable(
                "dbo.RoundGroupRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SortType = c.Int(nullable: false),
                        SortyTypeValue = c.Int(nullable: false),
                        FromValue = c.Int(nullable: false),
                        FromDivision_Id = c.Int(),
                        FromSeries_Id = c.Int(),
                        ParentRule_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.FromDivision_Id)
                .ForeignKey("dbo.Series", t => t.FromSeries_Id)
                .ForeignKey("dbo.RoundRules", t => t.ParentRule_Id)
                .Index(t => t.FromDivision_Id)
                .Index(t => t.FromSeries_Id)
                .Index(t => t.ParentRule_Id);
            
            CreateTable(
                "dbo.SeriesRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeTeamFromType = c.Int(nullable: false),
                        HomeTeamFromPoolName = c.String(),
                        HomeTeamFromValue = c.Int(nullable: false),
                        AwayTeamFromType = c.Int(nullable: false),
                        AwayTeamFromPoolName = c.String(),
                        AwayTeamFromValue = c.Int(nullable: false),
                        AwayTeamFromDivision_Id = c.Int(),
                        AwayTeamFromSeries_Id = c.Int(),
                        HomeTeamFromDivision_Id = c.Int(),
                        HomeTeamFromSeries_Id = c.Int(),
                        League_Id = c.Int(),
                        Playoff_Id = c.Int(),
                        Series_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.AwayTeamFromDivision_Id)
                .ForeignKey("dbo.Series", t => t.AwayTeamFromSeries_Id)
                .ForeignKey("dbo.Divisions", t => t.HomeTeamFromDivision_Id)
                .ForeignKey("dbo.Series", t => t.HomeTeamFromSeries_Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.Series", t => t.Series_Id)
                .Index(t => t.AwayTeamFromDivision_Id)
                .Index(t => t.AwayTeamFromSeries_Id)
                .Index(t => t.HomeTeamFromDivision_Id)
                .Index(t => t.HomeTeamFromSeries_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.Series_Id);
            
            AddColumn("dbo.Playoffs", "CurrentRound", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "Round", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "Rule_Id", c => c.Int());
            CreateIndex("dbo.Series", "Rule_Id");
            AddForeignKey("dbo.Series", "Rule_Id", "dbo.SeriesRules", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoundRules", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.RoundRules", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.RoundGroupRules", "ParentRule_Id", "dbo.RoundRules");
            DropForeignKey("dbo.RoundGroupRules", "FromSeries_Id", "dbo.Series");
            DropForeignKey("dbo.Series", "Rule_Id", "dbo.SeriesRules");
            DropForeignKey("dbo.SeriesRules", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesRules", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.SeriesRules", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromSeries_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.SeriesRules", "AwayTeamFromSeries_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesRules", "AwayTeamFromDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.RoundGroupRules", "FromDivision_Id", "dbo.Divisions");
            DropIndex("dbo.SeriesRules", new[] { "Series_Id" });
            DropIndex("dbo.SeriesRules", new[] { "Playoff_Id" });
            DropIndex("dbo.SeriesRules", new[] { "League_Id" });
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromSeries_Id" });
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromDivision_Id" });
            DropIndex("dbo.SeriesRules", new[] { "AwayTeamFromSeries_Id" });
            DropIndex("dbo.SeriesRules", new[] { "AwayTeamFromDivision_Id" });
            DropIndex("dbo.Series", new[] { "Rule_Id" });
            DropIndex("dbo.RoundGroupRules", new[] { "ParentRule_Id" });
            DropIndex("dbo.RoundGroupRules", new[] { "FromSeries_Id" });
            DropIndex("dbo.RoundGroupRules", new[] { "FromDivision_Id" });
            DropIndex("dbo.RoundRules", new[] { "Playoff_Id" });
            DropIndex("dbo.RoundRules", new[] { "League_Id" });
            DropColumn("dbo.Series", "Rule_Id");
            DropColumn("dbo.Series", "Round");
            DropColumn("dbo.Playoffs", "CurrentRound");
            DropTable("dbo.SeriesRules");
            DropTable("dbo.RoundGroupRules");
            DropTable("dbo.RoundRules");
            RenameIndex(table: "dbo.Games", name: "IX_Series_Id", newName: "IX_PlayoffSeries_Id");
            RenameColumn(table: "dbo.Games", name: "Series_Id", newName: "PlayoffSeries_Id");
            RenameTable(name: "dbo.Series", newName: "PlayoffSeries");
        }
    }
}
