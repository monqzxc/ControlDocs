namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataFocalIDRelationship : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DocDatas", "FocalID", "dbo.Focals");
            DropIndex("dbo.DocDatas", new[] { "FocalID" });
            AlterColumn("dbo.DocDatas", "FocalID", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DocDatas", "FocalID", c => c.Int(nullable: false));
            CreateIndex("dbo.DocDatas", "FocalID");
            AddForeignKey("dbo.DocDatas", "FocalID", "dbo.Focals", "Id", cascadeDelete: true);
        }
    }
}
