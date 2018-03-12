namespace JodyApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TeamStatisticsNameFix : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.TeamStatitistics", newName: "TeamStatistics");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.TeamStatistics", newName: "TeamStatitistics");
        }
    }
}
