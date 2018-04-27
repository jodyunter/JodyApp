namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Playoff_04 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SeriesRules", name: "TeamFromDivision_Id", newName: "AwayTeamFromDivision_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "TeamFromSeries_Id", newName: "AwayTeamFromSeries_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_TeamFromDivision_Id", newName: "IX_AwayTeamFromDivision_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_TeamFromSeries_Id", newName: "IX_AwayTeamFromSeries_Id");
            AddColumn("dbo.SeriesRules", "HomeTeamFromType", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "HomeTeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "HomeTeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "AwayTeamFromType", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "AwayTeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "AwayTeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "SeriesType", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "GamesNeeded", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "CanTie", c => c.Boolean(nullable: false));
            AddColumn("dbo.SeriesRules", "HomeGames", c => c.String());
            AddColumn("dbo.SeriesRules", "HomeTeamFromDivision_Id", c => c.Int());
            AddColumn("dbo.SeriesRules", "HomeTeamFromSeries_Id", c => c.Int());
            CreateIndex("dbo.SeriesRules", "HomeTeamFromDivision_Id");
            CreateIndex("dbo.SeriesRules", "HomeTeamFromSeries_Id");
            AddForeignKey("dbo.SeriesRules", "HomeTeamFromDivision_Id", "dbo.Divisions", "Id");
            AddForeignKey("dbo.SeriesRules", "HomeTeamFromSeries_Id", "dbo.Series", "Id");
            DropColumn("dbo.SeriesRules", "TeamFromType");
            DropColumn("dbo.SeriesRules", "TeamFromPoolName");
            DropColumn("dbo.SeriesRules", "TeamFromValue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SeriesRules", "TeamFromValue", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "TeamFromPoolName", c => c.String());
            AddColumn("dbo.SeriesRules", "TeamFromType", c => c.Int(nullable: false));
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromSeries_Id", "dbo.Series");
            DropForeignKey("dbo.SeriesRules", "HomeTeamFromDivision_Id", "dbo.Divisions");
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromSeries_Id" });
            DropIndex("dbo.SeriesRules", new[] { "HomeTeamFromDivision_Id" });
            DropColumn("dbo.SeriesRules", "HomeTeamFromSeries_Id");
            DropColumn("dbo.SeriesRules", "HomeTeamFromDivision_Id");
            DropColumn("dbo.SeriesRules", "HomeGames");
            DropColumn("dbo.SeriesRules", "CanTie");
            DropColumn("dbo.SeriesRules", "GamesNeeded");
            DropColumn("dbo.SeriesRules", "SeriesType");
            DropColumn("dbo.SeriesRules", "AwayTeamFromValue");
            DropColumn("dbo.SeriesRules", "AwayTeamFromPoolName");
            DropColumn("dbo.SeriesRules", "AwayTeamFromType");
            DropColumn("dbo.SeriesRules", "HomeTeamFromValue");
            DropColumn("dbo.SeriesRules", "HomeTeamFromPoolName");
            DropColumn("dbo.SeriesRules", "HomeTeamFromType");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_AwayTeamFromSeries_Id", newName: "IX_TeamFromSeries_Id");
            RenameIndex(table: "dbo.SeriesRules", name: "IX_AwayTeamFromDivision_Id", newName: "IX_TeamFromDivision_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "AwayTeamFromSeries_Id", newName: "TeamFromSeries_Id");
            RenameColumn(table: "dbo.SeriesRules", name: "AwayTeamFromDivision_Id", newName: "TeamFromDivision_Id");
        }
    }
}
