namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupChange01 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GroupRules", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.GroupRules", "SortByDivision_Id", "dbo.Divisions");
            DropIndex("dbo.GroupRules", new[] { "Playoff_Id" });
            DropIndex("dbo.GroupRules", new[] { "SortByDivision_Id" });
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Playoff_Id = c.Int(),
                        SortByDivision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Playoffs", t => t.Playoff_Id)
                .ForeignKey("dbo.Divisions", t => t.SortByDivision_Id)
                .Index(t => t.Playoff_Id)
                .Index(t => t.SortByDivision_Id);
            
            AddColumn("dbo.GroupRules", "Group_Id", c => c.Int());
            CreateIndex("dbo.GroupRules", "Group_Id");
            AddForeignKey("dbo.GroupRules", "Group_Id", "dbo.Groups", "Id");
            DropColumn("dbo.GroupRules", "Playoff_Id");
            DropColumn("dbo.GroupRules", "SortByDivision_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.GroupRules", "SortByDivision_Id", c => c.Int());
            AddColumn("dbo.GroupRules", "Playoff_Id", c => c.Int());
            DropForeignKey("dbo.Groups", "SortByDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.Groups", "Playoff_Id", "dbo.Playoffs");
            DropForeignKey("dbo.GroupRules", "Group_Id", "dbo.Groups");
            DropIndex("dbo.GroupRules", new[] { "Group_Id" });
            DropIndex("dbo.Groups", new[] { "SortByDivision_Id" });
            DropIndex("dbo.Groups", new[] { "Playoff_Id" });
            DropColumn("dbo.GroupRules", "Group_Id");
            DropTable("dbo.Groups");
            CreateIndex("dbo.GroupRules", "SortByDivision_Id");
            CreateIndex("dbo.GroupRules", "Playoff_Id");
            AddForeignKey("dbo.GroupRules", "SortByDivision_Id", "dbo.Divisions", "Id");
            AddForeignKey("dbo.GroupRules", "Playoff_Id", "dbo.Playoffs", "Id");
        }
    }
}
