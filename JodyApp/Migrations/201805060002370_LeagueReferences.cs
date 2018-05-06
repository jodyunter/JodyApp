namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LeagueReferences : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReferenceCompetitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        League_Id = c.Int(),
                        Playoff_Id = c.Int(),
                        Season_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Leagues", t => t.League_Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.Seasons", t => t.Season_Id)
                .Index(t => t.League_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.Season_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferenceCompetitions", "Season_Id", "dbo.Seasons");
            DropForeignKey("dbo.ReferenceCompetitions", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.ReferenceCompetitions", "League_Id", "dbo.Leagues");
            DropIndex("dbo.ReferenceCompetitions", new[] { "Season_Id" });
            DropIndex("dbo.ReferenceCompetitions", new[] { "Playoff_Id" });
            DropIndex("dbo.ReferenceCompetitions", new[] { "League_Id" });
            DropTable("dbo.ReferenceCompetitions");
        }
    }
}
