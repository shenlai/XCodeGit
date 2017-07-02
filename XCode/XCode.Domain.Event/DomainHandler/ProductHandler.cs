using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain.Events;
using XCode.Event.Handles;

namespace XCode.Domain.Event.DomainHandler
{
    public class ProductHandler:IEventHandler<ProductEvent>
    {
        public void Handle(ProductEvent productEvent)
        {
            productEvent.Source = productEvent.info;
        }
    }
}
