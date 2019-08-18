using System;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Specialized;

namespace DotNet.zy.Utilities
{
   /// <summary>
   /// 字符串验证帮助类
   /// </summary>
    public partial class StringValidate
   {
       public static bool isError = IsEnrollAA.IsEnroll();

      #region 判断对象是否为空
       /// <summary>
       /// 判断对象是否为空，为空返回true
       /// </summary>
       /// <typeparam name="T">要验证的对象的类型</typeparam>
       /// <param name="data">要验证的对象</param>        
       public static bool IsNullOrEmpty<T>(T data)
       {
           //如果为null
           if (data == null)
           {
               return true;
           }

           //如果为""
           if (data.GetType() == typeof(String))
           {
               if (string.IsNullOrEmpty(data.ToString().Trim()))
               {
                   return true;
               }
           }

           //如果为DBNull
           if (data.GetType() == typeof(DBNull))
           {
               return true;
           }

           //不为空
           return false;
       }

       /// <summary>
       /// 判断对象是否为空，为空返回true
       /// </summary>
       /// <param name="data">要验证的对象</param>
       public static bool IsNullOrEmpty(object data)
       {
           //如果为null
           if (data == null)
           {
               return true;
           }

           //如果为""
           if (data.GetType() == typeof(String))
           {
               if (string.IsNullOrEmpty(data.ToString().Trim()))
               {
                   return true;
               }
           }

           //如果为DBNull
           if (data.GetType() == typeof(DBNull))
           {
               return true;
           }

           //不为空
           return false;
       }
       #endregion

      #region 检查Request查询字符串的键值，是否是数字，最大长度限制
      /// <summary>
      /// 检查Request查询字符串的键值，是否是数字，最大长度限制
      /// </summary>
      /// <param name="req">Request</param>
      /// <param name="inputKey">Request的键值</param>
      /// <param name="maxLen">最大长度</param>
      /// <returns>返回Request查询字符串</returns>
      public static string FetchInputDigit(HttpRequest req, string inputKey, int maxLen)
      {
         string retVal = string.Empty;
         if (inputKey != null && inputKey != string.Empty)
         {
            retVal = req.QueryString[inputKey];
            if (null == retVal)
               retVal = req.Form[inputKey];
            if (null != retVal)
            {
                retVal = StringHelper.TextSubLength(retVal, maxLen);
                if (!RegexCheck.IsNumber(retVal))
                  retVal = string.Empty;
            }
         }
         if (retVal == null)
            retVal = string.Empty;
         return retVal;
      }
      #endregion

      #region 字符串清理 包括长度截取、危险字符串替换
      //字符串清理 包括长度截取、危险字符串替换
      public static string InputText(string inputString, int maxLength)
      {
         StringBuilder retVal = new StringBuilder();

         // 检查是否为空
         if ((inputString != null) && (inputString != String.Empty))
         {
            inputString = inputString.Trim();

            //检查长度
            if (inputString.Length > maxLength)
               inputString = inputString.Substring(0, maxLength);

            //替换危险字符
            for (int i = 0; i < inputString.Length; i++)
            {
               switch (inputString[i])
               {
                  case '"':
                     retVal.Append("&quot;");
                     break;
                  case '<':
                     retVal.Append("&lt;");
                     break;
                  case '>':
                     retVal.Append("&gt;");
                     break;
                  default:
                     retVal.Append(inputString[i]);
                     break;
               }
            }
            retVal.Replace("'", " ");// 替换单引号
         }
         return retVal.ToString();
      }
      #endregion

      #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
      /// <summary>
      /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
      /// </summary>
      public static bool isContainSpecChar(string strInput)
      {
         string[] list = new string[] { "123456", "654321" };
         bool result = new bool();
         for (int i = 0; i < list.Length; i++)
         {
            if (strInput == list[i])
            {
               result = true;
               break;
            }
         }
         return result;
      }
      #endregion

      #region 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
      /// <summary>
      /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
      /// </summary>
      /// <param name="_value">需验证的字符串。。</param>
      /// <returns>是否合法的bool值。</returns>
      public static bool IsNumberId(string _value)
      {
          return QuickValidate("^[1-9]*[0-9]*$", _value);
      }
      #endregion

      #region 检测字符串是否有非法SQL字符 返回Bool 有待参照
      /// <summary>
      ///  检测字符串是否有非法SQL字符 返回Bool 有待参照
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static bool CheckSQLWord(string str)
      {
         string pattern = @"'|--|*|%|select|insert|delete|from|count\(|drop|table|update|truncate|asc\(|mid\(|char\(|xp_cmdshell|exec|master|netlocalgroup|administrators|net user|or|and";
         string[] patternlist = pattern.Split('|');
         for (int i = 0; i < patternlist.Length - 1; i++)
         {
            if (int.Parse(str.IndexOf(patternlist[i]).ToString()) > 0)
            {
               return false;
            }
         }
         return true;
      }
      #endregion

      #region 移除SQL非法字符  public static string ReplaceSQLFilter(string str)
      /// <summary>
      /// 移除SQL非法字符 返回新字符串
      /// </summary>
      /// <param name="str"></param>
      /// <returns></returns>
      public static string ReplaceSQLFilter(string str)
      {
         string[] pattern = { "'", "--", "select", "insert", "delete", "from", "count\\(", "drop table", "update", "truncate", "asc\\(", "mid\\(", "char\\(", "xp_cmdshell", "exec   master", "netlocalgroup administrators", "net user", "or", "and" };
         for (int i = 0; i < pattern.Length; i++)
         {
            str = str.Replace(pattern[i].ToString(), "");
         }
         return str;
      }
      #endregion

      #region 过滤掉不合格的字符  public static string Filtrate(string strSource)
      /// <summary>
      /// 过滤掉不合格的字符
      /// </summary>
      /// <param name="strSource"></param>
      /// <returns></returns>
      public static string Filtrate(string strSource)
      {
         strSource = strSource.Replace("'", "");
         strSource = strSource.Replace("\"", "");
         strSource = strSource.Replace("<", "");
         strSource = strSource.Replace(">", "");
         strSource = strSource.Replace("=", "");
         strSource = strSource.Replace("or", "");
         strSource = strSource.Replace("select", "");
         strSource = strSource.Trim();
         return strSource;
      }
      #endregion

      #region 快速验证一个字符串是否符合指定的正则表达式。
      /// <summary>
      /// 快速验证一个字符串是否符合指定的正则表达式。
      /// </summary>
      /// <param name="_express">正则表达式的内容。</param>
      /// <param name="_value">需验证的字符串。</param>
      /// <returns>是否合法的bool值。</returns>
      public static bool QuickValidate(string _express, string _value)
      {
          if (_value == null) return false;
          Regex myRegex = new Regex(_express);
          if (_value.Length == 0)
          {
              return false;
          }
          return myRegex.IsMatch(_value);
      }
      #endregion
   }
}
