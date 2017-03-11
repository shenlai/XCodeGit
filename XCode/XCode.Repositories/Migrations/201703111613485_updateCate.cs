namespace XCode.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateCate : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Categories", "Temp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Temp", c => c.String(unicode: false));
        }
    }
}
