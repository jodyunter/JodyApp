namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeriesName : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series");
            DropIndex("dbo.GroupRules", new[] { "FromSeries_Id" });
            AddColumn("dbo.GroupRules", "SeriesName", c => c.String());
            DropColumn("dbo.GroupRules", "FromSeries_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupRules", "FromSeries_Id", c => c.Int());
            DropColumn("dbo.GroupRules", "SeriesName");
            CreateIndex("dbo.GroupRules", "FromSeries_Id");
            AddForeignKey("dbo.GroupRules", "FromSeries_Id", "dbo.Series", "Id");
        }
    }
}
