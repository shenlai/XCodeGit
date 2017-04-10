namespace XCode.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false, maxLength: 30, storeType: "nvarchar"));
            AlterColumn("dbo.Products", "UnitPrice", c => c.Decimal(nullable: false, precision: 6, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Products", "Name", c => c.String(unicode: false));
        }
    }
}
