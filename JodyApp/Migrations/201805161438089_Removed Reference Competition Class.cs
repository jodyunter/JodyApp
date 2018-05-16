namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedReferenceCompetitionClass : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReferenceCompetitions", "Competition_Id", "dbo.ConfigCompetitions");
            DropIndex("dbo.ReferenceCompetitions", new[] { "Competition_Id" });
            DropIndex("dbo.ReferenceCompetitions", new[] { "League_Id" });
            AddColumn("dbo.ConfigCompetitions", "Order", c => c.Int(nullable: false));
            DropTable("dbo.ReferenceCompetitions");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ReferenceCompetitions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Competition_Id = c.Int(),
                        League_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.ConfigCompetitions", "Order");
            CreateIndex("dbo.ReferenceCompetitions", "League_Id");
            CreateIndex("dbo.ReferenceCompetitions", "Competition_Id");
            AddForeignKey("dbo.ReferenceCompetitions", "Competition_Id", "dbo.ConfigCompetitions", "Id");
        }
    }
}
