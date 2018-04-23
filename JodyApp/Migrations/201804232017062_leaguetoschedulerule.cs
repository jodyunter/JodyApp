namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leaguetoschedulerule : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleRules", "League_Id", c => c.Int());
            CreateIndex("dbo.ScheduleRules", "League_Id");
            AddForeignKey("dbo.ScheduleRules", "League_Id", "dbo.Leagues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleRules", "League_Id", "dbo.Leagues");
            DropIndex("dbo.ScheduleRules", new[] { "League_Id" });
            DropColumn("dbo.ScheduleRules", "League_Id");
        }
    }
}
