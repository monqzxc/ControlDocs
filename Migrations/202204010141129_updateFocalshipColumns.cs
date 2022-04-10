namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFocalshipColumns : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Focalships", "Acronym", c => c.String());
            DropColumn("dbo.Focalships", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Focalships", "Description", c => c.String());
            DropColumn("dbo.Focalships", "Acronym");
        }
    }
}
