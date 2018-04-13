namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecordTableDivision : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SortingRules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GroupNumber = c.Int(nullable: false),
                        PositionsToUse = c.String(),
                        DivisionToGetTeamsFrom_Id = c.Int(),
                        DivisionToSort_Id = c.Int(),
                        RecordTableDivision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionToGetTeamsFrom_Id)
                .ForeignKey("dbo.Divisions", t => t.DivisionToSort_Id)
                .ForeignKey("dbo.Divisions", t => t.RecordTableDivision_Id)
                .Index(t => t.DivisionToGetTeamsFrom_Id)
                .Index(t => t.DivisionToSort_Id)
                .Index(t => t.RecordTableDivision_Id);
            
            CreateTable(
                "dbo.Leagues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SortingRules", "RecordTableDivision_Id", "dbo.Divisions");
            DropForeignKey("dbo.SortingRules", "DivisionToSort_Id", "dbo.Divisions");
            DropForeignKey("dbo.SortingRules", "DivisionToGetTeamsFrom_Id", "dbo.Divisions");
            DropIndex("dbo.SortingRules", new[] { "RecordTableDivision_Id" });
            DropIndex("dbo.SortingRules", new[] { "DivisionToSort_Id" });
            DropIndex("dbo.SortingRules", new[] { "DivisionToGetTeamsFrom_Id" });
            DropTable("dbo.Leagues");
            DropTable("dbo.SortingRules");
        }
    }
}
