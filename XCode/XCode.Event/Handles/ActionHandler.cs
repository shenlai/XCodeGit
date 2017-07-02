using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Events;

namespace XCode.Event.Handles
{
    public class ActionHandler<TEvent>:IEventHandler<TEvent> where TEvent:IEvent 
    {
        public Action<TEvent> action { get; private set; }

        public ActionHandler()
        {
        }


        public ActionHandler(Action<TEvent> handler)
        {
            action = handler;
        }

        public void Handle(TEvent Event)
        {
            throw new NotImplementedException();
        }


    }
}
