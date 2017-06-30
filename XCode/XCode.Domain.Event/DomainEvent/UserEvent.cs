using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain;
using XCode.Events.Events;

namespace XCode.Domain.Events
{
    public class UserEvent:Event
    {
        public Product info { get; set; }
    }
}
