namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedConfigDivisionToDivision : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Divisions", "ConfigDivision_Id", c => c.Int());
            CreateIndex("dbo.Divisions", "ConfigDivision_Id");
            AddForeignKey("dbo.Divisions", "ConfigDivision_Id", "dbo.ConfigDivisions", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Divisions", "ConfigDivision_Id", "dbo.ConfigDivisions");
            DropIndex("dbo.Divisions", new[] { "ConfigDivision_Id" });
            DropColumn("dbo.Divisions", "ConfigDivision_Id");
        }
    }
}
