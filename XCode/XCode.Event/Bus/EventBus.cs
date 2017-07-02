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
    /// </summary>
    public class EventBus:IEventBus
    {
        private object locker = new object();

        /// <summary>
        ///  => 属性初始化
        /// </summary>
        public static EventBus bus => new EventBus();

        private static IEnumerable<Assembly> assemblies { get; set; }

        public static readonly ConcurrentDictionary<Type, List<Type>> EventMapping = new ConcurrentDictionary<Type, List<Type>>();


        //注册所有事件
        public void RegisterAllHandler(IEnumerable<Assembly> assembles)
        {
            assemblies = assembles;
            foreach (var assembly in assembles)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    Type handerInterfaceType = type.GetInterface("IEventHandler`1");
                    if (handerInterfaceType != null)
                    {
                        Type eventType = handerInterfaceType.GetGenericArguments()[0];
                        if (!EventMapping.Keys.Contains(eventType))
                        {
                            Register(eventType, type);
                        }
                    }
                }
                
            }


        }

        /// <summary>
        /// 注册到事件总线
        /// </summary>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private List<Type> GetOrCreateHandlers(Type eventType)
        {
            //return EventMapping.GetOrAdd(eventType, (type) => new List<Type>());    No

            //return EventMapping.GetOrAdd(eventType, new List<Type>() { eventType}); OK

            return EventMapping.GetOrAdd(eventType, (type) => new List<Type>() { type });  //OK

            //var list = new List<Type>();
            //if (EventMapping.TryGetValue(eventType, out list))
            //{
            //    return list;
            //}

            //if (EventMapping.TryAdd(eventType, new List<Type> { eventType}))
            //{
            //    list = new List<Type> { eventType };
            //}

            //return list;
        }



        /// <summary>
        /// 手动绑定事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="handle"></param>
        public void Register<THandle>(IHandler handle)
        {
            Register(typeof(THandle), handle.GetType());
        }

        public void Register(Type eventType, Type handler)
        {
            lock (locker)
            {
                GetOrCreateHandlers(eventType).Add(handler);
            }
        }

        /// <summary>
        /// 通过委托注册
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="action"></param>
        public void Register<THandle>(Action<THandle> action) where THandle : IEvent
        {
            ActionHandler<THandle> actionHandler = new ActionHandler<THandle>(action);
            Register<THandle>(actionHandler);
        }



        /// <summary>
        /// 手动卸载单个事件
        /// </summary>
        /// <typeparam name="THandle"></typeparam>
        /// <param name="handleType"></param>
        public void UnRegister<THandle>(Type handleType) where THandle : IEvent
        {
            lock (locker)
            {
                GetOrCreateHandlers(typeof(THandle)).RemoveAll(t => t == handleType);
            }
        }
        /// <summary>
        /// 卸载所有事件
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        public void UnRegisrerAllHandle<Thandle>()
        {
            lock (locker)
            {
                GetOrCreateHandlers(typeof(Thandle)).Clear();
            }
        }


        /// <summary>
        /// 根据事件源触发事件
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        /// <param name="eventHandle"></param>
        /// <returns></returns>
        public void TiggerEvent<Thandle>(Thandle eventData) where Thandle : IEvent
        {
            //获取所有的事件处理
            List<Type> handlerTypes = GetOrCreateHandlers(typeof(Thandle));
            if (handlerTypes != null && handlerTypes.Count > 0)
            {
                foreach (var handlerType in handlerTypes)
                {
                    var handlerInterface = handlerType.GetInterface("IEventHandler`1");
                    foreach (Assembly assembly in assemblies)
                    {
                        Type[] types = assembly.GetTypes();
                        foreach (Type type in types)
                        {
                            Type handlerInterfaceType = handlerType.GetInterface("IEventHandler`1");
                            if (handlerInterfaceType != null)
                            {
                                //判断两个类型是否相等
                                if (handlerInterface == handlerInterfaceType)
                                {
                                    var eventType = handlerInterfaceType.GenericTypeArguments[0];
                                    EventMapping[eventType].ForEach(s =>
                                    {
                                        var obj = Activator.CreateInstance(s) as IEventHandler<Thandle>;
                                        obj?.Handle(eventData);
                                    });
                                }
                            }
                        }
                    }
                }
            }


        }

        /// <summary>
        /// 触发指定事件
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        /// <param name="eventHandlerType"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public void TiggerEvent<Thandle>(Type eventHandlerType, Thandle eventData) where Thandle : IEvent
        {
            var handlerInterface = eventHandlerType.GetInterface("IEventHandler`1");
            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();
                foreach (Type type in types)
                {
                    Type handlerInterfaceType = type.GetInterface("IEventHandler`1");
                    if (handlerInterfaceType != null)
                    {
                        //判断两个类型是否相等
                        if (handlerInterface == handlerInterfaceType)
                        {
                            var eventType = handlerInterfaceType.GenericTypeArguments[0];
                            EventMapping[eventType].ForEach(s =>
                            {
                                var obj = Activator.CreateInstance(s) as IEventHandler<Thandle>;
                                obj?.Handle(eventData);
                            });
                        }
                    }

                }
            }
        }

        /// <summary>
        /// 根据事件源触发事件（异步）
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        /// <param name="eventHandle"></param>
        /// <returns></returns>
        public Task TiggerEventAsync<Thandle>(Thandle eventHandle) where Thandle:IEvent
        {
            return Task.Run(() => { TiggerEvent<Thandle>(eventHandle); });
        }

        /// <summary>
        /// 根据事件源触发事件（异步）
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        /// <param name="eventHandle"></param>
        public async void TiggerEventAsy<Thandle>(Thandle eventHandle) where Thandle : IEvent
        {
            await Task.Run(() => { TiggerEvent<Thandle>(eventHandle); });
            return;
        }

        /// <summary>
        /// 触发指定事件(异步)
        /// </summary>
        /// <typeparam name="Thandle"></typeparam>
        /// <param name="eventHandlerType"></param>
        /// <param name="eventData"></param>
        /// <returns></returns>
        public Task TiggerEventAsync<Thandle>(Type eventHandlerType, Thandle eventData) where Thandle : IEvent
        {
            return Task.Run(() => TiggerEvent<Thandle>(eventHandlerType, eventData));
        }
    }
}
