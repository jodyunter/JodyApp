namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatisgoingon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupRules", "FromSeries", c => c.String());
            DropColumn("dbo.GroupRules", "SeriesName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupRules", "SeriesName", c => c.String());
            DropColumn("dbo.GroupRules", "FromSeries");
        }
    }
}
