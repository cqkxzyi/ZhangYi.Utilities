using DoNet基础.EntityFramework;
using DoNet基础.EntityFramework.模型;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoNet基础.Linq
{
    /// <summary>   
    /// 订单实体 
    /// </summary>   
    public class Order 
    {
        /// <summary>   
        /// 订单名称   
        /// </summary>   
        public string OrderName { get; set; }
        /// <summary>   
        /// 下单时间   
        /// </summary>   
        public DateTime OrderTime { get; set; }
        /// <summary>   
        /// 订单编号   
        /// </summary>   
        public Guid OrderCode { get; set; }
    }

    /// <summary>
    /// 扩展IEnumerable
    /// </summary>
    public static class OrderCollectionExtent
    {
        public static bool WhereOrderListAdd<T>(this IEnumerable<T> IEnumerable) where T : Order
        {
            foreach (var item in IEnumerable)
            {
                if (item.OrderCode != null && !String.IsNullOrEmpty(item.OrderName) && item.OrderTime != null)
                {
                    continue;
                }
                return false;
            }
            return true;
        }
    }

    public static class 延迟加载
    {
        public static void test()
        { 
            List<Order> list=new List<Order> (){
                new Order(){OrderName="aaa",OrderTime=DateTime.Now,OrderCode=Guid.NewGuid()},
                new Order(){OrderName="bbb",OrderTime=DateTime.Now,OrderCode=Guid.NewGuid()},
            };

            IQueryable<Order> queryorderlist = (from o in list where o.OrderName == "aaa" select o).AsQueryable<Order>();
            //以上不会立刻执行，如果要想马上得到数据，可以用一下代码执行。
            queryorderlist.Provider.Execute<Order>(queryorderlist.Expression);
        }

        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            if (source == null)
            {
                return null;
            }
            if (selector == null)
            {
                return null;
            }
            return source.Provider.CreateQuery<TResult>(Expression.Call(null, ((MethodInfo)MethodBase.GetCurrentMethod()).MakeGenericMethod(new Type[] { typeof(TSource), typeof(TResult) }), new Expression[] { source.Expression, Expression.Quote(selector) }));
        }
    }


    public class IQueryable接口与IEnumberable接口的区别
    {
        //IQueryable<T> 是将Skip ,take 这些方法表达式翻译成T-SQL语句之后再向SQL服务器发送命令，它并不是把所有数据都加载到内存里来才进行条件过滤
        //IEnumerable<T> 泛型类在调用自己的SKip 和 Take 等扩展方法之前数据就已经加载在本地内存里了，

        public void test()
        {
            using (HuNiEntities _HuNiEntities = new HuNiEntities())
            {
                //查询的结果放入IQueryable接口的集合中
                IQueryable<Base_Area> classesIQue = (from c in _HuNiEntities.Base_Area orderby c.ID select c).Skip<Base_Area>(3).Take<Base_Area>(3);

                //注意这个AsEnumerable<T_Class>()在分页查询之前，先将其转换成IEnumerable类型
                IEnumerable<Base_Area> classesIEnu = (from c in _HuNiEntities.Base_Area orderby c.ID select c).AsEnumerable<Base_Area>().Skip<Base_Area>(3).Take<Base_Area>(3);
            }
        }
    }
 

}
