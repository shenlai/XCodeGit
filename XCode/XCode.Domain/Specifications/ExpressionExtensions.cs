using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Domain.Specifications
{
    //ParameterExpression类的作用是将一个表达式里的所有ParameterExpression替换成我们指定的新对象

    /// <summary>
    /// /*对Linq查询支持 SpecExprExtensions 和 ParameterReplacer */
    /// 简单查询不需要定义规约，扩展的And,Not,Or直接重组lambda表达式
    /// </summary>
    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var parameters = expression.Parameters;
            var body = expression.Body;
            return Expression.Lambda<Func<T, bool>>(body, parameters);
        }


        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> other)
        {
            // 首先定义好一个ParameterExpression
            var parameterExp = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(parameterExp);

            // // 将表达式树的参数统一替换成我们定义好的candidateExpr (表达式分为 参数和body，将参数统一成一个对象)
            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(other.Body);

            var combindBody = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(combindBody, parameterExp);
        }

        
        public static Expression<Func<T, bool>> Or<T>(
            this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

    }
}
