using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCode.Events;
using XCode.Domain.Event.DomainHandler;

namespace XCode.Domain.Events.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProductEvent()
        {
            //订阅
            EventBus.Instance.Subscribe(new ProductHandler());
            var entity = new ProductEvent { ProductID = 1 };
            EventBus.Instance.Publish(entity);

            Assert.IsTrue(true);

        }
    }
}
