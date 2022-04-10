namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataDateTimeColumntoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DocDatas", "DateAdd", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DocDatas", "DateAdd", c => c.DateTime(nullable: false));
        }
    }
}
