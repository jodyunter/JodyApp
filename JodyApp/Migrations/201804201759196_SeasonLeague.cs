namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeasonLeague : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Seasons", "League_Id", c => c.Int());
            CreateIndex("dbo.Seasons", "League_Id");
            AddForeignKey("dbo.Seasons", "League_Id", "dbo.Leagues", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Seasons", "League_Id", "dbo.Leagues");
            DropIndex("dbo.Seasons", new[] { "League_Id" });
            DropColumn("dbo.Seasons", "League_Id");
        }
    }
}
