using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCode.Domain.Specifications;
using XCode.Infrastructure;

namespace XCode.Domain.IRepositories
{
    public interface IRepository<TAggregateRoot> where TAggregateRoot:class,IAggregateRoot
    {
        TAggregateRoot Add(TAggregateRoot aggregateroot);
        void Update(TAggregateRoot aggregateRoot);
        void Remove(TAggregateRoot aggregateRoot);

        // 根据聚合根的ID值，从仓储中读取聚合根
        TAggregateRoot GetById(int id);

        //// 读取所有聚合根。
        //IEnumerable<TAggregateRoot> GetAll();
        PagedResult<TAggregateRoot> GetEntityList(ISpecification<TAggregateRoot> specification, int pageIndex = 1, int pageSize = int.MaxValue, string ordering = null, string includes = null);



    }
}
