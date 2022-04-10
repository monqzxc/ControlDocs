namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateDocPathaddColumnDocStatusTag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DocPaths", "DocStatusTag", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DocPaths", "DocStatusTag");
        }
    }
}
