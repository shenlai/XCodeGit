using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Events
{
    public class BusinessEvent : IEvent
    {
        public DateTime CreateTime { get; set; }

        public object Source { get; set; }

        public BusinessEvent()
        {
            CreateTime = DateTime.Now;
        }
    }


}
