namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataTable030722 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocDatas", "Addressee_Id", "dbo.Addressees");
            DropForeignKey("dbo.DocDatas", "Focal_Id", "dbo.Focals");
            DropIndex("dbo.DocDatas", new[] { "Addressee_Id" });
            DropIndex("dbo.DocDatas", new[] { "Focal_Id" });
            RenameColumn(table: "dbo.DocDatas", name: "Addressee_Id", newName: "AddresseeID");
            RenameColumn(table: "dbo.DocDatas", name: "Focal_Id", newName: "FocalID");
            AlterColumn("dbo.DocDatas", "AddresseeID", c => c.Int(nullable: false));
            AlterColumn("dbo.DocDatas", "FocalID", c => c.Int(nullable: false));
            CreateIndex("dbo.DocDatas", "FocalID");
            CreateIndex("dbo.DocDatas", "AddresseeID");
            AddForeignKey("dbo.DocDatas", "AddresseeID", "dbo.Addressees", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DocDatas", "FocalID", "dbo.Focals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DocDatas", "FocalID", "dbo.Focals");
            DropForeignKey("dbo.DocDatas", "AddresseeID", "dbo.Addressees");
            DropIndex("dbo.DocDatas", new[] { "AddresseeID" });
            DropIndex("dbo.DocDatas", new[] { "FocalID" });
            AlterColumn("dbo.DocDatas", "FocalID", c => c.Int());
            AlterColumn("dbo.DocDatas", "AddresseeID", c => c.Int());
            RenameColumn(table: "dbo.DocDatas", name: "FocalID", newName: "Focal_Id");
            RenameColumn(table: "dbo.DocDatas", name: "AddresseeID", newName: "Addressee_Id");
            CreateIndex("dbo.DocDatas", "Focal_Id");
            CreateIndex("dbo.DocDatas", "Addressee_Id");
            AddForeignKey("dbo.DocDatas", "Focal_Id", "dbo.Focals", "Id");
            AddForeignKey("dbo.DocDatas", "Addressee_Id", "dbo.Addressees", "Id");
        }
    }
}
