namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAddressee : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocDatas", "DocTypeID", "dbo.DocTypes");
            DropForeignKey("dbo.DocDatas", "DocToID", "dbo.Focals");
            DropIndex("dbo.DocDatas", new[] { "DocTypeID" });
            DropIndex("dbo.DocDatas", new[] { "DocFromID" });
            DropIndex("dbo.DocDatas", new[] { "DocToID" });
            RenameColumn(table: "dbo.DocDatas", name: "DocFromID", newName: "Focal_Id");
            CreateTable(
                "dbo.Addressees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Office = c.String(),
                        FullName = c.String(),
                        Email = c.String(),
                        ContactNo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.DocDatas", "DoctTypes", c => c.String());
            AddColumn("dbo.DocDatas", "Addressee_Id", c => c.Int());
            AlterColumn("dbo.DocDatas", "Focal_Id", c => c.Int());
            CreateIndex("dbo.DocDatas", "Addressee_Id");
            CreateIndex("dbo.DocDatas", "Focal_Id");
            AddForeignKey("dbo.DocDatas", "Addressee_Id", "dbo.Addressees", "Id");
            DropColumn("dbo.DocDatas", "DocTypeID");
            DropColumn("dbo.DocDatas", "DocToID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocDatas", "DocToID", c => c.Int(nullable: false));
            AddColumn("dbo.DocDatas", "DocTypeID", c => c.Int(nullable: false));
            DropForeignKey("dbo.DocDatas", "Addressee_Id", "dbo.Addressees");
            DropIndex("dbo.DocDatas", new[] { "Focal_Id" });
            DropIndex("dbo.DocDatas", new[] { "Addressee_Id" });
            AlterColumn("dbo.DocDatas", "Focal_Id", c => c.Int(nullable: false));
            DropColumn("dbo.DocDatas", "Addressee_Id");
            DropColumn("dbo.DocDatas", "DoctTypes");
            DropTable("dbo.Addressees");
            RenameColumn(table: "dbo.DocDatas", name: "Focal_Id", newName: "DocFromID");
            CreateIndex("dbo.DocDatas", "DocToID");
            CreateIndex("dbo.DocDatas", "DocFromID");
            CreateIndex("dbo.DocDatas", "DocTypeID");
            AddForeignKey("dbo.DocDatas", "DocToID", "dbo.Focals", "Id");
            AddForeignKey("dbo.DocDatas", "DocTypeID", "dbo.DocTypes", "Id", cascadeDelete: true);
        }
    }
}
