using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Domain.Specifications
{
    public abstract class Specification<T>:ISpecification<T>
    {
        public bool IsStatisfiedBy(T obj)
        {
            return this.Expression.Compile()(obj);
        }

        /// <summary>
        /// 获得规约表达式树
        /// </summary>
        public abstract Expression<Func<T, bool>> Expression { get; }
    }
}
