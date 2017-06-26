using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using XCode.Application;
using XCode.Application.Implementation;
using XCode.Domain.IRepositories;
using XCode.Repositories.EF;

namespace XCode.WebApi.App_Start
{
    public class UnityContainerConfig
    {
        public static IUnityContainer RegisterUnityDependency()
        {
            IUnityContainer container = new UnityContainer();
            RegisterTypes(container);
            return container;
        }

        private static void RegisterTypes(IUnityContainer container)
        {

            //单例实体
            container.RegisterType<IRepositoryContext, EntityFrameworkRepositoryContext>(new ContainerControlledLifetimeManager());

            #region repository
            container.RegisterType<IProductRepository, ProductRepository>();
            #endregion

            #region service
            container.RegisterType<IProductService, ProductService>();
            #endregion


        }

    }

    public class UnityDependencyResolver : IDependencyResolver
    {
        public readonly IUnityContainer container;
        public UnityDependencyResolver(IUnityContainer container)
        {
            this.container = container;
        }

        //判断一下serviceType是否被注册，如果没有被注册，就返回null。ASP.NET MVC得到null返回值，会自己解析这个接口
        public object GetService(Type serviceType)
        {
            try
            {
                return this.container.Resolve(serviceType);
            }
            catch(ResolutionFailedException)
            {
                // 按微软的要求，此方法，在没有解析到任何对象的情况下，必须返回 null，必须这么做！！！！
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return this.container.ResolveAll(serviceType);
            }
            catch(ResolutionFailedException)
            {
                // 按微软的要求，此方法，在没有解析到任何对象的情况下，必须返回 null，必须这么做！！！！
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = container.CreateChildContainer();
            return new UnityDependencyResolver(child);
        }


        public void Dispose()
        {
            this.container.Dispose();
        }
       


    }
}