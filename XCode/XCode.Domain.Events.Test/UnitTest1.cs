using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XCode.Events;

namespace XCode.Domain.Events.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestProductEvent()
        {
            EventBus.bus.TiggerEvent<ProductEvent>(new ProductEvent());
            
        }
    }
}
