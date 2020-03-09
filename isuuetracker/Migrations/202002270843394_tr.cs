namespace isuuetracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tr : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.bugpools",
                c => new
                    {
                        bugid = c.Int(nullable: false, identity: true),
                        bugname = c.String(nullable: false, maxLength: 30),
                        bugtype = c.String(nullable: false, maxLength: 10),
                        status = c.String(nullable: false, maxLength: 10),
                        assignto_loginId = c.Int(),
                        projectid_projectid = c.Int(nullable: false),
                        testerid_loginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.bugid)
                .ForeignKey("dbo.logins", t => t.assignto_loginId)
                .ForeignKey("dbo.projects", t => t.projectid_projectid, cascadeDelete: true)
                .ForeignKey("dbo.logins", t => t.testerid_loginId, cascadeDelete: true)
                .Index(t => t.assignto_loginId)
                .Index(t => t.projectid_projectid)
                .Index(t => t.testerid_loginId);
            
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
                        comment = c.String(nullable: false, maxLength: 100),
                        status = c.String(nullable: false, maxLength: 10),
                        time = c.DateTime(nullable: false),
                        bugid_bugid = c.Int(nullable: false),
                        userid_loginId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.historyid)
                .ForeignKey("dbo.bugpools", t => t.bugid_bugid, cascadeDelete: true)
                .ForeignKey("dbo.logins", t => t.userid_loginId, cascadeDelete: true)
                .Index(t => t.bugid_bugid)
                .Index(t => t.userid_loginId);
            
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
            DropForeignKey("dbo.histories", "userid_loginId", "dbo.logins");
            DropForeignKey("dbo.histories", "bugid_bugid", "dbo.bugpools");
            DropForeignKey("dbo.bugpools", "testerid_loginId", "dbo.logins");
            DropForeignKey("dbo.bugpools", "projectid_projectid", "dbo.projects");
            DropForeignKey("dbo.bugpools", "assignto_loginId", "dbo.logins");
            DropIndex("dbo.roles", new[] { "userid_loginId" });
            DropIndex("dbo.roles", new[] { "projectid_projectid" });
            DropIndex("dbo.histories", new[] { "userid_loginId" });
            DropIndex("dbo.histories", new[] { "bugid_bugid" });
            DropIndex("dbo.bugpools", new[] { "testerid_loginId" });
            DropIndex("dbo.bugpools", new[] { "projectid_projectid" });
            DropIndex("dbo.bugpools", new[] { "assignto_loginId" });
            DropTable("dbo.roles");
            DropTable("dbo.histories");
            DropTable("dbo.projects");
            DropTable("dbo.logins");
            DropTable("dbo.bugpools");
        }
    }
}
