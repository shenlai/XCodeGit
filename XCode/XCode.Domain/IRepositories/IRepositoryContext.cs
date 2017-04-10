
using System;
using XCode.Infrastructure;

namespace XCode.Domain.IRepositories
{

    /// <summary>
    /// 仓储上下文接口,持久化定义
    /// 这里把传统的IUnitOfWork接口中方法分别在2个接口定义：一个是IUnitOfWork,另一个就是该接口
    /// </summary>
    public interface IRepositoryContext:IUnitOfWork, IDisposable
    {
        TAggregateRoot RegisterNew<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;

        void RegisterModified<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;

        void RegisterDeleted<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot;
    }
}
