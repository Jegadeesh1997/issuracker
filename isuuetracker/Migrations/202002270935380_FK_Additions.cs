namespace isuuetracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FK_Additions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bugpools",
                c => new
                    {
                        bugid = c.Int(nullable: false, identity: true),
                        testerid = c.Int(nullable: false),
                        projectid = c.Int(nullable: false),
                        bugname = c.String(nullable: false, maxLength: 30),
                        bugtype = c.String(nullable: false, maxLength: 10),
                        status = c.String(nullable: false, maxLength: 10),
                        assigntoId = c.Int(nullable: true),
                    })
                .PrimaryKey(t => t.bugid)
                .ForeignKey("dbo.logins", t => t.assigntoId, cascadeDelete: false)
                .ForeignKey("dbo.projects", t => t.projectid, cascadeDelete: true)
                .ForeignKey("dbo.logins", t => t.testerid, cascadeDelete: false)
                .Index(t => t.testerid)
                .Index(t => t.projectid)
                .Index(t => t.assigntoId);
            
            CreateTable(
                "dbo.logins",
                c => new
                    {
                        loginId = c.Int(nullable: false, identity: true),
                        username = c.String(nullable: false, maxLength: 30),
                        password = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.loginId);
            
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        projectid = c.Int(nullable: false, identity: true),
                        projectname = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.projectid);
            
            CreateTable(
                "dbo.histories",
                c => new
                    {
                        historyid = c.Int(nullable: false, identity: true),
                        bugid = c.Int(nullable: false),
                        ModifieduserId = c.Int(nullable: false),
                        comment = c.String(nullable: false, maxLength: 100),
                        status = c.String(nullable: false, maxLength: 10),
                        time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.historyid)
                .ForeignKey("dbo.bugpools", t => t.bugid, cascadeDelete: true)
                .ForeignKey("dbo.logins", t => t.ModifieduserId, cascadeDelete: true)
                .Index(t => t.bugid)
                .Index(t => t.ModifieduserId);
            
            CreateTable(
                "dbo.roles",
                c => new
                    {
                        rid = c.Int(nullable: false, identity: true),
                        work = c.String(nullable: false, maxLength: 10),
                        projectid_projectid = c.Int(nullable: false),
                        userid_loginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.rid)
                .ForeignKey("dbo.projects", t => t.projectid_projectid, cascadeDelete: true)
                .ForeignKey("dbo.logins", t => t.userid_loginId, cascadeDelete: true)
                .Index(t => t.projectid_projectid)
                .Index(t => t.userid_loginId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.roles", "userid_loginId", "dbo.logins");
            DropForeignKey("dbo.roles", "projectid_projectid", "dbo.projects");
            DropForeignKey("dbo.histories", "ModifieduserId", "dbo.logins");
            DropForeignKey("dbo.histories", "bugid", "dbo.bugpools");
            DropForeignKey("dbo.bugpools", "testerid", "dbo.logins");
            DropForeignKey("dbo.bugpools", "projectid", "dbo.projects");
            DropForeignKey("dbo.bugpools", "assigntoId", "dbo.logins");
            DropIndex("dbo.roles", new[] { "userid_loginId" });
            DropIndex("dbo.roles", new[] { "projectid_projectid" });
            DropIndex("dbo.histories", new[] { "ModifieduserId" });
            DropIndex("dbo.histories", new[] { "bugid" });
            DropIndex("dbo.bugpools", new[] { "assigntoId" });
            DropIndex("dbo.bugpools", new[] { "projectid" });
            DropIndex("dbo.bugpools", new[] { "testerid" });
            DropTable("dbo.roles");
            DropTable("dbo.histories");
            DropTable("dbo.projects");
            DropTable("dbo.logins");
            DropTable("dbo.bugpools");
        }
    }
}
