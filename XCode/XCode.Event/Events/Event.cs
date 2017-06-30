using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Events.Events
{
    public class Event : IEvent
    {
        public DateTime CreateTime { get; set; }

        public object Source { get; set; }

        public Event()
        {
            CreateTime = DateTime.Now;
        }
    }


}
