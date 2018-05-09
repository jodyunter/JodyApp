namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupRuleName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupRules", "Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.GroupRules", "Name");
        }
    }
}
