using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace XCode.Domain.Entity.ModelConfigurations
{
    public class ProductConfiguration: EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            //配置主键
            HasKey(p => p.Id);


            //配置字段属性
            Property(x => x.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(x => x.Name).IsRequired().HasMaxLength(30);
            //string不指定长度默认取max
            Property(x => x.Description).IsOptional();
            //指定数值长度6,有效小数点2位
            Property(x => x.UnitPrice).HasPrecision(6, 2);
            

        }
    }
}
