namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PlayoffLeagueReferences : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeriesRules", "League_Id", "dbo.Leagues");
            DropForeignKey("dbo.GroupRules", "League_Id", "dbo.Leagues");
            DropIndex("dbo.GroupRules", new[] { "League_Id" });
            DropIndex("dbo.SeriesRules", new[] { "League_Id" });
            DropColumn("dbo.GroupRules", "League_Id");
            DropColumn("dbo.SeriesRules", "League_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeriesRules", "League_Id", c => c.Int());
            AddColumn("dbo.GroupRules", "League_Id", c => c.Int());
            CreateIndex("dbo.SeriesRules", "League_Id");
            CreateIndex("dbo.GroupRules", "League_Id");
            AddForeignKey("dbo.GroupRules", "League_Id", "dbo.Leagues", "Id");
            AddForeignKey("dbo.SeriesRules", "League_Id", "dbo.Leagues", "Id");
        }
    }
}
