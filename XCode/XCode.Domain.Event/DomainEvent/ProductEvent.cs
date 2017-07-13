
using XCode.Events;

namespace XCode.Domain.Events
{
    public class ProductEvent: BusinessEvent
    {
        public int ProductID { get; set; }
    }
}
