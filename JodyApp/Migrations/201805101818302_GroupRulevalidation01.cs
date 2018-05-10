namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupRulevalidation01 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.GroupRules", "IsHomeTeam");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupRules", "IsHomeTeam", c => c.Boolean(nullable: false));
        }
    }
}
