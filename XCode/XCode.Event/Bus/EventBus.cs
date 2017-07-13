using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCode.Event.Handles;


//async await  http://www.cnblogs.com/xianyudotnet/p/5716908.html
//http://www.cnblogs.com/jesse2013/p/async-and-await.html


namespace XCode.Events
{
    /// <summary>
    /// 事件总线
    /// 发布与订阅处理逻辑
    /// </summary>
    public class EventBus//:IEventBus
    {
        private EventBus() { }

        private static EventBus bus = null;

        //初始化空的事件总件
        public static EventBus Instance
        {
            get {
                return bus ?? (bus = new EventBus());
            }
        }


        private object locker = new object();

        //public static readonly ConcurrentDictionary<Type, List<Type>> EventMapping = new ConcurrentDictionary<Type, List<Type>>();
        //事件数据存储，暂时采用内存字典
        private static Dictionary<Type, List<object>> eventHandlersList = new Dictionary<Type, List<object>>();//object : IEventHandler<TEvent>

        private readonly Func<object, object, bool> eventHandlerEquals = (oIn1, oIn2) =>
        {
            var oIn1Type = oIn1.GetType();
            var oIn2Type = oIn2.GetType();

            if (oIn1Type.IsGenericType && oIn1Type.GetGenericTypeDefinition() == typeof(ActionDelegatedEventHandler<>) &&
            oIn2Type.IsGenericType && oIn2Type.GetGenericTypeDefinition()==typeof(ActionDelegatedEventHandler<>))
            {
                return oIn1.Equals(oIn2);
            }

            return oIn1Type == oIn2Type;
        };

        /// <summary>
        /// 订阅事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent:class,IEvent
        {
            lock (locker)
            {
                var eventType = typeof(TEvent);
                if (eventHandlersList.ContainsKey(eventType))
                {
                    var handers = eventHandlersList[eventType];
                    if (handers != null)
                    {
                        if (!handers.Exists(x => eventHandlerEquals(x, eventHandler)))
                        {
                            handers.Add(eventHandler);
                        }

                    }
                    else
                    {
                        handers = new List<object>() { eventHandler };
                    }
                }
                else
                {
                    eventHandlersList.Add(eventType, new List<object>() { eventHandler });
                }
            }
        }

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="eventHandler"></param>
        public void UnSubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : class, IEvent
        {
            lock (locker)
            {
                var eventType = typeof(TEvent);
                if (eventHandlersList.ContainsKey(eventType))
                {
                    var handlers = eventHandlersList[eventType];
                    if (handlers!=null)
                    {
                        if (handlers.Exists(x => eventHandlerEquals(x, eventHandler)))
                        {
                            var handerToRemove = handlers.First(x => eventHandlerEquals(x, eventHandler));
                            handlers.Remove(handerToRemove);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 发布事件
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="evnt"></param>
        public void Publish<TEvent>(TEvent evnt) where TEvent : class, IEvent
        {
            if (evnt == null)
            {
                throw new ArgumentNullException("evnt");
            }

            var eventType = evnt.GetType();
            if (eventHandlersList.ContainsKey(eventType) && eventHandlersList[eventType] != null && eventHandlersList[eventType].Count > 0)
            {
                var handlers = eventHandlersList[eventType];
                foreach (var handler in handlers)
                {
                    var eventHandler = handler as IEventHandler<TEvent>;

                    //if (eventHandler.GetType().IsDefined(typeof(HandlesAsynchronouslyAttribute), false))
                    //{
                    //    Task.Factory.StartNew((o) => eventHandler.Handle((TEvent)o), evnt);
                    //}
                    eventHandler.Handle(evnt);
                }


            }

        }














    }
}
