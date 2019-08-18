using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DotNet.zy.Utilities.字符串
{
    /// <summary>
    /// 字符串扩展
    /// </summary>
    public static class StringExample
    {
        public static void StringExample测试(string userInput)
        {
            string sbc = userInput.ToSBC(); //转全角

            string dbc = userInput.ToDBC();//转半角


            string demoString = "this is test string";
            int count = demoString.GetCharCount('i'); //直接调用扩展函数
            Console.WriteLine(count);


            bool b2 = demoString.IsNullOrEmpty();

            List<string> aa = new List<string> { "a", "b" };
            "a".In(aa);

            string today = "今天是：{0:yyyy年MM月dd日 星期ddd}".FormatWith(DateTime.Today);

            bool b = "12345".IsMatch(@"\d+");
            string s = "ldp615".Match("[a-zA-Z]+");
        }

        #region 自定义实体
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
        #endregion


        #region string常用扩展**********************

        #region 转全角(SBC case)
        /// <summary>
        /// 转全角(SBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>全角字符串</returns>
        public static string ToSBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }
        #endregion

        #region 转半角(DBC case)
        /// <summary>
        /// 转半角(DBC case)
        /// </summary>
        /// <param name="input">任意字符串</param>
        /// <returns>半角字符串</returns>
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }
        #endregion

        #region 统计字符在字符串串中出现的次数
        /// <summary>
        /// 统计字符在字符串串中出现的次数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int GetCharCount(this string source, char c)
        {
            return (from item in source where item == c select item).Count();
        }
        #endregion

        #region 判断对象是否在集合中存在
        /// <summary>
        /// 判断对象是否在集合中存在
        /// </summary>
        /// <param name="o"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool In(this object o, IEnumerable b)
        {
            foreach (var item in b)
            {
                if (o == item)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool In<T>(this T t, params T[] c)
        {
            return c.Any(i => i.Equals(t));
        }
        #endregion

        #region IsNullOrEmpty
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }
        #endregion

        #region FormatWith
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }
        #endregion

        #region IsMatch
        public static bool IsMatch(this string s, string pattern)
        {
            if (s == null) return false;
            else return Regex.IsMatch(s, pattern);
        }
        #endregion

        #region Match
        public static string Match(this string s, string pattern)
        {
            if (s == null) return "";
            return Regex.Match(s, pattern).Value;
        }
        #endregion


        #region 首字母大写
        public static string ToCamel(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToLower() + s.Substring(1);
        }
        #endregion

        #region 首字母小写
        public static string ToPascal(this string s)
        {
            if (s.IsNullOrEmpty()) return s;
            return s[0].ToString().ToUpper() + s.Substring(1);
        }
        #endregion

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~

        #region byte 常用扩展********************

        #region 转换为十六进制字符串
        public static string ToHex(this byte b)
        {
            return b.ToString("X2");
        }

        public static string ToHex(this IEnumerable<byte> bytes)
        {
            var sb = new StringBuilder();
            foreach (byte b in bytes)
                sb.Append(b.ToString("X2"));
            return sb.ToString();
        }
        #endregion

        #region 转换为Base64字符串
        public static string ToBase64String(byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }
        #endregion

        #region 转换为基础数据类型
        public static int ToInt(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt32(value, startIndex);
        }
        public static long ToInt64(this byte[] value, int startIndex)
        {
            return BitConverter.ToInt64(value, startIndex);
        }
        #endregion

        #region 转换为指定编码的字符串
        public static string Decode(this byte[] data, Encoding encoding)
        {
            return encoding.GetString(data);
        }
        #endregion

        #region  Hash转换
        //使用指定算法Hash
        public static byte[] Hash(this byte[] data, string hashName)
        {
            HashAlgorithm algorithm;
            if (string.IsNullOrEmpty(hashName)) algorithm = HashAlgorithm.Create();
            else algorithm = HashAlgorithm.Create(hashName);
            return algorithm.ComputeHash(data);
        }
        //使用默认算法Hash
        public static byte[] Hash(this byte[] data)
        {
            return Hash(data, null);
        }
        #endregion

        #region 位运算
        //index从0开始
        //获取取第index是否为1
        public static bool GetBit(this byte b, int index)
        {
            return (b & (1 << index)) > 0;
        }
        //将第index位设为1
        public static byte SetBit(this byte b, int index)
        {
            b |= (byte)(1 << index);
            return b;
        }
        //将第index位设为0
        public static byte ClearBit(this byte b, int index)
        {
            b &= (byte)((1 << 8) - 1 - (1 << index));
            return b;
        }
        //将第index位取反
        public static byte ReverseBit(this byte b, int index)
        {
            b ^= (byte)(1 << index);
            return b;
        }
        #endregion

        #region
        public static void Save(this byte[] data, string path)
        {
            File.WriteAllBytes(path, data);
        }
        #endregion

        #region 转换为内存流
        public static MemoryStream ToMemoryStream(this byte[] data)
        {
            return new MemoryStream(data);
        }
        #endregion

        #endregion~~~~~~~~~~~~~~~~~~~~~~~

        #region Random 扩展********************

        //布尔：NextBool
        public static bool NextBool(this Random random)
        {
            return random.NextDouble() >= 0.5;
            //调用示例：bool b = random.NextBool();
        }

        //枚举: NextEnum
        public static T NextEnum<T>(this Random random) where T : struct
        {
            Type type = typeof(T);
            if (type.IsEnum == false) throw new InvalidOperationException();

            var array = Enum.GetValues(type);
            var index = random.Next(array.GetLowerBound(0), array.GetUpperBound(0) + 1);
            return (T)array.GetValue(index);
        }

        //系统提供的 NextBytes 方法，要事先生成一个 byte 数组，不太方便。扩展一下，输入长度返回数组
        public static byte[] NextBytes(this Random random, int length)
        {
            var data = new byte[length];
            random.NextBytes(data);
            return data;
        }

        //NextUInt16、NextInt16、NextFloat…
        public static UInt16 NextUInt16(this Random random)
        {
            return BitConverter.ToUInt16(random.NextBytes(2), 0);
        }
        public static Int16 NextInt16(this Random random)
        {
            return BitConverter.ToInt16(random.NextBytes(2), 0);
        }
        public static float NextFloat(this Random random)
        {
            return BitConverter.ToSingle(random.NextBytes(4), 0);
        }

        //时间日期：NextDateTime
        public static DateTime NextDateTime(this Random random)
        {

            return NextDateTime(random, DateTime.MinValue, DateTime.MaxValue);
        }
        public static DateTime NextDateTime(this Random random, DateTime minValue, DateTime maxValue)
        {
            var ticks = minValue.Ticks + (long)((maxValue.Ticks - minValue.Ticks) * random.NextDouble());
            return new DateTime(ticks);
            //调用示例：
            //DateTime d = random.NextDateTime(new DateTime(2000, 1, 1), new DateTime(2010, 12, 31));
        }

        #endregion~~~~~~~~~~~~~~~~~~~~~~~

        #region Dictionary<TKey, TValue> 扩展***********************

        /// <summary>
        /// 尝试将键和值添加到字典中：如果不存在，才添加；存在，不添加也不抛导常
        /// </summary>
        public static Dictionary<TKey, TValue> TryAdd<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            if (dict.ContainsKey(key) == false)
            {
                dict.Add(key, value);
            }
            return dict;
        }

        /// <summary>
        /// 将键和值添加或替换到字典中：如果不存在，则添加；存在，则替换
        /// </summary>
        public static Dictionary<TKey, TValue> AddOrReplace<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue value)
        {
            dict[key] = value;
            return dict;
        }

        /// <summary>
        /// 获取与指定的键相关联的值，如果没有则返回输入的默认值
        /// </summary>
        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> dict, TKey key, TValue defaultValue = default(TValue))
        {
            return dict.ContainsKey(key) ? dict[key] : defaultValue;
        }

        /// <summary>
        /// 向字典中批量添加键值对
        /// 为考虑线程安全，建议使用Framewo4,0新增的ConcurrentDictionary<TKey, TValue> 类
        /// </summary>
        /// <param name="replaceExisted">如果已存在，是否替换</param>
        public static Dictionary<TKey, TValue> AddRange<TKey, TValue>(this Dictionary<TKey, TValue> dict, IEnumerable<KeyValuePair<TKey, TValue>> values, bool replaceExisted)
        {
            foreach (var item in values)
            {
                if (dict.ContainsKey(item.Key) == false || replaceExisted)
                    dict[item.Key] = item.Value;
            }
            return dict;
        }

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region WhereIf 扩展*******************************

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Expression<Func<T, int, bool>> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<T, int, bool> predicate, bool condition)
        {
            return condition ? source.Where(predicate) : source;
        }

        //上面的测试
        public static IQueryable<Employee> Query(IQueryable<Employee> source, string name, string code, string address)
        {
            return source
                .WhereIf(p => p.Name.Contains(name), string.IsNullOrEmpty(name) == false)
                .WhereIf(p => p.Code.Contains(code), string.IsNullOrEmpty(code) == false)
                .WhereIf(p => p.Code.Contains(address), string.IsNullOrEmpty(address) == false);
        }


        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~``

        #region IsBetween 通用扩展***************

        //从表面看IComparable<T>是排序时使用 IComparer<T>只是比较

        /// <summary>
        /// 泛型版本 IsBetween 扩展
        /// </summary>
        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, bool includeLowerBound = false, bool includeUpperBound = false) where T : IComparable<T>
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);

            //测试方法
            //bool b1 = 3.IsBetween(1, 3, includeUpperBound: true);
        }

        #region 带 IComparer<T> 参数的 IsBetween 扩展

        public static bool IsBetween<T>(this T t, T lowerBound, T upperBound, IComparer<T> comparer, bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (comparer == null) throw new ArgumentNullException("comparer");

            var lowerCompareResult = comparer.Compare(t, lowerBound);
            var upperCompareResult = comparer.Compare(t, upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }

        //比较器
        public class PersonBirthdayComparer : IComparer<Employee>
        {
            public int Compare(Employee x, Employee y)
            {
                return Comparer<int>.Default.Compare(x.Age, y.Age);
            }
        }

        public class IsBetweenTest
        {
            public void Test()
            {
                var p1 = new Employee { Name = "张毅", Code = "001", Age = 10 };
                var p2 = new Employee { Name = "张毅", Code = "001", Age = 20 };
                var p3 = new Employee { Name = "张毅", Code = "001", Age = 30 };
                bool b6 = p2.IsBetween(p1, p3, new PersonBirthdayComparer());
            }
        }

        #endregion

        #region 针对 IComparable<T> 接口的 IsBetween 扩展

        public static bool IsBetween<T>(this IComparable<T> t, T lowerBound, T upperBound, bool includeLowerBound = false, bool includeUpperBound = false)
        {
            if (t == null) throw new ArgumentNullException("t");

            var lowerCompareResult = t.CompareTo(lowerBound);
            var upperCompareResult = t.CompareTo(upperBound);

            return (includeLowerBound && lowerCompareResult == 0) ||
                (includeUpperBound && upperCompareResult == 0) ||
                (lowerCompareResult > 0 && upperCompareResult < 0);
        }

        //public class BigInt : IComparable<int>, IComparable<double>
        //{
        //    public int CompareTo(int other)
        //    {
        //        //...
        //    }
        //    public int CompareTo(double other)
        //    {
        //        //...
        //    }
        //}
        #endregion

        #endregion~~~~~~~~~~~~~~~~~~~~~`~~~~~~~~~~

        #region Distinct 扩展*********

        public class CommonEqualityComparer<T, V> : IEqualityComparer<T>
        {
            /// <summary>
            /// 委托
            /// </summary>
            private Func<T, V> keySelector;

            public CommonEqualityComparer(Func<T, V> keySelector)
            {
                this.keySelector = keySelector;
            }

            public bool Equals(T x, T y)
            {
                return EqualityComparer<V>.Default.Equals(keySelector(x), keySelector(y));
            }

            public int GetHashCode(T obj)
            {
                return EqualityComparer<V>.Default.GetHashCode(keySelector(obj));
            }
        }

        public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        {
            return source.Distinct(new CommonEqualityComparer<T, V>(keySelector));
        }

        public class DistinctTest
        {
            public void Test()
            {
                var data1 = new Employee[] {
                new Employee{ Id = 1, Name = "张毅"},
                new Employee{ Id = 1, Name = "ldp"}
            };
                var ps1 = data1.Distinct(p => p.Id).ToArray();
            }
        }

        //不区分大小写地排除重复的字符串”，也不难实现，只需要把上面的代码改进下就 OK：
        //public class CommonEqualityComparer2<T, V> : IEqualityComparer<T>
        //{
        //    private Func<T, V> keySelector;
        //    private IEqualityComparer<V> comparer;

        //    public CommonEqualityComparer2(Func<T, V> keySelector, IEqualityComparer<V> comparer)
        //    {
        //        this.keySelector = keySelector;
        //        this.comparer = comparer;
        //    }

        //    public CommonEqualityComparer2(Func<T, V> keySelector)
        //        : this(keySelector, EqualityComparer<V>.Default)
        //    { }

        //    public bool Equals(T x, T y)
        //    {
        //        return comparer.Equals(keySelector(x), keySelector(y));
        //    }

        //    public int GetHashCode(T obj)
        //    {
        //        return comparer.GetHashCode(keySelector(obj));
        //    }
        //}

        //public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector)
        //{
        //    return source.Distinct(new CommonEqualityComparer2<T, V>(keySelector));
        //}

        //public static IEnumerable<T> Distinct<T, V>(this IEnumerable<T> source, Func<T, V> keySelector, IEqualityComparer<V> comparer)
        //{
        //    return source.Distinct(new CommonEqualityComparer2<T, V>(keySelector, comparer));
        //}

        //public void Test3()
        //{
        //    var data1 = new Employee[] {
        //        new Employee{ Id = 1, Name = "张毅"},
        //        new Employee{ Id = 1, Name = "ldp"}
        //    };
        //    var ps1 = data1.Distinct(p => p.Id, StringComparer.CurrentCultureIgnoreCase).ToArray();
        //}

        #endregion ~~~~~~~~~~~~~~~~~


        #region Expression 扩展*******************

        //以上表达式可以用如下方法得到
        public static void ExpressionTest()
        {
            Expression<Func<Employee, bool>> exp = (p => p.Name.Contains("ldp") && p.Age > 10);

            var parameter = Expression.Parameter(typeof(Employee), "p");
            var left = Expression.Call(
                Expression.Property(parameter, "Name"),
                typeof(string).GetMethod("Contains"),
                Expression.Constant("ldp"));
            var right = Expression.GreaterThan(
                Expression.Property(Expression.Property(Expression.Property(parameter, "Birthday"), "Value"), "Year"),
                Expression.Constant(10));
            var body = Expression.AndAlso(left, right);
            var lambda = Expression.Lambda<Func<Employee, bool>>(body, parameter);
        }

        //下面是扩展的方法
        public static Expression AndAlso(this Expression left, Expression right)
        {
            return Expression.AndAlso(left, right);
        }
        public static Expression Call(this Expression instance, string methodName, params Expression[] arguments)
        {
            return Expression.Call(instance, instance.Type.GetMethod(methodName), arguments);
        }
        public static Expression Property(this Expression expression, string propertyName)
        {
            return Expression.Property(expression, propertyName);
        }
        public static Expression GreaterThan(this Expression left, Expression right)
        {
            return Expression.GreaterThan(left, right);
        }
        public static Expression<TDelegate> ToLambda<TDelegate>(this Expression body, params  ParameterExpression[] parameters)
        {
            return Expression.Lambda<TDelegate>(body, parameters);
        }

        #endregion~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        #region Aggregate 扩展及其改进**********

        public static void Test1()
        {
            int[] nums = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            int sum1 = nums.Sum();
            int sum2 = nums.Aggregate((i, j) => i + j);
        }
        #endregion

        #region ToString 重构

        public static string ToString1(this object obj, string format)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

            string[] names = properties.Select(p => p.Name).ToArray();
            string pattern = string.Join("|", names);

            MatchEvaluator evaluator = (match =>
            {
                PropertyInfo property = properties.First(p => p.Name == match.Value);
                object propertyValue = property.GetValue(obj, null);
                if (propertyValue != null)
                {
                    return propertyValue.ToString();
                }
                else return "";
            });
            return Regex.Replace(format, pattern, evaluator);

            //测试
            Employee emp = new Employee { Id = 1, Name = "张毅", Brithday = new DateTime(1990, 9, 9) };
            string s0 = emp.ToString1("Name 生日是 Brithday"); //理想输出：张毅 生日是 1990-9-9
            string s1 = emp.ToString1("编号为：Id，姓名：Name"); //理想输出：编号为：1，姓名：张毅

        }

        public static string ToString2(this object obj, string format)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

            MatchEvaluator evaluator = match =>
            {
                string propertyName = match.Groups["Name"].Value;
                PropertyInfo property = properties.FirstOrDefault(p => p.Name == propertyName);
                if (property != null)
                {
                    object propertyValue = property.GetValue(obj, null);
                    if (propertyValue != null) return propertyValue.ToString();
                    else return "";
                }
                else return match.Value;
            };
            return Regex.Replace(format, @"\[(?<Name>[^\]]+)\]", evaluator, RegexOptions.Compiled);

            //测试
            Employee p2 = new Employee { Id = 1, Name = "张毅", Brithday = new DateTime(1990, 9, 9) };
            string s2 = p2.ToString2("People：Id [Id], Name [Name], Brithday [Brithday]");

        }

        public static string ToString3(this object obj, string format)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties(
                BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.Instance);

            MatchEvaluator evaluator = match =>
            {
                string propertyName = match.Groups["Name"].Value;
                string propertyFormat = match.Groups["Format"].Value;

                PropertyInfo propertyInfo = properties.FirstOrDefault(p => p.Name == propertyName);
                if (propertyInfo != null)
                {
                    object propertyValue = propertyInfo.GetValue(obj, null);
                    if (string.IsNullOrEmpty(propertyFormat) == false)
                        return string.Format("{0:" + propertyFormat + "}", propertyValue);
                    else return propertyValue.ToString();
                }
                else return match.Value;
            };
            string pattern = @"\[(?<Name>[^\[\]:]+)(\s*:\s*(?<Format>[^\[\]:]+))?\]";
            return Regex.Replace(format, pattern, evaluator, RegexOptions.Compiled);

            //测试
            Employee p3 = new Employee { Id = 1, Name = "张毅", Brithday = new DateTime(1990, 9, 9) };
            string s3 = p3.ToString2("People：Id [Id: d4], Name [Name], Brithday [Brithday: yyyy-MM-dd]");
        }

        /// <summary>
        /// 多层属性时的扩展
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString4(this object obj, string format)
        {
            MatchEvaluator evaluator = match =>
            {
                string[] propertyNames = match.Groups["Name"].Value.Split('.');
                string propertyFormat = match.Groups["Format"].Value;

                object propertyValue = obj;
                try
                {
                    foreach (string propertyName in propertyNames)
                        propertyValue = propertyValue.GetPropertyValue(propertyName);
                }
                catch
                {
                    return match.Value;
                }

                if (string.IsNullOrEmpty(format) == false)
                    return string.Format("{0:" + propertyFormat + "}", propertyValue);
                else return propertyValue.ToString();
            };
            string pattern = @"\[(?<Name>[^\[\]:]+)(\s*[:]\s*(?<Format>[^\[\]:]+))?\]";
            return Regex.Replace(format, pattern, evaluator, RegexOptions.Compiled);

            //测试
            //Employee p4 = new Employee { Id = 1, Name = "张毅", Brithday = new DateTime(1990, 9, 9) };
            // p4.Son = new People { Id = 2, Name = "鹤小天", Brithday = new DateTime(2015, 9, 9) };
            // p4.Son.Son = new People { Id = 3, Name = "鹤微天", Brithday = new DateTime(2040, 9, 9) };
            // string s4 = p4.ToString4("[Name] 的孙子 [Son.Son.Name] 的生日是：[Son.Son.Brithday: yyyy年MM月dd日]。");
        }

        public static object GetPropertyValue(this object obj, string propertyName)
        {
            Type type = obj.GetType();
            PropertyInfo info = type.GetProperty(propertyName);
            return info.GetValue(obj, null);
        }

        /// <summary>
        /// 如果Employee有Friends属性，这是一个集合属性，我们想获取朋友的个数，并列出朋友的名字
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToString5(this object obj, string format)
        {
            MatchEvaluator evaluator = match =>
            {
                string[] propertyNames = match.Groups["Name"].Value.Split('.');
                string propertyFormat = match.Groups["Format"].Value;

                object propertyValue = obj;

                try
                {
                    foreach (string propertyName in propertyNames)
                        propertyValue = propertyValue.GetPropertyValue(propertyName);
                }
                catch
                {
                    return match.Value;
                }

                if (string.IsNullOrEmpty(propertyFormat) == false)
                {
                    if (propertyFormat.StartsWith("."))
                    {
                        string subPropertyName = propertyFormat.Substring(1);
                        IEnumerable<object> objs = ((IEnumerable)propertyValue).Cast<object>();
                        if (subPropertyName == "Count")
                            return objs.Count().ToString();
                        else
                        {
                            string[] subProperties = objs.Select(
                                o => o.GetPropertyValue(subPropertyName).ToString()).ToArray();
                            return string.Join(", ", subProperties);
                        }
                    }
                    else
                        return string.Format("{0:" + propertyFormat + "}", propertyValue);
                }
                else return propertyValue.ToString();
            };
            string pattern = @"\[(?<Name>[^\[\]:]+)(\s*[:]\s*(?<Format>[^\[\]:]+))?\]";
            return Regex.Replace(format, pattern, evaluator, RegexOptions.Compiled);

            //测试
            //Employee p5 = new Employee { Id = 1, Name = "张毅"};
            //p5.AddFriend(new People { Id = 11, Name = "南霸天" });
            //p5.AddFriend(new People { Id = 12, Name = "日中天" });
            //string s5 = p5.ToString5("[Name] 目前有 [Friends: .Count] 个朋友：[Friends: .Name]。");
        }

        #endregion

        #region Form控件扩展
        public static IEnumerable<T> GetControls<T>(this Control control, Func<T, bool> filter) where T : Control
        {
            foreach (Control c in control.Controls)
            {
                if (c is T)
                {
                    T t = c as T;
                    if (filter != null)
                    {
                        if (filter(t))
                        {
                            yield return t;
                        }
                        else
                        {
                            foreach (T _t in GetControls<T>(c, filter))
                                yield return _t;
                        }
                    }
                    else
                        yield return t;
                }
                else
                {
                    foreach (T _t in GetControls<T>(c, filter))
                        yield return _t;
                }
            }

            //测试

            ////禁用所有Button
            //this.GetControls<Button>(null).ForEach(b => b.Enabled = false);

            ////反选groupBox1中CheckBox
            //this.GetControls<CheckBox>(c => c.Parent == groupBox1).ForEach(c => c.Checked = !c.Checked);
            //    
            ////将label1的前景色设为红色
            //this.GetControls<Label>(l => l.Name == "label1").FirstOrDefault().ForeColor = Color.Red;
            //    
        }

        #endregion

        #region “树”通用遍历器

        public class People
        {
            public string Name { get; set; }
            /// <summary>
            /// 是否男性
            /// </summary>
            public bool IsMale { get; private set; }
            /// <summary>
            /// 子孙
            /// </summary>
            public IEnumerable<People> Children { get; set; }
        }

        public static IEnumerable<T> GetDescendants<T>(this T root, Func<T, IEnumerable<T>> childSelector, Predicate<T> filter)
        {
            foreach (T t in childSelector(root))
            {
                if (filter == null || filter(t))
                {
                    yield return t;
                }
                foreach (T child in GetDescendants((T)t, childSelector, filter))
                {
                    yield return child;
                }
            }


            //调用示例

            People people = new People();
            //获取所有子孙
            var descendants = people.GetDescendants(p => p.Children, null);
            //获取所有男性子孙
            var males = people.GetDescendants(p => p.Children, p => p.IsMale);

            //不包含子孙及其女性：
            var p1 = people.GetDescendants(p => p.IsMale ? p.Children : Enumerable.Empty<People>(), p => p.IsMale);
            //不包含子孙 包含女性
            var p2 = people.GetDescendants(p => p.IsMale ? p.Children : Enumerable.Empty<People>(), null);



            //上面Form遍历的扩展就可以改成这样
            ////获取本窗体所有控件
            //var controls = (this as Control).GetDescendants(c => c.Controls.Cast<Control>(), null);
            ////获取所有选中的CheckBox
            //var checkBoxes = (this as Control).GetDescendants(
            //        c => c.Controls.Cast<Control>(),
            //        c => (c is CheckBox) && (c as CheckBox).Checked
            //    )
            //    .Cast<CheckBox>();
        }

        /// <summary>
        /// “根”与“子孙”类型不相同时，需要这样扩展
        /// </summary>
        public static IEnumerable<T> GetDescendants<TRoot, T>(this TRoot root, Func<TRoot, IEnumerable<T>> rootChildSelector, Func<T, IEnumerable<T>> childSelector, Predicate<T> filter)
        {
            foreach (T t in rootChildSelector(root))
            {
                if (filter == null || filter(t))
                {
                    yield return t;
                }
                foreach (T child in GetDescendants(t, childSelector, filter))
                {
                    yield return child;
                }
            }

            //调用示例

            //获取TreeView中所有以“酒”结尾的树结点 
            TreeView treeView1 = new TreeView();
            //var treeViewNode = treeView1.GetDescendants(
            //    treeView => treeView.Nodes.Cast<TreeNode>(),
            //    treeNode => treeNode.Nodes.Cast<TreeNode>(),
            //    treeNode => treeNode.Text.EndsWith("酒")
            //    );
        }
        #endregion

        #region Type类扩展

        public static bool IsNullableType(this Type type)
        {
            return (((type != null) && type.IsGenericType) &&
                (type.GetGenericTypeDefinition() == typeof(Nullable<>)));
        }

        public static Type GetNonNullableType(this Type type)
        {
            if (IsNullableType(type))
            {
                return type.GetGenericArguments()[0];
            }

            return type;

            //或者是下面这个方法实现，更简洁
            //return Nullable.GetUnderlyingType(type);
        }

        public static bool IsEnumerableType(this Type enumerableType)
        {
            return (FindGenericType(typeof(IEnumerable<>), enumerableType) != null);
        }

        public static Type GetElementType(this Type enumerableType)
        {
            Type type = FindGenericType(typeof(IEnumerable<>), enumerableType);
            if (type != null)
            {
                return type.GetGenericArguments()[0];
            }
            return enumerableType;
        }

        public static bool IsKindOfGeneric(this Type type, Type definition)
        {
            return (FindGenericType(definition, type) != null);
        }

        public static Type FindGenericType(this Type definition, Type type)
        {
            while ((type != null) && (type != typeof(object)))
            {
                if (type.IsGenericType && (type.GetGenericTypeDefinition() == definition))
                {
                    return type;
                }
                if (definition.IsInterface)
                {
                    foreach (Type type2 in type.GetInterfaces())
                    {
                        Type type3 = FindGenericType(definition, type2);
                        if (type3 != null)
                        {
                            return type3;
                        }
                    }
                }
                type = type.BaseType;
            }
            return null;
        }

        #endregion

        #region OrderBy的扩展 未完成

        /// <summary>
        /// 创建表达式
        /// </summary>
        public static void CreateExpression()
        {
            ParameterExpression numParam = Expression.Parameter(typeof(int), "num");
            ConstantExpression five = Expression.Constant(5, typeof(int));
            BinaryExpression numLessThanFive = Expression.LessThan(numParam, five);
            Expression<Func<int, bool>> lambda1 = Expression.Lambda<Func<int, bool>>(numLessThanFive, new ParameterExpression[] { numParam });

        }

        public static void Test()
        {
            Expression<Func<Employee, int>> myexpression = (order => order.Age);

            IQueryable<Employee> query = null;
            //string propertyName = "";
            //bool desc =true;
            //var data = query.Where<a=>a.id>.ToArray();

            var type = typeof(Employee);
            var propertyName = "OrderDate";

            ParameterExpression param = Expression.Parameter(type, type.Name);
            MemberExpression body = Expression.Property(param, propertyName);
            dynamic keySelector = Expression.Lambda(body, param);


            //List<Employee> a=new List<Employee> ();
            //a.OrderBy<myexpression>;

            //var orderedQueryable = Queryable.OrderBy(keySelector, (dynamic)keySelector);

        }


        #endregion

        #region 封装 if/else、swith/case 及 while

        public class People2
        {
            public string Name { get; set; }
            public bool IsHungry { get; set; }
            public bool IsThirsty { get; set; }
            public bool IsTired { get; set; }
            public int WorkCount { get; private set; }

            public void Eat()
            {
                Console.WriteLine("Eat");
                IsHungry = false;
            }
            public void Drink()
            {
                Console.WriteLine("Drink");
                IsThirsty = false;
            }
            public void Rest()
            {
                Console.WriteLine("Rest");
                IsTired = false;
            }
            public void Work()
            {
                Console.WriteLine("Work");
                IsHungry = IsThirsty = IsTired = true;
                WorkCount++;
            }
        }

        /// <summary>
        /// 无返回值
        /// </summary>
        public static T If<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t)) action(t);
            return t;
        }

        /// <summary>
        /// 无返回值,多个执行参数
        /// </summary>
        public static T If<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
            return t;
        }

        public class 测试
        {
            public static void Test()
            {
                //常规代码
                People2 people1 = new People2 { Name = "ldp615", IsHungry = true, IsThirsty = true, IsTired = true };
                if (people1.IsHungry) people1.Eat();
                if (people1.IsThirsty) people1.Drink();
                if (people1.IsTired) people1.Rest();
                people1.Work();
                //使用扩展方法
                People2 people2 = new People2 { Name = "ldp615", IsHungry = true, IsThirsty = true, IsTired = true }
                    .If(p => p.IsHungry, p => p.Eat())
                    .If(p => p.IsThirsty, p => p.Drink())
                    .If(p => p.IsTired, p => p.Rest());
                people2.Work();
            }
        }

        /// <summary>
        /// 有返回值
        /// </summary>
        public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func)
        {
            return predicate(t) ? func(t) : t;
        }

        public static void Test2()
        {
            //扩展方式
            int int0 = -121;
            int int1 = int0.If(i => i < 0, i => -i)
                .If(i => i > 100, i => i - 100)
                .If(i => i % 2 == 1, i => i - 1);
            //常规方式
            int int3 = -121;
            if (int3 < 0) int3 = -int3;
            if (int3 > 100) int3 -= 100;
            if (int3 % 2 == 1) int3--;



            //从邮箱变换成主页
            string email = "ldp615@163.com";
            string page = email.If(s => s.Contains("@"), s => s.Substring(0, s.IndexOf("@")))
                .If(s => !s.StartsWith("www."), s => s = "www." + s)
                .If(s => !s.EndsWith(".com"), s => s += ".com");
        }

        /// <summary>
        /// 扩展switch
        /// </summary>
        public static TOutput Switch<TOutput, TInput>(this TInput input, IEnumerable<TInput> inputSource, IEnumerable<TOutput> outputSource, TOutput defaultOutput)
        {
            IEnumerator<TInput> inputIterator = inputSource.GetEnumerator();
            IEnumerator<TOutput> outputIterator = outputSource.GetEnumerator();

            TOutput result = defaultOutput;
            while (inputIterator.MoveNext())
            {
                if (outputIterator.MoveNext())
                {
                    if (input.Equals(inputIterator.Current))
                    {
                        result = outputIterator.Current;
                        break;
                    }
                }
                else break;
            }
            return result;
        }

        public static void Test5()
        {
            string englishName = "apple";
            string chineseName = englishName.Switch(
                new string[] { "apple", "orange", "banana", "pear" },
                new string[] { "苹果", "桔子", "香蕉", "梨" },
                "未知"
                );
            Console.WriteLine(chineseName);
        }

        public static void While<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            while (predicate(t)) action(t);
        }

        public static void Test6()
        {
            People2 people = new People2 { Name = "Wretch" };
            people.While(
                p => p.WorkCount < 7,
                p => p.Work()
                    );
            people.Rest();
        }

        /// <summary>
        ///  扩展While
        /// </summary>
        public static void While<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            while (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
        }

        public static void Test7()
        {
            People2 people = new People2 { Name = "Wretch" };
            people.While(
                p => p.WorkCount < 7,
                p => p.Work(),
                p => p.Eat(),
                p => p.Drink(),
                p => p.Rest()
                    );
            people.Rest();
        }

        #endregion

    }
}
