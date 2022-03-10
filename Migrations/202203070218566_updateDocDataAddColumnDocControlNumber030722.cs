namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataAddColumnDocControlNumber030722 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocDatas", "DocControlNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocDatas", "DocControlNumber");
        }
    }
}
