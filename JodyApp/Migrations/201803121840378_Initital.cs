namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
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
                "dbo.Teams",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Stats_Id = c.Int(),
                        Season_Id = c.Int(),
                        Division_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TeamStatitistics", t => t.Stats_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .ForeignKey("dbo.Divisions", t => t.Division_Id)
                .Index(t => t.Stats_Id)
                .Index(t => t.Season_Id)
                .Index(t => t.Division_Id);
            
            CreateTable(
                "dbo.TeamStatitistics",
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
                        Name = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Divisions", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Teams", "Division_Id", "dbo.Divisions");
            DropForeignKey("dbo.Teams", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.Teams", "Stats_Id", "dbo.TeamStatitistics");
            DropForeignKey("dbo.Divisions", "Parent_Id", "dbo.Divisions");
            DropIndex("dbo.Teams", new[] { "Division_Id" });
            DropIndex("dbo.Teams", new[] { "Season_Id" });
            DropIndex("dbo.Teams", new[] { "Stats_Id" });
            DropIndex("dbo.Divisions", new[] { "Season_Id" });
            DropIndex("dbo.Divisions", new[] { "Parent_Id" });
            DropTable("dbo.Seasons");
            DropTable("dbo.TeamStatitistics");
            DropTable("dbo.Teams");
            DropTable("dbo.Divisions");
        }
    }
}
