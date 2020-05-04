using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DoNet基础
{
    public static class DoNet开发技巧类
    {
        /// <summary>
        /// 移除字符
        /// </summary>
        /// <param name="value"></param>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static string RemoveCharot(this string value, IEnumerable<char> chars)
        {
            var sb = new StringBuilder();
            var array = chars.Distinct().ToArray();
            Array.Sort(array);
            foreach (var c in value)
            {
                if (Array.BinarySearch(array, c) < 0)
                    sb.Append(c);
            }
            return sb.ToString();
        }

    }
}
