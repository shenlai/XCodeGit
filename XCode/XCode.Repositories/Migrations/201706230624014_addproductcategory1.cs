namespace XCode.Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproductcategory1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductCategorizations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ProductCategorizations");
        }
    }
}
