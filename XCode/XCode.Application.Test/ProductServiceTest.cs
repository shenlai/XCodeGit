using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCode.Application.Implementation;
using XCode.Repositories.EF;

namespace XCode.Application.Test
{
    [TestClass]
    public class ProductServiceTest
    {
        [TestMethod]
        public void TestGetProducts()
        {
            var context = new EntityFrameworkRepositoryContext();
            var pro = new ProductRepository(context);

            var service = new Implementation.ProductService(pro);

            var list = service.GetProducts();

            Assert.IsNotNull(list);
        }
    }
}
