using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCode.Repositories.EF;
using XCode.Domain;
using XCode.Infrastructure;

namespace XCode.Repositories.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var context = new EntityFrameworkRepositoryContext();
            var pro = new ProductRepository(context);

            var entity = new Product()
            {
             Name="TEST",
             Description = "TEST  Description",
              UnitPrice = 15.00M,
              IsNew = true,
              ImageUrl = "www.tujia.com"
            };

            pro.Add(entity);

            //context.Commit();

            int x = 0;
        }
    }
}
