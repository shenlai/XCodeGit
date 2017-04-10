using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain.IRepositories;
using XCode.Infrastructure;

namespace XCode.Repositories.EF
{
    public abstract class EntityFrameworkRepository<TAggregateRoot>: IRepository<TAggregateRoot> 
    //public class EntityFrameworkRepository<TAggregateRoot> : IRepository<TAggregateRoot>
        where TAggregateRoot : class, IAggregateRoot
    {


        private readonly IEntityFrameworkRepositoryContext efContext;

        //开放到具体Repository中
        protected IEntityFrameworkRepositoryContext EfContext
        {
            get { return this.efContext; }
        }

        //IOC注册EF仓储上下文
        public EntityFrameworkRepository(IRepositoryContext context)
        {
            var ctx = context as IEntityFrameworkRepositoryContext;
            if (ctx != null)
            {
                this.efContext = ctx;
            }
        }

        public TAggregateRoot Add(TAggregateRoot aggregateroot)
        {
            return this.efContext.RegisterNew<TAggregateRoot>(aggregateroot);
        }
        public void Update(TAggregateRoot aggregateRoot)
        {
            this.efContext.RegisterModified<TAggregateRoot>(aggregateRoot);
        }

        public void Remove(TAggregateRoot aggregateRoot)
        {
            this.efContext.RegisterDeleted<TAggregateRoot>(aggregateRoot);
        }

        public TAggregateRoot GetById(int id)
        {
            return this.efContext.DbContext.Set<TAggregateRoot>().Where(x => x.Id == id).FirstOrDefault();
        }
    }
}
