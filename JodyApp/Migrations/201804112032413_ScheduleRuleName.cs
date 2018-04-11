namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleRuleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleRules", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleRules", "Name");
        }
    }
}
