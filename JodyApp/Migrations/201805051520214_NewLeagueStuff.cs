namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewLeagueStuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leagues", "CurrentYear", c => c.Int(nullable: false));
            AddColumn("dbo.Leagues", "SeasonName", c => c.String());
            AddColumn("dbo.Leagues", "PlayoffName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leagues", "PlayoffName");
            DropColumn("dbo.Leagues", "SeasonName");
            DropColumn("dbo.Leagues", "CurrentYear");
        }
    }
}
