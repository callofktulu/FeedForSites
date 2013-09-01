namespace FeedForSites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageConversationId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PrivateMessages", "ConversationId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PrivateMessages", "ConversationId");
        }
    }
}
