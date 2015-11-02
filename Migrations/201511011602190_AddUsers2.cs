namespace Lab01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsers2 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Users", "UserName");
            DropColumn("dbo.Users", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "UserId", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Users");
            AlterColumn("dbo.Users", "UserName", c => c.String());
            AddPrimaryKey("dbo.Users", "UserId");
        }
    }
}
