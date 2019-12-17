using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DoNet基础.泛型集合
{
    public class 集合字典有以下
    {
        SortedList sList = new SortedList();

        SortedSet<Employee> b;
        SortedList<Employee, string> a;
        SortedDictionary<Employee, string> c;

        //HashTable 的Using是 System.Collections   
        NameValueCollection d = new NameValueCollection();//允许重复 
        Dictionary<int, int> dic = new Dictionary<int, int>();
        KeyValuePair<string, object> keyval = new KeyValuePair<string, object>();
    }

    #region 学习① 轻松加愉快”地实现并使用IComparer接口自定义排序规则================================

    /// <summary>
    /// 自定义实体
    /// </summary>
    public class Employee
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public int Age { get; set; }
    }

    //以Name字段为比较规则的比较器
    public class EmployeeNameComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {

            string a = System.Environment.NewLine;
            return StringComparer.CurrentCulture.Compare(x.Name, y.Name);
        }
    }
    //以Age字段为比较规则的比较器
    class EmployeeAgeComparer : IComparer<Employee>
    {
        public int Compare(Employee x, Employee y)
        {
            return x.Age - y.Age;
        }
    }

    //用法
    public class test1
    {
        public void test()
        {
            var empTelTable = new SortedDictionary<Employee, String>(new EmployeeNameComparer());

            SortedDictionary<Employee, string> list = new SortedDictionary<Employee, string>(new EmployeeNameComparer());
            list.Add(new Employee(), "1");
        }
    }


    //以上方法的弊端就是不同的需求需要写不同的比较器
    //下面写一个通用的比较器
    public class DelegatedComparer<T> : IComparer<T>
    {
        //在构造函数结束后，_compare就没法改变了。因此DelegatedComparer<T>实例的行为也就可以被锁定了。
        private readonly Func<T, T, int> _compare;

        //传入一个委托，表示比较算法。
        public DelegatedComparer(Func<T, T, int> func)
        {
            _compare = func;
        }

        public int Compare(T x, T y)
        {
            return _compare(x, y);
        }
    }

    //以上通用比较器的用法
    public class test2
    {
        public void test()
        {
            var empTelTable = new SortedDictionary<Employee, string>(
                new DelegatedComparer<Employee>((x, y) => x.Id - y.Id)
               );
        }
    }

    #endregion


    #region 学习② Linq的Distinct太不给力了(Distinct去重复)=========================================
    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Test3
    {
        public static void Test()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){ Id="1", Name="n1"},
                new Product(){ Id="1", Name="n2"},
                new Product(){ Id="2", Name="n1"},
                new Product(){ Id="2", Name="n2"},
            };
            var distinctProduct = products.Distinct();//因为Distinct 默认比较的是Product对象的引用，所以返回4条数据
            Console.ReadLine();

            //Cast的用法
            IEnumerable<Product> query = products.Cast<Product>();
            IEnumerable<string> query2 = products.Select(p => p.Id);
        }
    }

    /// <summary>
    /// 自定义比较器
    /// </summary>
    public class ProductIdComparer : IEqualityComparer<Product>
    {
        public bool Equals(Product x, Product y)
        {
            if (x == null)
                return y == null;
            return x.Id == y.Id;
        }

        public int GetHashCode(Product obj)
        {
            if (obj == null)
                return 0;
            return obj.Id.GetHashCode();
        }
    }

    //上面比较器的用法
    public class Test4
    {
        public void test()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){ Id="1", Name="n1"},
                new Product(){ Id="1", Name="n2"},
                new Product(){ Id="2", Name="n1"},
                new Product(){ Id="2", Name="n2"},
            };
            var distinctProduct = products.Distinct(new ProductIdComparer());
        }
    }

    //通用比较器的方法
    //缺点：大量的反射导致性能下降
    public class PropertyComparer<T> : IEqualityComparer<T>
    {
        private PropertyInfo _PropertyInfo;

        //通过propertyName 获取PropertyInfo对象  
        public PropertyComparer(string propertyName)
        {
            _PropertyInfo = typeof(T).GetProperty(propertyName,BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            if (_PropertyInfo == null)
            {
                throw new ArgumentException(string.Format("{0} is not a property of type {1}.",propertyName, typeof(T)));
            }
        }

        public bool Equals(T x, T y)
        {
            object xValue = _PropertyInfo.GetValue(x, null);
            object yValue = _PropertyInfo.GetValue(y, null);

            if (xValue == null)
                return yValue == null;

            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            object propertyValue = _PropertyInfo.GetValue(obj, null);

            if (propertyValue == null)
                return 0;
            else
                return propertyValue.GetHashCode();
        }
    }

    //上面比较器的用法
    public class Test5
    {
        public void test()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){ Id="1", Name="n1"},
                new Product(){ Id="1", Name="n2"},
                new Product(){ Id="2", Name="n1"},
                new Product(){ Id="2", Name="n2"},
            };
            var distinctProduct1 = products.Distinct(new PropertyComparer<Product>("Id"));
            var distinctProduct2 = products.Distinct(new PropertyComparer<Product>("Name"));
        }
    }


    //以上反射效率太差，为了提升性能，可以使用表达式树将反射调用改为委托调用
    public class FastPropertyComparer<T> : IEqualityComparer<T>
    {
        private Func<T, Object> getPropertyValueFunc = null;

        public FastPropertyComparer(string propertyName)
        {
            PropertyInfo _PropertyInfo = typeof(T).GetProperty(propertyName,
            BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public);
            if (_PropertyInfo == null)
            {
                throw new ArgumentException(string.Format("{0} is not a property of type {1}.",
                    propertyName, typeof(T)));
            }

            ParameterExpression expPara = Expression.Parameter(typeof(T), "obj");
            MemberExpression me = Expression.Property(expPara, _PropertyInfo);
            getPropertyValueFunc = Expression.Lambda<Func<T, object>>(me, expPara).Compile();
        }

        public bool Equals(T x, T y)
        {
            object xValue = getPropertyValueFunc(x);
            object yValue = getPropertyValueFunc(y);

            if (xValue == null)
                return yValue == null;

            return xValue.Equals(yValue);
        }

        public int GetHashCode(T obj)
        {
            object propertyValue = getPropertyValueFunc(obj);

            if (propertyValue == null)
                return 0;
            else
                return propertyValue.GetHashCode();
        }
    }

    //上面比较器的用法
    public class Test6
    {
        public void test()
        {
            List<Product> products = new List<Product>()
            {
                new Product(){ Id="1", Name="n1"},
                new Product(){ Id="1", Name="n2"},
                new Product(){ Id="2", Name="n1"},
                new Product(){ Id="2", Name="n2"},
            };
            var distinctProduct = products.Distinct(new FastPropertyComparer<Product>("Id")).ToList();



            //为什么不用GroupBy呢，用这个简单多了
            var result = products.GroupBy(p => p.Id).Select(
                p => new
                {
                    Id = p.Key,
                    Name = p.FirstOrDefault().Name
                });

            result.ToList().ForEach(v => { Console.WriteLine(v.Id + ":" + v.Name); });
        }
    }


    #endregion


    #region 快速创建 IEqualityComparer<T> 的实例

    public static class Equality<T>
    {
        /// <summary>
        /// 自定义实体
        /// </summary>
        public class Employee
        {
            public int Id { get; set; }
            public string Code { get; set; }
            public String Name { get; set; }
            public int Age { get; set; }
            public DateTime Brithday { get; set; }
        }

        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CommonEqualityComparer<V>(keySelector);
        }
        public static IEqualityComparer<T> CreateComparer<V>(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        {
            return new CommonEqualityComparer<V>(keySelector, comparer);
        }

        class CommonEqualityComparer<V> : IEqualityComparer<T>
        {
            private Func<T, V> keySelector;
            private IEqualityComparer<V> comparer;

            public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }

            public CommonEqualityComparer(Func<T, V> keySelector): this(keySelector, EqualityComparer<V>.Default)
            {
            }

            public bool Equals(T x, T y)
            {
                return comparer.Equals(keySelector(x), keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return comparer.GetHashCode(keySelector(obj));
            }
        }

        /// <summary>
        /// 测试
        /// </summary>
        public static void Test1()
        {
            var equalityComparer1 = Equality<Employee>.CreateComparer(p => p.Id);
            var equalityComparer2 = Equality<Employee>.CreateComparer(p => p.Name);
            var equalityComparer4 = Equality<Employee>.CreateComparer(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
        }
    }
    #endregion

    #region  快速创建 IComparer<T> 的实例
    /// <summary>
    /// 比较大小排序用
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class Comparison<T>
    {
        public static IComparer<T> CreateComparer<V>(Func<T, V> keySelector)
        {
            return new CommonComparer<V>(keySelector);
        }
        public static IComparer<T> CreateComparer<V>(Func<T, V> keySelector, IComparer<V> comparer)
        {
            return new CommonComparer<V>(keySelector, comparer);
        }

        class CommonComparer<V> : IComparer<T>
        {
            private Func<T, V> keySelector;
            private IComparer<V> comparer;

            public CommonComparer(Func<T, V> keySelector, IComparer<V> comparer)
            {
                this.keySelector = keySelector;
                this.comparer = comparer;
            }

            public CommonComparer(Func<T, V> keySelector): this(keySelector, Comparer<V>.Default)
            {

            }

            public int Compare(T x, T y)
            {
                return comparer.Compare(keySelector(x), keySelector(y));
            }
        }
    }

    public class 快速创建测试
    {
        public void Test2()
        {
            var comparer1 = Comparison<Employee>.CreateComparer(p => p.Id);
            var comparer2 = Comparison<Employee>.CreateComparer(p => p.Name);
            var comparer4 = Comparison<Employee>.CreateComparer(p => p.Name, StringComparer.CurrentCultureIgnoreCase);
        }
    }


    #endregion
}
