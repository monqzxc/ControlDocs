namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocDatas", "Remarks", c => c.String());
            DropColumn("dbo.DocDatas", "MotherDocNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DocDatas", "MotherDocNumber", c => c.String());
            DropColumn("dbo.DocDatas", "Remarks");
        }
    }
}
