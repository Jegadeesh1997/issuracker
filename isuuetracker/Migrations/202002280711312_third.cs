namespace isuuetracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.bugpools", "assigntoId", "dbo.logins");
            DropIndex("dbo.bugpools", new[] { "assigntoId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.bugpools", "assigntoId");
            AddForeignKey("dbo.bugpools", "assigntoId", "dbo.logins", "loginId", cascadeDelete: true);
        }
    }
}
