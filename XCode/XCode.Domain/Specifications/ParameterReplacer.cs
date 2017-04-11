using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Domain.Specifications
{
    /// <summary>
    /// /*对Linq查询支持 SpecExprExtensions 和 ParameterReplacer */
    /// </summary>
    public class ParameterReplacer:ExpressionVisitor
    {
        public ParameterExpression ParameterExpression { get; private set; }

        public ParameterReplacer(ParameterExpression expression)
        {
            this.ParameterExpression = expression;
        }

        public Expression Replace(Expression expr)
        {
            return base.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression expression)
        {
            return this.ParameterExpression;
        }

    }
}
