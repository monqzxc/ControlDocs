namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateDocPath : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DocPaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        DocDataID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DocDatas", t => t.DocDataID, cascadeDelete: true)
                .Index(t => t.DocDataID);
            
            DropColumn("dbo.DocDatas", "Link");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocDatas", "Link", c => c.String());
            DropForeignKey("dbo.DocPaths", "DocDataID", "dbo.DocDatas");
            DropIndex("dbo.DocPaths", new[] { "DocDataID" });
            DropTable("dbo.DocPaths");
        }
    }
}
