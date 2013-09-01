namespace FeedForSites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class minleng : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FitFits", "Text", c => c.String(maxLength: 1400));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FitFits", "Text", c => c.String());
        }
    }
}
