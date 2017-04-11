using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Domain.Specifications
{
    public interface ISpecification<T>
    {
        bool IsStatisfiedBy(T obj);

        /// <summary>
        /// 获得规约表达式树
        /// </summary>
        Expression<Func<T, bool>> GetExpression { get; }
    }
}
