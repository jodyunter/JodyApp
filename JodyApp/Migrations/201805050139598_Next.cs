namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Next : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Series", "HomeWins", c => c.Int(nullable: false));
            AddColumn("dbo.Series", "AwayWins", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Series", "AwayWins");
            DropColumn("dbo.Series", "HomeWins");
        }
    }
}
