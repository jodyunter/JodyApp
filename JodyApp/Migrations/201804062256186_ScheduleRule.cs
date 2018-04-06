namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleRule : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ScheduleRules", name: "AwayDivisio_Id", newName: "AwayDivision_Id");
            RenameIndex(table: "dbo.ScheduleRules", name: "IX_AwayDivisio_Id", newName: "IX_AwayDivision_Id");
            AddColumn("dbo.ScheduleRules", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ScheduleRules", "Season_Id", c => c.Int());
            CreateIndex("dbo.ScheduleRules", "Season_Id");
            AddForeignKey("dbo.ScheduleRules", "Season_Id", "dbo.Seasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScheduleRules", "Season_Id", "dbo.Seasons");
            DropIndex("dbo.ScheduleRules", new[] { "Season_Id" });
            DropColumn("dbo.ScheduleRules", "Season_Id");
            DropColumn("dbo.ScheduleRules", "Discriminator");
            RenameIndex(table: "dbo.ScheduleRules", name: "IX_AwayDivision_Id", newName: "IX_AwayDivisio_Id");
            RenameColumn(table: "dbo.ScheduleRules", name: "AwayDivision_Id", newName: "AwayDivisio_Id");
        }
    }
}
