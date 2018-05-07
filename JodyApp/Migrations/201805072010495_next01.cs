namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class next01 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleRules", "Reverse", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleRules", "Reverse");
        }
    }
}
