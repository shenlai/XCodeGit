using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain;

namespace XCode.Repositories.EF
{
    public sealed class XCodeDbContext : DbContext
    {
        public XCodeDbContext() 
            : base("MySqlDbContext")
        {
        }

        public DbSet<Product> Contracts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
