namespace MzansiBuilds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsDeletedToDeveloper : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Developers", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Developers", "IsDeleted");
        }
    }
}
