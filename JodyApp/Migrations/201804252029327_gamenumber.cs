namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gamenumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "GameNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "GameNumber");
        }
    }
}
