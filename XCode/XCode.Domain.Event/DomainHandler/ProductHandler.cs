using System;
using XCode.Domain.Events;
using XCode.Event.Handles;

namespace XCode.Domain.Event.DomainHandler
{
    public class ProductHandler:IEventHandler<ProductEvent>
    {
        public void Handle(ProductEvent productEvent)
        {

            //短信通知

            int i = 0;
            i = i * 100;
        }
    }
}
