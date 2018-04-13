namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGamesTables : DbMigration
    {
        public override void Up()
        {
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
            DropIndex("dbo.Games", new[] { "Season_Id" });
            DropIndex("dbo.Games", new[] { "HomeTeam_Id" });
            DropIndex("dbo.Games", new[] { "AwayTeam_Id" });
            DropTable("dbo.Games");
        }
    }
}
