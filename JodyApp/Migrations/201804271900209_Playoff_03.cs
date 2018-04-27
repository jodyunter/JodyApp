namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playoff_03 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromSeries_Id", "dbo.Series");
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromDivision_Id" });
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromSeries_Id" });
            RenameColumn(table: "dbo.SeriesRules", name: "AwayTeamFromDivision_Id", newName: "TeamFromDivision_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "AwayTeamFromSeries_Id", newName: "TeamFromSeries_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_AwayTeamFromDivision_Id", newName: "IX_TeamFromDivision_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_AwayTeamFromSeries_Id", newName: "IX_TeamFromSeries_Id");
            AddColumn("dbo.SeriesRules", "TeamFromType", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "TeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "TeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "HomeTeam", c => c.Boolean(nullable: false));
            DropColumn("dbo.SeriesRules", "HomeTeamFromType");
            DropColumn("dbo.SeriesRules", "HomeTeamFromPoolName");
            DropColumn("dbo.SeriesRules", "HomeTeamFromValue");
            DropColumn("dbo.SeriesRules", "AwayTeamFromType");
            DropColumn("dbo.SeriesRules", "AwayTeamFromPoolName");
            DropColumn("dbo.SeriesRules", "AwayTeamFromValue");
            DropColumn("dbo.SeriesRules", "HomeTeamFromDivision_Id");
            DropColumn("dbo.SeriesRules", "HomeTeamFromSeries_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeriesRules", "HomeTeamFromSeries_Id", c => c.Int());
            AddColumn("dbo.SeriesRules", "HomeTeamFromDivision_Id", c => c.Int());
            AddColumn("dbo.SeriesRules", "AwayTeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "AwayTeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "AwayTeamFromType", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "HomeTeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "HomeTeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "HomeTeamFromType", c => c.Int(nullable: false));
            DropColumn("dbo.SeriesRules", "HomeTeam");
            DropColumn("dbo.SeriesRules", "TeamFromValue");
            DropColumn("dbo.SeriesRules", "TeamFromPoolName");
            DropColumn("dbo.SeriesRules", "TeamFromType");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_TeamFromSeries_Id", newName: "IX_AwayTeamFromSeries_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_TeamFromDivision_Id", newName: "IX_AwayTeamFromDivision_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "TeamFromSeries_Id", newName: "AwayTeamFromSeries_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "TeamFromDivision_Id", newName: "AwayTeamFromDivision_Id");
            CreateIndex("dbo.SeriesRules", "HomeTeamFromSeries_Id");
            CreateIndex("dbo.SeriesRules", "HomeTeamFromDivision_Id");
            AddForeignKey("dbo.SeriesRules", "HomeTeamFromSeries_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.SeriesRules", "HomeTeamFromDivision_Id", "dbo.Divisions", "Id");
        }
    }
}
