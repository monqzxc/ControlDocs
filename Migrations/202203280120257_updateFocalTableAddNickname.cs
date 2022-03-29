namespace DocsControl.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateFocalTableAddNickname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Focals", "NickName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Focals", "NickName");
        }
    }
}
