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
    //也可在config中配置<entityFramework codeConfigurationType="MySql.Data.Entity.MySqlEFConfiguration, MySql.Data.Entity.EF6">
    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public sealed class XCodeDbContext : DbContext
    {
        public XCodeDbContext() 
            : base("MySqlDbContext")
        {
        }

        //codefirst 映射到数据库中Category（程序类名）<--> Categories(数据库表名)
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Contracts { get; set; }

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
