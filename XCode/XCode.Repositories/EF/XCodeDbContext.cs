using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain;
using XCode.Domain.Entity.ModelConfigurations;

namespace XCode.Repositories.EF
{

    /*Mehdi El Gueddari对EF DbContext封装  https://github.com/mehdime/DbContextScope*/

    //也可在config中配置<entityFramework codeConfigurationType="MySql.Data.Entity.MySqlEFConfiguration, MySql.Data.Entity.EF6">
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public sealed class XCodeDbContext : DbContext
    {
        public XCodeDbContext() 
            : base("name=MySqlDbContext")
        {

            //打印sql log
            this.Database.Log = new Action<string>(p => System.Diagnostics.Debug.WriteLine(p));

        }


        //注册Context下所有实体，EF默认使用方式在DbContext下定义实体如DbSet<Product> Products
        // 通过dbContext.Products.Where(... 方式调用，而非dbContext.Set<Product>().Where(...调用，
        // 也可在此手写方法注册，避免改动量过大 可参考  xx.framework.dal下EFDbContext

        // 所有需要关联到 Context 的类都要类似如下代码这样定义(针对每个聚合根都会定义一个DbSet的属性)
        //codefirst 映射到数据库中Category（程序类名）<--> Categories(数据库表名)
        //public DbSet<Category> Categories { get; set; }
        //public DbSet<Product> Products { get; set; }
        //public DbSet<ProductCategorization> ProductCategorizations { get; set; }

        public DbSet<Category> Categories { get { return this.Set<Category>(); } }
        public DbSet<Product> Products { get { return this.Set<Product>(); } }
        public DbSet<ProductCategorization> ProductCategorizations { get { return this.Set<ProductCategorization>(); } }




        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //Fluent API配置属性
            modelBuilder.Configurations.Add(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
            // register mysql code generator
            new  DbMigrationsConfiguration().SetSqlGenerator("MySql.Data.MySqlClient", new MySql.Data.Entity.MySqlMigrationSqlGenerator());
        }
    }
}
