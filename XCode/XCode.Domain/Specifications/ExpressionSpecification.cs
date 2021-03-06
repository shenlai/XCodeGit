﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XCode.Domain.Specifications
{
    /// <summary>
    /// 规约实例类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ExpressionSpecification<T>:Specification<T>
    {
        private readonly Expression<Func<T, bool>> expression;

        public ExpressionSpecification(Expression<Func<T, bool>> expression)
        {
            this.expression = expression;
        }

        public override Expression<Func<T, bool>> Expression
        {
            get
            {
                return this.expression;
            }
        }

    }
}
