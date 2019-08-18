using DoNet基础.EntityFramework.模型;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DoNet基础.表达式树
{
    public class 杂
    {
        public void 临时()
        {
            ConstantExpression exp1 = Expression.Constant(1);
            ConstantExpression exp2 = Expression.Constant(2);

            BinaryExpression exp12 = Expression.Add(exp1, exp2);

            ConstantExpression exp3 = Expression.Constant(3);

            BinaryExpression exp123 = Expression.Add(exp12, exp3);
        }
        public void 临时2()
        {
            ParameterExpression expA = Expression.Parameter(typeof(double), "a"); //参数a
            MethodCallExpression expCall = Expression.Call(null,
                typeof(Math).GetMethod("Sin", BindingFlags.Static | BindingFlags.Public),
                expA); //相当于Math.Sin(a)

            LambdaExpression exp = Expression.Lambda(expCall, expA); // a => Math.Sin(a)
        }


        public void 临时3()
        {
            string[] starts = new string[] { "", "" };
            ParameterExpression c = Expression.Parameter(typeof(Base_Area), "c");
            Expression condition = Expression.Constant(false);

            foreach (string value in starts)
            {
                MemberExpression member = Expression.Property(c, typeof(Base_Area).GetProperty("CustomerID"));
                MethodInfo method = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                ConstantExpression constant = Expression.Constant(value);

                Expression con = Expression.Call(member, method, constant);
                condition = Expression.Or(con, condition);
            }
            Expression<Func<Base_Area, bool>> end =
                Expression.Lambda<Func<Base_Area, bool>>(condition, new ParameterExpression[] { c });
        }


    }


}
