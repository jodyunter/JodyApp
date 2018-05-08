namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seasonforplayoffs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Playoffs", "Season_Id", c => c.Int());
            CreateIndex("dbo.Playoffs", "Season_Id");
            AddForeignKey("dbo.Playoffs", "Season_Id", "dbo.Seasons", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Playoffs", "Season_Id", "dbo.Seasons");
            DropIndex("dbo.Playoffs", new[] { "Season_Id" });
            DropColumn("dbo.Playoffs", "Season_Id");
        }
    }
}
