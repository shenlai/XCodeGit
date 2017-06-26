using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain.IRepositories;
using XCode.Domain.Specifications;
using XCode.Infrastructure;
using System.Linq.Dynamic;

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

        public PagedResult<TAggregateRoot> GetEntityList(ISpecification<TAggregateRoot> specification, int pageIndex = 1, int pageSize = int.MaxValue, string ordering = null, string includes = null) 
        {
            try
            {
                if (pageIndex <= 0)
                    throw new ArgumentOutOfRangeException("pageNumber", pageIndex, "页码必须大于等于1");
                if (pageSize <= 0)
                    throw new ArgumentOutOfRangeException("pageSize", pageSize, "每页大小必须大于或等于1");

                //using (var context = this.efContext.DbContext)
                //{

                    var query = this.efContext.DbContext.Set<TAggregateRoot>() as IQueryable<TAggregateRoot>;
                    //var query = context.Set<TAggregateRoot>() as IQueryable<TAggregateRoot>;
                    //饥饿加载
                    //if (!string.IsNullOrEmpty(includes))
                    //{
                    //    foreach (var include in includes.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    //        ret = ret.Include(include.Trim());
                    //}

                    if (specification != null)
                    {
                        query = query.Where(specification.Expression);
                    }
                    
                    if (string.IsNullOrEmpty(ordering))
                    {
                        query = query.OrderBy(i => i.Id);
                    }
                    else
                    {
                        query = query.OrderBy(ordering, new object[] { });
                    }
                    var count = query.Count();

                    var list = query.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();

                    return new PagedResult<TAggregateRoot>(count, pageSize, pageIndex, list);
                //}
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
    }
}
