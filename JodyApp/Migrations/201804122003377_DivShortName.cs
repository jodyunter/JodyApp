namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DivShortName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Divisions", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Divisions", "ShortName");
        }
    }
}
