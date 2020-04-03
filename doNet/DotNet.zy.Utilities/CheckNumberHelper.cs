using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities
{
    public static class CheckNumberHelper
    {
        public static bool IsNumeric(this Type t)
        {
            t = Nullable.GetUnderlyingType(t) ?? t;
            return t == typeof(Int16)//short
                || t == typeof(Int32)//int
                || t == typeof(Int64)//long
                || t == typeof(UInt16)//ushort
                || t == typeof(UInt32)//uint
                || t == typeof(UInt64)//ulong
                || t == typeof(Single)//float
                || t == typeof(Double)//double
                || t == typeof(Decimal)//decimal
                || t == typeof(Byte)
                || t == typeof(SByte)//sbyte
                || t == typeof(UIntPtr)
                || t == typeof(IntPtr);
        }

        /// <summary>
        /// IsNumeric函数的优化版
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsNumeric2(this Type t)
        {
            if (t.IsEnum) return false;
            var tc = Type.GetTypeCode(t);
            switch (tc)
            {
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Byte:
                case TypeCode.Decimal:
                case TypeCode.SByte:
                    return true;
                default:
                    return t == typeof(UIntPtr) || t == typeof(IntPtr);
            }
        }


        public static bool IsNumericType(this Type dataType)
        {
            return !dataType.IsClass
                 && !dataType.IsInterface
                && dataType.GetInterfaces().Any(q => q == typeof(IFormattable));
        }

        /// <summary>
        /// 可空类型Nullable<T> 例如double?这种
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static bool IsNumericOrNullableNumericType(this Type o)
        {
            if (!o.Name.StartsWith("Nullable")) return false;
            return o.GetGenericArguments()[0].IsNumericType();
        }


        //以下为另一种方向判断

        /// <summary>
        /// 判断是否为数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为数值类型</returns>
        public static bool IsNumericType2(this Type t)
        {
            var tc = Type.GetTypeCode(t);
            return (t.IsPrimitive && t.IsValueType && !t.IsEnum && tc != TypeCode.Char && tc != TypeCode.Boolean) || tc == TypeCode.Decimal;
        }

        /// <summary>
        /// 判断是否为可空数值类型。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空数值类型</returns>
        public static bool IsNumericOrNullableNumericType2(this Type t)
        {
            return t.IsNumericType() || (t.IsNullableType() && t.GetGenericArguments()[0].IsNumericType());
        }

        /// <summary>
        /// 判断是否为可空类型。
        /// 注意，直接调用可空对象的.GetType()方法返回的会是其泛型值的实际类型，用其进行此判断肯定返回false。
        /// </summary>
        /// <param name="t">要判断的类型</param>
        /// <returns>是否为可空类型</returns>
        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
