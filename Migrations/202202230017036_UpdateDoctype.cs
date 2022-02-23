namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDoctype : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocTypes", "Description", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocTypes", "Description");
        }
    }
}
