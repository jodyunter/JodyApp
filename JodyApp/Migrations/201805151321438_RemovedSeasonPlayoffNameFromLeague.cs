namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedSeasonPlayoffNameFromLeague : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Leagues", "SeasonName");
            DropColumn("dbo.Leagues", "PlayoffName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Leagues", "PlayoffName", c => c.String());
            AddColumn("dbo.Leagues", "SeasonName", c => c.String());
        }
    }
}
