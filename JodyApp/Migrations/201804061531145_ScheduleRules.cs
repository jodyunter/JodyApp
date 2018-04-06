namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleRules : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ScheduleRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HomeType = c.Int(nullable: false),
                        AwayType = c.Int(nullable: false),
                        PlayHomeAway = c.Boolean(nullable: false),
                        AwayDivisio_Id = c.Int(),
                        AwayTeam_Id = c.Int(),
                        HomeDivision_Id = c.Int(),
                        HomeTeam_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.AwayDivisio_Id)
                .ForeignKey("dbo.Teams", t => t.AwayTeam_Id)
                .ForeignKey("dbo.Divisions", t => t.HomeDivision_Id)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_Id)
                .Index(t => t.AwayDivisio_Id)
                .Index(t => t.AwayTeam_Id)
                .Index(t => t.HomeDivision_Id)
                .Index(t => t.HomeTeam_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleRules", "HomeTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.ScheduleRules", "HomeDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.ScheduleRules", "AwayTeam_Id", "dbo.Teams");
            DropForeignKey("dbo.ScheduleRules", "AwayDivisio_Id", "dbo.Divisions");
            DropIndex("dbo.ScheduleRules", new[] { "HomeTeam_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "HomeDivision_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "AwayTeam_Id" });
            DropIndex("dbo.ScheduleRules", new[] { "AwayDivisio_Id" });
            DropTable("dbo.ScheduleRules");
        }
    }
}
