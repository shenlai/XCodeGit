﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Events;

namespace XCode.Event.Handles
{
    public class ActionDelegatedEventHandler<TEvent>:IEventHandler<TEvent> where TEvent:IEvent 
    {
        public Action<TEvent> action { get; private set; }


        public ActionDelegatedEventHandler(Action<TEvent> action)
        {
            this.action = action;
        }

        public void Handle(TEvent tEvent)
        {
            action(tEvent);
        }


    }
}
