using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCode.Repositories.EF;
using XCode.Domain;
using XCode.Infrastructure;
using System.Linq.Expressions;
using XCode.Domain.Specifications;
using System.Collections.Generic;

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

           // int x = 0;
        }


        [TestMethod]
        public void TestSpecification()
        {
            Expression<Func<Product, bool>> one = x => x.Name == "人民的名义";
            Expression<Func<Product, bool>> other = y => y.UnitPrice < 100;

            var combine = one.And(other);

            var expressionSpec = new ExpressionSpecification<Product>(combine);

            List<Product> list = new List<Product>() { new Product() { Name = "人民的名义", UnitPrice = 80 }, new Product() { Name = "人民的名义", UnitPrice = 120 } };

            Assert.IsTrue(combine.Compile()(list[0]));
            Assert.IsFalse(combine.Compile()(list[1]));


        }
    }
}
