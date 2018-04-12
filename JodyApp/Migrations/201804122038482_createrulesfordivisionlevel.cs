namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createrulesfordivisionlevel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ScheduleRules", "DivisionLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ScheduleRules", "DivisionLevel");
        }
    }
}
