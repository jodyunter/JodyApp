namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SortingRules", "DivisionLevel", c => c.Int(nullable: false));
            AddColumn("dbo.SortingRules", "Type", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SortingRules", "Type");
            DropColumn("dbo.SortingRules", "DivisionLevel");
        }
    }
}
