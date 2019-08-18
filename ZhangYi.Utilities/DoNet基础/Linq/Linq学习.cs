using System;
using System.IO;
using System.Linq;
using System.Collections;
using DoNet基础.EntityFramework;
using System.Linq.Expressions;
using System.Collections.Generic;
using DoNet基础.EntityFramework.模型;

namespace DoNet基础.Linq
{
    public class helper
    {
        public static void 匿名类型()
        {
            //匿名类型
            var data = new { name = "zhangyi", age = 18 };
            Console.WriteLine(data.name + data.age);
        } 
 
        /// <summary>
        /// 获取匿名类型的属性
        /// </summary>
        /// <param name="varobject"></param>
        public static void PrintObjectProperty(params object[] varobject)   
        {
            foreach (object obj in varobject)
            {
                foreach (System.Reflection.PropertyInfo property in obj.GetType().GetProperties())
                {
                    Console.WriteLine(string.Format("PropertyName:{0},PropertyValue:{1}",property.Name, property.GetValue(obj, null)));
                }
            }
        }
        public void 测试获取匿名类型的属性()
        {
            var Student1 = new { name = "zhangyi", age = 18 };
            PrintObjectProperty(Student1); 
        }

    }

    /// <summary>
    /// 对象初始化
    /// </summary>
    public class Person2
    {
        public string username { get; set; }
        public int age { get; set; }

        public override string ToString()
        {
            return string.Format("username:{0} age:{1}", this.username, this.age);
        }
    }

    /// <summary>
    /// 自动属性
    /// </summary>
    public class Person
    {
        public string username
        {
            get;
            protected set;
        }
        public Person()
        {
            this.username = "zhangyi";
        }
    }

    public class Test
    {
        public void Test1()
        {
            Person per = new Person();//per.username="zhangyi";
            Console.WriteLine(per.username);

            Person2 per2 = new Person2() { username = "zhangyi", age = 18 };
            Console.WriteLine(per2.ToString());

            var per2List = new List<Person2>()
             {
                new Person2{username = "zhangyi", age = 18},
                new Person2{username = "gaoxiao", age = 22}
             }; 

            var selectPer = from p in per2List where p.age >= 10 select p.username;
            //等价于：
            selectPer = per2List.Where(p => p.age >= 10).Select(p => p.username.ToUpper());
            foreach (var item in selectPer)
            {
                Console.WriteLine(item);
            }
        }
    }


    public class Lambda表达式
    {
        public void aaa()
        {
            //方式1
            var list = new[] { "a", "b", "c", "aa" };
            var result = Array.FindAll(list, a => (a.IndexOf("a") > -1));
            foreach (var item in result)
            {
                Console.WriteLine(item);
            }

            //方式2
            List<int> list2 = new List<int> { 0, 1, 2, 6, 7, 8, 9 };
            var inString = list2.FindAll(delegate(int a) { return a > 6; });//委托也可以变成一个方法，这里传入方法名即可
        }

        /// <summary>   
        /// 根据委托过滤数据   
        /// </summary>   
        public static IEnumerable<T> Filter<T>(IEnumerable<T> ObjectList, Func<T, bool> FilterFunc)
        {
            List<T> ResultList = new List<T>();
            foreach (var item in ObjectList)
            {
                if (FilterFunc(item))
                    ResultList.Add(item);
            }
            return ResultList;
        }

        public void 上面方法的调用测试()
        { 
            int[] Number = new int[5] { 1, 2, 3, 4, 5 };
            IEnumerable<int> result1 = Filter<int>(Number, (int item) => { return item > 3; });
            var result2 = Filter(Number, (int item) => { return item > 3; }); 
        }

        /// <summary>   
        /// 根据委托过滤数据，具有延迟加载的特性。   
        /// </summary>   
        public static IEnumerable<T> FilterByYield<T>(IEnumerable<T> ObjectList, Func<T, bool> FilterFunc)
        {
            foreach (var item in ObjectList)
            {
                if (FilterFunc(item))
                    yield return item;
            }
        }
    }

    public class 表达式树
    {
        Func<int> Func = () => 10;
        Expression<Func<int>> expression = () => 10;

    }



    public class Linq学习1
    {
        HuNiEntities _HuNiEntities = new HuNiEntities();
        EmptyModelContainer HuNiEntitiesEmpty = new EmptyModelContainer();

        public void Linq语句语法()
        {
            var 构建匿名类型1 = from c in HuNiEntitiesEmpty.Teacher select new { 名称 = c.Name, 电话 = c.Phone, 出生年份 = c.CreteDate.Year };
            var 构建匿名类型2 = from c in HuNiEntitiesEmpty.Teacher select new { 名称 = c.Name, 综合信息 = new { 电话 = c.Phone, 出生年份 = c.CreteDate.Year } };
            var 构建匿名类型3 = from c in HuNiEntitiesEmpty.Teacher select new { 是否张毅 = (c.Name == "zhangyi" ? "是" : "否") };
            var 构建匿名类型4 = from c in HuNiEntitiesEmpty.Teacher where c.ID > 5 select c;
            var 构建匿名类型5 = from c in HuNiEntitiesEmpty.Teacher orderby c.Name descending, c.ID ascending select c;
            var 分页 = (from c in HuNiEntitiesEmpty.Teacher select c).Skip(10).Take(10);
            var 一般分组 = from c in HuNiEntitiesEmpty.Teacher group c by c.Name into g where g.Count() > 5 orderby g.Count() descending select new { 名称 = g.Key, 数量 = g.Count() };
            var 匿名类型分组 = from c in HuNiEntitiesEmpty.Teacher group c by new { c.Name } into g orderby g.Key.Name select new { 名称 = g.Key.Name };
            var 按条件分组 = from c in HuNiEntitiesEmpty.Teacher group c by new { 条件 = c.ID > 5 } into g select new { ID大于5的 = g.Key.条件 ? "是" : "否", 数量 = g.Count() };
            var 过滤相同项 = (from c in HuNiEntitiesEmpty.Teacher select c.Name).Distinct();
            var 链接并且过滤相同值 = (from c in HuNiEntitiesEmpty.Teacher where c.Name.Contains("z") select c).Union(from c in HuNiEntitiesEmpty.Teacher where c.Name.Contains("b") select c);
            var 链接并且不过滤相同项 = (from c in HuNiEntitiesEmpty.Teacher where c.Name.Contains("a") select c).Concat(from c in HuNiEntitiesEmpty.Teacher where c.Name.StartsWith("b") select c);
            var 取相交项 = (from c in HuNiEntitiesEmpty.Teacher where c.Name.Contains("a") select c).Intersect(from c in HuNiEntitiesEmpty.Teacher where c.Name.StartsWith("b") select c);
            var 排除相交项 = (from c in HuNiEntitiesEmpty.Teacher where c.Name.Contains("a") select c).Except(from c in HuNiEntitiesEmpty.Teacher where c.Name.StartsWith("b") select c);
            var in操作 = from c in HuNiEntitiesEmpty.Teacher where new string[] { "aaa", "bbb", "ccc" }.Contains(c.Name) select c;
            var join操作 = from c in HuNiEntitiesEmpty.Teacher join d in HuNiEntitiesEmpty.Student on c.Name equals d.Name select c;
            var 外连接join = from c in HuNiEntitiesEmpty.Teacher join d in HuNiEntitiesEmpty.Student on c.Name equals d.Name into pro from x in pro.DefaultIfEmpty() select c;
        }
        public void Linq存储过程()
        {

        }
    }





    public class Linq学习2
    {
        public class StuInfo
        {
            /// <summary>
            /// 姓名
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 成绩集合
            /// </summary>
            public List<int> Scroes { get; set; }
        }

        public void LINQ查询表达式的编写技巧()
        {
            //LINQ筛选数据
            IEnumerable<FileInfo> files = from filename in Directory.GetFiles("e:\\")
                                          where File.GetLastWriteTime(filename) < DateTime.Now
                                          && Path.GetExtension(filename).ToUpper() == ".TXT"
                                          select new FileInfo(filename);

            //消除查询结果中的重复项
            IEnumerable<string> enumberableMethodName = (from method in typeof(Enumerable).GetMembers(
                                                              System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public)
                                                         select method.Name).Distinct();
            //排序
            IEnumerable<string> fileNames = from filename in Directory.GetFiles("c:\\")
                                            orderby (new FileInfo(filename).Length) descending, filename ascending
                                            select filename;
            //组合数据中的特定属性为匿名对像
            var files2 = from filename in Directory.GetFiles("c:\\")
                         orderby File.GetLastWriteTime(filename) descending
                         select new { Name = filename, LastWriteTime = File.GetLastWriteTime(filename) };

            //可以嵌套多个from子句以处理多层次的数据
            List<StuInfo> students = new List<StuInfo>
            {
               new StuInfo {Name="张三", Scroes= new List<int> {97, 92, 81, 60}},
               new StuInfo {Name="李四", Scroes= new List<int> {75, 84, 91, 39}}
            };
            var scoreQuer = from student in students
                            from scroes in student.Scroes
                            where scroes > 90
                            select new { student.Name, scroe = student.Scroes.Average() };


            //引入新的范围变量暂存查询结果
            IEnumerable<FileInfo> fileNames2 = from filename in Directory.GetFiles("c:\\")
                                               let files3 = new FileInfo(filename)
                                               orderby files3.Length ascending, filename ascending
                                               select files3;
            //等同于如下方式
            IEnumerable<FileInfo> fileNames4 = from filename in Directory.GetFiles("c:\\")
                                               orderby new FileInfo(filename).Length, filename ascending
                                               select new FileInfo(filename);

        }

        public void 学习LinqToObject()
        {
            //①.什么是LINQ to Objects？
            string[] items = { "csharp", "cpp", "python", "perl", "java" };
            var lens1 = System.Linq.Enumerable.Select(items, n => n.Length);//静态函数
            var lens2 = items.Select(n => n.Length);//扩展函数

            //在.NET2.0之前有很多遗留的集合类，比如ArrayList，Stack，Hashtable等非泛型的集合类, 由于没有实现IEnumerable<T>接口，因此我们不能直接
            //使用LINQ对他们进行查询，而需要通过函数Cast() 或者 OfType()进行转换为序列。示例如下：
            ArrayList list = new ArrayList() { "csharp", "java", "vb" };
            //如下错误
            //var error = from item in list select item; 
            var ok = from item in list.Cast<string>() select item;
            IEnumerable<string> ok2 = list.OfType<string>().Select(n => n);
 
            //②. LINQ的延迟查询

            //那么我们如何让查询不是延迟的呢？方法很简单，在IEnumerable<T>的扩展函数中，比如ToArray, ToList, ToDictionary, or ToLookup等几个非延迟方法可以将查询结果返回。

        }
    }

}
