namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moreplayoffstuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Series", "Id", "dbo.SeriesRules");
            DropForeignKey("dbo.Games", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series");
            DropIndex("dbo.Series", new[] { "Id" });
            RenameColumn(table: "dbo.GroupRules", name: "SortyByDivision_Id", newName: "SortByDivision_Id");
            RenameIndex(table: "dbo.GroupRules", name: "IX_SortyByDivision_Id", newName: "IX_SortByDivision_Id");
            DropPrimaryKey("dbo.Series");
            AddColumn("dbo.GroupRules", "RuleType", c => c.Int(nullable: false));
            AddColumn("dbo.GroupRules", "IsHomeTeam", c => c.Boolean(nullable: false));
            AddColumn("dbo.Series", "Name", c => c.String());
            AddColumn("dbo.Series", "Rule_Id", c => c.Int(nullable: false));
            AddColumn("dbo.SeriesRules", "Name", c => c.String());
            AddColumn("dbo.SeriesRules", "Round", c => c.Int(nullable: false));
            AlterColumn("dbo.Series", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Series", "Id");
            CreateIndex("dbo.Series", "Rule_Id");
            AddForeignKey("dbo.Series", "Rule_Id", "dbo.SeriesRules", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Games", "Series_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series", "Id");
            DropColumn("dbo.GroupRules", "SortType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupRules", "SortType", c => c.Int(nullable: false));
            DropForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series");
            DropForeignKey("dbo.Games", "Series_Id", "dbo.Series");
            DropForeignKey("dbo.Series", "Rule_Id", "dbo.SeriesRules");
            DropIndex("dbo.Series", new[] { "Rule_Id" });
            DropPrimaryKey("dbo.Series");
            AlterColumn("dbo.Series", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.SeriesRules", "Round");
            DropColumn("dbo.SeriesRules", "Name");
            DropColumn("dbo.Series", "Rule_Id");
            DropColumn("dbo.Series", "Name");
            DropColumn("dbo.GroupRules", "IsHomeTeam");
            DropColumn("dbo.GroupRules", "RuleType");
            AddPrimaryKey("dbo.Series", "Id");
            RenameIndex(table: "dbo.GroupRules", name: "IX_SortByDivision_Id", newName: "IX_SortyByDivision_Id");
            RenameColumn(table: "dbo.GroupRules", name: "SortByDivision_Id", newName: "SortyByDivision_Id");
            CreateIndex("dbo.Series", "Id");
            AddForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.Games", "Series_Id", "dbo.Series", "Id");
            AddForeignKey("dbo.Series", "Id", "dbo.SeriesRules", "Id");
        }
    }
}
