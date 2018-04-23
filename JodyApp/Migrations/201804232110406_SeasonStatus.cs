namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeasonStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seasons", "Started", c => c.Boolean(nullable: false));
            AddColumn("dbo.Seasons", "Complete", c => c.Boolean(nullable: false));
            AddColumn("dbo.Seasons", "StartingDay", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Seasons", "StartingDay");
            DropColumn("dbo.Seasons", "Complete");
            DropColumn("dbo.Seasons", "Started");
        }
    }
}
