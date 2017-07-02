using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Events;

namespace XCode.Event.Handles
{
    /// <summary>
    /// 事件处理
    /// </summary>
    public interface IEventHandler<TEvent>:IHandler where TEvent:IEvent
    {
        /// <summary>
        /// 处理具体的事件
        /// </summary>
        /// <param name="Event"></param>
        void Handle(TEvent Event);

    }
}
