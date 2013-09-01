namespace FeedForSites.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.FitFits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserProfile", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.PrivateMessages",
                c => new
                    {
                        PrivateMessageId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        Date = c.DateTime(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        SenderUserId = c.Int(nullable: false),
                        ReceiverUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PrivateMessageId)
                .ForeignKey("dbo.UserProfile", t => t.SenderUserId, cascadeDelete: false)
                .ForeignKey("dbo.UserProfile", t => t.ReceiverUserId, cascadeDelete: false)
                .Index(t => t.SenderUserId)
                .Index(t => t.ReceiverUserId);
            
            CreateTable(
                "dbo.Friendships",
                c => new
                    {
                        FriendshipId = c.Int(nullable: false, identity: true),
                        FriendOneId = c.Int(nullable: false),
                        FriendTwoId = c.Int(nullable: false),
                        Approval = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.FriendshipId)
                .ForeignKey("dbo.UserProfile", t => t.FriendOneId, cascadeDelete: false)
                .ForeignKey("dbo.UserProfile", t => t.FriendTwoId, cascadeDelete: false)
                .Index(t => t.FriendOneId)
                .Index(t => t.FriendTwoId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Friendships", new[] { "FriendTwoId" });
            DropIndex("dbo.Friendships", new[] { "FriendOneId" });
            DropIndex("dbo.PrivateMessages", new[] { "ReceiverUserId" });
            DropIndex("dbo.PrivateMessages", new[] { "SenderUserId" });
            DropIndex("dbo.FitFits", new[] { "UserId" });
            DropForeignKey("dbo.Friendships", "FriendTwoId", "dbo.UserProfile");
            DropForeignKey("dbo.Friendships", "FriendOneId", "dbo.UserProfile");
            DropForeignKey("dbo.PrivateMessages", "ReceiverUserId", "dbo.UserProfile");
            DropForeignKey("dbo.PrivateMessages", "SenderUserId", "dbo.UserProfile");
            DropForeignKey("dbo.FitFits", "UserId", "dbo.UserProfile");
            DropTable("dbo.Friendships");
            DropTable("dbo.PrivateMessages");
            DropTable("dbo.FitFits");
            DropTable("dbo.UserProfile");
        }
    }
}
