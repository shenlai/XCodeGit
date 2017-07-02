using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XCode.Event.Handles;

namespace XCode.Events
{
    public interface IEventBus
    {

        //注册事件
        void RegisterAllHandler(IEnumerable<Assembly> assembles);
        void Register<THandle>(IHandler handle);
        void Register(Type eventType, Type handler);
        void Register<THandle>(Action<THandle> action) where THandle : IEvent;


        //反注册事件
        void UnRegister<THandle>(Type handleType) where THandle : IEvent;
        void UnRegisrerAllHandle<Thandle>();


        //触发事件
        Task TiggerEventAsync<Thandle>(Thandle eventHandle) where Thandle : IEvent;
        Task TiggerEventAsync<Thandle>(Type eventHandlerType, Thandle eventData) where Thandle : IEvent;




    }
}
