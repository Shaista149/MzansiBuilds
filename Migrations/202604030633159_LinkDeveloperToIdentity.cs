namespace MzansiBuilds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LinkDeveloperToIdentity : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Developers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Developers", new[] { "UserId" });
            AddColumn("dbo.AspNetUsers", "Developer_DeveloperId", c => c.Int());
            AlterColumn("dbo.Developers", "UserId", c => c.String());
            CreateIndex("dbo.AspNetUsers", "Developer_DeveloperId");
            AddForeignKey("dbo.AspNetUsers", "Developer_DeveloperId", "dbo.Developers", "DeveloperId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Developer_DeveloperId", "dbo.Developers");
            DropIndex("dbo.AspNetUsers", new[] { "Developer_DeveloperId" });
            AlterColumn("dbo.Developers", "UserId", c => c.String(maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Developer_DeveloperId");
            CreateIndex("dbo.Developers", "UserId");
            AddForeignKey("dbo.Developers", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
