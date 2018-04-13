namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SortingRules", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SortingRules", "Name");
        }
    }
}
