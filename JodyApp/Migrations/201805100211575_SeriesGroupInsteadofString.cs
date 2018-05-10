namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeriesGroupInsteadofString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SeriesRules", "AwayTeamFromGroup_Id", c => c.Int());
            AddColumn("dbo.SeriesRules", "HomeTeamFromGroup_Id", c => c.Int());
            CreateIndex("dbo.SeriesRules", "AwayTeamFromGroup_Id");
            CreateIndex("dbo.SeriesRules", "HomeTeamFromGroup_Id");
            AddForeignKey("dbo.SeriesRules", "AwayTeamFromGroup_Id", "dbo.Groups", "Id");
            AddForeignKey("dbo.SeriesRules", "HomeTeamFromGroup_Id", "dbo.Groups", "Id");
            DropColumn("dbo.GroupRules", "GroupIdentifier");
            DropColumn("dbo.SeriesRules", "HomeTeamFromGroup");
            DropColumn("dbo.SeriesRules", "AwayTeamFromGroup");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeriesRules", "AwayTeamFromGroup", c => c.String());
            AddColumn("dbo.SeriesRules", "HomeTeamFromGroup", c => c.String());
            AddColumn("dbo.GroupRules", "GroupIdentifier", c => c.String());
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromGroup_Id", "dbo.Groups");
            DropForeignKey("dbo.SeriesRules", "AwayTeamFromGroup_Id", "dbo.Groups");
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromGroup_Id" });
            DropIndex("dbo.SeriesRules", new[] { "AwayTeamFromGroup_Id" });
            DropColumn("dbo.SeriesRules", "HomeTeamFromGroup_Id");
            DropColumn("dbo.SeriesRules", "AwayTeamFromGroup_Id");
        }
    }
}
