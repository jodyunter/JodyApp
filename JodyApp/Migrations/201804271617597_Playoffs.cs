namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playoffs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Playoffs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.Int(nullable: false),
                        StartingDay = c.Int(nullable: false),
                        Complete = c.Boolean(nullable: false),
                        Started = c.Boolean(nullable: false),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .Index(t => t.League_Id);
            
            CreateTable(
                "dbo.PlayoffSeries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AwayTeam_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                        Playoff_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeTeam_Id)
                .Index(t => t.Playoff_Id);
            
            AddColumn("dbo.Teams", "Playoff_Id", c => c.Int());
            AddColumn("dbo.Games", "Playoff_Id", c => c.Int());
            AddColumn("dbo.Games", "PlayoffSeries_Id", c => c.Int());
            CreateIndex("dbo.Teams", "Playoff_Id");
            CreateIndex("dbo.Games", "Playoff_Id");
            CreateIndex("dbo.Games", "PlayoffSeries_Id");
            AddForeignKey("dbo.Games", "Playoff_Id", "dbo.Playoffs", "Id");
            AddForeignKey("dbo.Games", "PlayoffSeries_Id", "dbo.PlayoffSeries", "Id");
            AddForeignKey("dbo.Teams", "Playoff_Id", "dbo.Playoffs", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teams", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.PlayoffSeries", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.PlayoffSeries", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Games", "PlayoffSeries_Id", "dbo.PlayoffSeries");
            DropForeignKey("dbo.Games", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.PlayoffSeries", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.Playoffs", "League_Id", "dbo.Leagues");
            DropIndex("dbo.Games", new[] { "PlayoffSeries_Id" });
            DropIndex("dbo.Games", new[] { "Playoff_Id" });
            DropIndex("dbo.PlayoffSeries", new[] { "Playoff_Id" });
            DropIndex("dbo.PlayoffSeries", new[] { "HomeTeam_Id" });
            DropIndex("dbo.PlayoffSeries", new[] { "AwayTeam_Id" });
            DropIndex("dbo.Playoffs", new[] { "League_Id" });
            DropIndex("dbo.Teams", new[] { "Playoff_Id" });
            DropColumn("dbo.Games", "PlayoffSeries_Id");
            DropColumn("dbo.Games", "Playoff_Id");
            DropColumn("dbo.Teams", "Playoff_Id");
            DropTable("dbo.PlayoffSeries");
            DropTable("dbo.Playoffs");
        }
    }
}
