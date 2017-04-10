using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using XCode.Infrastructure;

namespace XCode.Repositories.EF
{
    /// <summary>
    /// EF仓储上下文实现
    /// 完成工作单元具体实现，线程、session、异常、资源管理
    /// </summary>
    public class EntityFrameworkRepositoryContext : IEntityFrameworkRepositoryContext
    {
        // ThreadLocal代表线程本地存储，主要相当于一个静态变量
        // 但静态变量在多线程访问时需要显式使用线程同步技术。
        // 使用ThreadLocal变量，每个线程都会一个拷贝，从而避免了线程同步带来的性能开销
        private readonly ThreadLocal<XCodeDbContext> localCtx = new ThreadLocal<XCodeDbContext>(() => new XCodeDbContext());
        
        public DbContext DbContext { get { return this.localCtx.Value; } }


        
        public TAggregateRoot RegisterNew<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot
        {
            return this.localCtx.Value.Set<TAggregateRoot>().Add(entity);
        }

        public void RegisterModified<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot
        {
            this.localCtx.Value.Entry<TAggregateRoot>(entity).State = EntityState.Modified;
        }

        public void RegisterDeleted<TAggregateRoot>(TAggregateRoot entity)
            where TAggregateRoot : class, IAggregateRoot
        {
            this.localCtx.Value.Set<TAggregateRoot>().Remove(entity);
        }


        public void Commit()
        {
            var validationErr = this.localCtx.Value.GetValidationErrors();
            this.localCtx.Value.SaveChanges();
        }



        public bool Committed { get; protected set; }


        public void Rollback()
        {

        }

        public void Dispose()
        {

        }





    }
}
