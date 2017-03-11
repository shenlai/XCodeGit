namespace XCode.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "Temp", c => c.String(unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "Temp");
        }
    }
}
