using DoNet基础.EntityFramework.模型;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础.表达式树
{

    public static class SpecExprExtensions
    {
        /// <summary>
        /// Not
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> one)
        {
            var candidateExpr = one.Parameters[0];
            var body = Expression.Not(one.Body);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// And
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.And(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }

        /// <summary>
        /// OR
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="one"></param>
        /// <param name="another"></param>
        /// <returns></returns>
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> one, Expression<Func<T, bool>> another)
        {
            var candidateExpr = Expression.Parameter(typeof(T), "candidate");
            var parameterReplacer = new ParameterReplacer(candidateExpr);

            var left = parameterReplacer.Replace(one.Body);
            var right = parameterReplacer.Replace(another.Body);
            var body = Expression.Or(left, right);

            return Expression.Lambda<Func<T, bool>>(body, candidateExpr);
        }
    }

    public class 用法
    {
        public void yongFa()
        {
            Expression<Func<int, bool>> f = (i => i % 2 == 0);
            f = f.Not();

            foreach (var i in new int[] { 1, 2, 3, 4, 5, 6 }.AsQueryable().Where(f))
            {
                Console.WriteLine(i);
            }

            Expression<Func<int, bool>> f2 = i => i % 2 == 0;
            f2 = f2.Not()
                .And(i => i % 3 == 0).
                Or(i => i % 4 == 0);
            f2 = f2.Or(i => i % 4 == 0);

            foreach (var i in new int[] { 1, 2, 3, 4, 5, 6 }.AsQueryable().Where(f2))
            {
                Console.WriteLine(i);
            }
        }
    }


    public abstract class ProductCriteria
    {
        public Expression<Func<Base_Area, bool>> Query { get; private set; }

        internal ProductCriteria(Expression<Func<Base_Area, bool>> query)
        {
            this.Query = query;
        }
    }

    class 测试
    {
        public Base_Area GetProduct(ProductCriteria predicate)
        {


            return new Base_Area();
        }
    }

    public class AreaByIDCriteria : ProductCriteria
    {
        public AreaByIDCriteria(int id) : base(p => p.ID == id)
        {
        }
    }
    public class AreaByIDsCriteria : ProductCriteria
    {
        public AreaByIDsCriteria(int min, int max) : base(p => p.ID > min && p.ID < max)
        {
        }
    }


    /// <summary>
    /// 重写
    /// </summary>
    internal class ParameterReplacer : ExpressionVisitor
    {
        public ParameterExpression ParameterExpression { get; private set; }

        public ParameterReplacer(ParameterExpression paramExpr)
        {
            this.ParameterExpression = paramExpr;
        }

        public Expression Replace(Expression expr)
        {
            return this.Visit(expr);
        }

        protected override Expression VisitParameter(ParameterExpression p)
        {
            return this.ParameterExpression;
        }
    }
}
