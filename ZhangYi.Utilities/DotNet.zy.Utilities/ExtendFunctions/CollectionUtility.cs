using System.Collections.Generic;
using System.Linq;

namespace System
{
    public static class CollectionUtility
    {
        /// <summary>
        /// 验证列表中的元素是否为升序排序，元素类型必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Boolean ValidSortAsc<T>(this IList<T> list) where T : IComparable<T>
        {
            lock (list)
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = list[i].CompareTo(list[i + 1]);
                    if (res > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否按指定属性为升序排序，元素中的指定属性必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Boolean ValidSortAsc<T, C>(this IList<T> list, Func<T, C> func) where C : IComparable<C>
        {
            lock (list)
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = func.Invoke(list[i]).CompareTo(func.Invoke(list[i + 1]));
                    if (res > 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否为升序排序，元素类型必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static Boolean ValidSortDesc<T>(this IList<T> list) where T : IComparable<T>
        {
            lock (list)
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = list[i].CompareTo(list[i + 1]);
                    if (res < 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 验证列表中的元素是否按指定属性为降序排序，元素中的指定属性必须实现IComparable接口
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="C"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Boolean ValidSortDesc<T, C>(this IList<T> list, Func<T, C> func) where C : IComparable<C>
        {
            lock (list)
            {
                for (var i = 0; i < list.Count - 1; i++)
                {
                    Int32 res = func.Invoke(list[i]).CompareTo(func.Invoke(list[i + 1]));
                    if (res < 0)
                        return false;
                }
                return true;
            }
        }

        /// <summary>
        /// 对集合中的所有元素依次执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action"></param>
        public static void ExcuteForeach<T>(this IEnumerable<T> list, Action<T> action)
        {
            if (list == null)
                throw new NullReferenceException();

            lock (list)
            {
                try
                {
                    foreach (var item in list)
                    {
                        action.Invoke(item);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 对集合中的所有元素依次执行操作
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="actions"></param>
        public static void ExcuteForeach<T>(this IEnumerable<T> list, params Action<T>[] actions)
        {
            if (list == null)
                throw new NullReferenceException();

            lock (list)
            {
                try
                {
                    foreach (var item in list)
                    {
                        foreach (var action in actions)
                        {
                            action.Invoke(item);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="current">当前对象</param>
        /// <returns>集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null</returns>
        public static T MoveNext<T>(this IList<T> list, T current) where T : class
        {
            var index = list.IndexOf(current);
            if (index < 0) return null;
            if (index + 1 >= list.Count) return null;
            return list[index + 1];
        }

        /// <summary>
        /// 获取集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="current">当前对象</param>
        /// <returns>集合中基于给定对象的下一个对象，若不存在给定对象或给定对象位于集合尾部则返回null</returns>
        public static T MoveNext<T>(this IEnumerable<T> list, T current) where T : class
        {
            var flage = false;

            foreach (var item in list)
            {
                if (flage)
                {
                    return item;
                }

                if (item.Equals(current))
                    flage = true;
            }
            return null;
        }

        /// <summary>
        /// 把一个键值对集合中的数据批量添加到另一个集合中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="other"></param>
        public static void AddRange<T, V>(this IDictionary<T, V> source, IDictionary<T, V> other)
        {
            foreach (var item in other)
            {
                if (!source.ContainsKey(item.Key))
                    source.Add(item.Key, item.Value);
            }
        }

        public static void AddRange<T, V>(this IDictionary<T, V> source, IList<T> keys, IList<V> values)
        {
            if (keys.Count != values.Count)
                throw new ArgumentException();
            for (var i = 0; i < keys.Count; i++)
            {
                if (!source.ContainsKey(keys[i]))
                    source.Add(keys[i], values[i]);
            }
        }

        /// <summary>
        /// 在键值对集合中根据值尝试查找键
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="source"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static K FindKey<K, V>(this IDictionary<K, V> source, V value)
        {
            var keyvaluepair = source.FirstOrDefault(kv => kv.Value.Equals(value));
            return keyvaluepair.Equals(default(KeyValuePair<K, V>)) ? default(K) : keyvaluepair.Key;
        }
        /// <summary>
        /// 把有序列表中的元素顺序随机重新排列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        public static IList<T> Shuffle<T>(this IList<T> source)
        {
            if (source.Count <= 1)
                return source;

            var array = source.ToArray();
            Array.Sort(array, new ShuffleComparer<T>());

            return array.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class ShuffleComparer<T> : IComparer<T>
        {
            readonly Random ran = new Random();
            public int Compare(T x, T y)
            {
                return ran.Next(1000) > 500 ? 1 : -1;
            }
        }

        /// <summary>
        /// 批量添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        public static void AddRange<T>(this ICollection<T> source, IEnumerable<T> list)
        {
            if (source is Array)
            {
                throw new NotSupportedException("Array size is fixed.");
            }
            var templist = source as List<T>;
            if (templist != null)
            {
                templist.AddRange(list); return;
            }

            list.ExcuteForeach(source.Add);
        }

        /// <summary>
        /// 批量删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="itemsToRemove"></param>
        public static void RemoveRange<T>(this IList<T> source, IEnumerable<T> itemsToRemove)
        {
            if (source is Array)
            {
                throw new NotSupportedException("Array size is fixed.");
            }
            var removingItems = new Dictionary<T, int>();

            foreach (var item in itemsToRemove)
            {
                if (removingItems.ContainsKey(item))
                {
                    removingItems[item]++;
                }
                else
                {
                    removingItems[item] = 1;
                }
            }

            var setIndex = 0;
            var _count = source.Count;
            for (var getIndex = 0; getIndex < _count; getIndex++)
            {
                var current = source[getIndex];
                if (removingItems.ContainsKey(current))
                {
                    removingItems[current]--;
                    if (removingItems[current] == 0)
                    {
                        removingItems.Remove(current);
                    }

                    continue;
                }

                source[setIndex++] = source[getIndex];
            }

            _count = setIndex;

            for (var i = source.Count; i > _count; i--)
            {
                source.RemoveAt(i - 1);
            }
        }

        /// <summary>
        /// 移除列表中第一个满足条件的元素，并返回它，如果列表中不存在满足条件的元素，则抛出异常
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="conf"></param>
        /// <returns></returns>
        public static T RemoveFirst<T>(this ICollection<T> source, Func<T, Boolean> conf)
        {
            if (source is Array)
            {
                throw new NotSupportedException("Array size is fixed.");
            }
            var item = source.First(conf);
            source.Remove(item); return item;
        }

        /// <summary>
        /// 批量删除元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        public static void RemoveAll<T>(this IList<T> source, Func<T, Boolean> conf)
        {
            if (source is Array)
            {
                throw new NotSupportedException("Array size is fixed.");
            }

            var templist = source as List<T>;
            if (templist != null)
            {
                templist.RemoveAll(new Predicate<T>(conf)); return;
            }

            var list = source.Where(conf).ToList();

            source.RemoveRange(list);
        }

        /// <summary>
        /// 将集合中的指定属性相加，返回计算结果，指定属性必须支持加号操作符并且返回同一类型，否则将抛出异常。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns>计算结果</returns>
        /// <exception cref="MissingMethodException">传入参数类型不支持加号操作符</exception>
        /// <exception cref="InvalidCastException">传入参数类型相加后不返回同一类型</exception>
        /// <exception cref="OverflowException">计算结果超过范围</exception>
        public static TResult Sum<T, TResult>(this IEnumerable<T> source, Func<T, TResult> func)
        {
            dynamic result = default(TResult);
            foreach (var item in source)
            {
                if (result.Equals(default(TResult))) { result = func.Invoke(item); continue; }
                result = func.Invoke(item) + result;
            }
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Taction"></typeparam>
        /// <param name="list"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static IEnumerable<T> Distinct<T, Taction>(this IEnumerable<T> list, Func<T, Taction> func) where Taction : IEquatable<Taction>
        {
            if (default(T) == null)
            {
                var result = new List<Taction>();
                foreach (var item in list)
                {
                    var current = func.Invoke(item);
                    if (!result.Contains(current))
                    {
                        result.Add(current);
                        yield return item;
                    }
                }
            }
            else
            {
                var result = new List<Taction>();
                foreach (var item in list)
                {
                    var current = func.Invoke(item);
                    if (result.IndexOf(current) < 0)
                    {
                        result.Add(current);
                        yield return item;
                    }
                }
            }
        }

        private static void Swap<T>(this IList<T> source, Int32 i1, Int32 i2)
        {
            T temp = source[i1];
            source[i1] = source[i2];
            source[i2] = temp;
        }
    }
}
