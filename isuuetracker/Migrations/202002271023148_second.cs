namespace isuuetracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.roles", name: "projectid_projectid", newName: "projectid");
            RenameColumn(table: "dbo.roles", name: "userid_loginId", newName: "userid");
            RenameIndex(table: "dbo.roles", name: "IX_userid_loginId", newName: "IX_userid");
            RenameIndex(table: "dbo.roles", name: "IX_projectid_projectid", newName: "IX_projectid");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.roles", name: "IX_projectid", newName: "IX_projectid_projectid");
            RenameIndex(table: "dbo.roles", name: "IX_userid", newName: "IX_userid_loginId");
            RenameColumn(table: "dbo.roles", name: "userid", newName: "userid_loginId");
            RenameColumn(table: "dbo.roles", name: "projectid", newName: "projectid_projectid");
        }
    }
}
