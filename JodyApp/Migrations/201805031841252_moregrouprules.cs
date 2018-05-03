namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moregrouprules : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.GroupRules", "FromTeam_Id", c => c.Int());
            CreateIndex("dbo.GroupRules", "FromTeam_Id");
            AddForeignKey("dbo.GroupRules", "FromTeam_Id", "dbo.Teams", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GroupRules", "FromTeam_Id", "dbo.Teams");
            DropIndex("dbo.GroupRules", new[] { "FromTeam_Id" });
            DropColumn("dbo.GroupRules", "FromTeam_Id");
        }
    }
}
