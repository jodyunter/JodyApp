namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ScheduleRuleRounds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleRules", "Rounds", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleRules", "Rounds");
        }
    }
}
