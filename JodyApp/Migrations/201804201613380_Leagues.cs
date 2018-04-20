namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leagues : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Divisions", "League_Id", c => c.Int());
            CreateIndex("dbo.Divisions", "League_Id");
            AddForeignKey("dbo.Divisions", "League_Id", "dbo.Leagues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Divisions", "League_Id", "dbo.Leagues");
            DropIndex("dbo.Divisions", new[] { "League_Id" });
            DropColumn("dbo.Divisions", "League_Id");
        }
    }
}
