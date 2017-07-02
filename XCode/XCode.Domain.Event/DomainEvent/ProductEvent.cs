using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain;
using XCode.Events;

namespace XCode.Domain.Events
{
    public class ProductEvent: BusinessEvent
    {
        public Product info { get; set; }
    }
}
