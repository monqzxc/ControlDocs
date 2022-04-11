namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelsFocalPlantilla : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plantillas", "Position", c => c.String());
            AlterColumn("dbo.Focals", "ContactNumber", c => c.String());
            DropColumn("dbo.Plantillas", "Item");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plantillas", "Item", c => c.String());
            AlterColumn("dbo.Focals", "ContactNumber", c => c.Double());
            DropColumn("dbo.Plantillas", "Position");
        }
    }
}
