namespace MzansiBuilds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Celebrations",
                c => new
                    {
                        CelebrationId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        CelebratedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CelebrationId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Developers",
                c => new
                    {
                        DeveloperId = c.Int(nullable: false, identity: true),
                        UserId = c.String(maxLength: 128),
                        Username = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Bio = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DeveloperId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        DeveloperId = c.Int(nullable: false),
                        Content = c.String(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId)
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ProjectId = c.Int(nullable: false, identity: true),
                        DeveloperId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 200),
                        Description = c.String(),
                        Stage = c.String(maxLength: 50),
                        SupportNeeded = c.String(),
                        IsComplete = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProjectId)
                .ForeignKey("dbo.Developers", t => t.DeveloperId)
                .Index(t => t.DeveloperId);
            
            CreateTable(
                "dbo.CollaborationRequests",
                c => new
                    {
                        RequestId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        RequesterId = c.Int(nullable: false),
                        Message = c.String(),
                        Status = c.String(maxLength: 20),
                        CreatedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.RequestId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .ForeignKey("dbo.Developers", t => t.RequesterId)
                .Index(t => t.ProjectId)
                .Index(t => t.RequesterId);
            
            CreateTable(
                "dbo.Milestones",
                c => new
                    {
                        MilestoneId = c.Int(nullable: false, identity: true),
                        ProjectId = c.Int(nullable: false),
                        Description = c.String(nullable: false),
                        AchievedAt = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MilestoneId)
                .ForeignKey("dbo.Projects", t => t.ProjectId, cascadeDelete: true)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Celebrations", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Celebrations", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.Developers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Milestones", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Projects", "DeveloperId", "dbo.Developers");
            DropForeignKey("dbo.Comments", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.CollaborationRequests", "RequesterId", "dbo.Developers");
            DropForeignKey("dbo.CollaborationRequests", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.Comments", "DeveloperId", "dbo.Developers");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Milestones", new[] { "ProjectId" });
            DropIndex("dbo.CollaborationRequests", new[] { "RequesterId" });
            DropIndex("dbo.CollaborationRequests", new[] { "ProjectId" });
            DropIndex("dbo.Projects", new[] { "DeveloperId" });
            DropIndex("dbo.Comments", new[] { "DeveloperId" });
            DropIndex("dbo.Comments", new[] { "ProjectId" });
            DropIndex("dbo.Developers", new[] { "UserId" });
            DropIndex("dbo.Celebrations", new[] { "DeveloperId" });
            DropIndex("dbo.Celebrations", new[] { "ProjectId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Milestones");
            DropTable("dbo.CollaborationRequests");
            DropTable("dbo.Projects");
            DropTable("dbo.Comments");
            DropTable("dbo.Developers");
            DropTable("dbo.Celebrations");
        }
    }
}
