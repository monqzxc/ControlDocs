namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocDataDateAddDateTimeColumn : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DocDatas", "DateAdd", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DocDatas", "DateAdd", c => c.String());
        }
    }
}
