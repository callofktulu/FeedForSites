namespace FeedForSites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minlen : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FitFits", "Text", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FitFits", "Text", c => c.String(maxLength: 1400));
        }
    }
}
