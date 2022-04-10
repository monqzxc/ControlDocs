namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocSubject = c.String(),
                        CurrentStatus = c.String(),
                        ForSigned = c.DateTime(),
                        Signed = c.DateTime(),
                        ForRelease = c.DateTime(),
                        Link = c.String(),
                        DocTypeID = c.Int(nullable: false),
                        DocFromID = c.Int(nullable: false),
                        DocToID = c.Int(nullable: false),
                        DateAdd = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocTypes", t => t.DocTypeID, cascadeDelete: true)
                .ForeignKey("dbo.Focals", t => t.DocFromID)
                .ForeignKey("dbo.Focals", t => t.DocToID)
                .Index(t => t.DocTypeID)
                .Index(t => t.DocFromID)
                .Index(t => t.DocToID);
            
            CreateTable(
                "dbo.DocTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DocType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Focals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        ContactNumber = c.Double(),
                        Email = c.String(),
                        PlantillaID = c.Int(nullable: false),
                        FocalshipID = c.Int(nullable: false),
                        OfficeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Focalships", t => t.FocalshipID, cascadeDelete: true)
                .ForeignKey("dbo.Offices", t => t.OfficeID, cascadeDelete: true)
                .ForeignKey("dbo.Plantillas", t => t.PlantillaID, cascadeDelete: true)
                .Index(t => t.PlantillaID)
                .Index(t => t.FocalshipID)
                .Index(t => t.OfficeID);
            
            CreateTable(
                "dbo.Focalships",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FocalshipName = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Offices",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OperatingUnit = c.String(),
                        ContactNo = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Plantillas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Item = c.String(),
                        Acronym = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        NickName = c.String(),
                        Sex = c.String(),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Focals", "PlantillaID", "dbo.Plantillas");
            DropForeignKey("dbo.Focals", "OfficeID", "dbo.Offices");
            DropForeignKey("dbo.Focals", "FocalshipID", "dbo.Focalships");
            DropForeignKey("dbo.DocDatas", "DocToID", "dbo.Focals");
            DropForeignKey("dbo.DocDatas", "DocFromID", "dbo.Focals");
            DropForeignKey("dbo.DocDatas", "DocTypeID", "dbo.DocTypes");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Focals", new[] { "OfficeID" });
            DropIndex("dbo.Focals", new[] { "FocalshipID" });
            DropIndex("dbo.Focals", new[] { "PlantillaID" });
            DropIndex("dbo.DocDatas", new[] { "DocToID" });
            DropIndex("dbo.DocDatas", new[] { "DocFromID" });
            DropIndex("dbo.DocDatas", new[] { "DocTypeID" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Plantillas");
            DropTable("dbo.Offices");
            DropTable("dbo.Focalships");
            DropTable("dbo.Focals");
            DropTable("dbo.DocTypes");
            DropTable("dbo.DocDatas");
        }
    }
}
