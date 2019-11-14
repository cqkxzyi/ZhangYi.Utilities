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
   /// �ַ�����֤������
   /// </summary>
    public partial class StringValidate
   {
       public static bool isError = IsEnrollAA.IsEnroll();

      #region �ж϶����Ƿ�Ϊ��
       /// <summary>
       /// �ж϶����Ƿ�Ϊ�գ�Ϊ�շ���true
       /// </summary>
       /// <typeparam name="T">Ҫ��֤�Ķ��������</typeparam>
       /// <param name="data">Ҫ��֤�Ķ���</param>        
       public static bool IsNullOrEmpty<T>(T data)
       {
           //���Ϊnull
           if (data == null)
           {
               return true;
           }

           //���Ϊ""
           if (data.GetType() == typeof(String))
           {
               if (string.IsNullOrEmpty(data.ToString().Trim()))
               {
                   return true;
               }
           }

           //���ΪDBNull
           if (data.GetType() == typeof(DBNull))
           {
               return true;
           }

           //��Ϊ��
           return false;
       }

       /// <summary>
       /// �ж϶����Ƿ�Ϊ�գ�Ϊ�շ���true
       /// </summary>
       /// <param name="data">Ҫ��֤�Ķ���</param>
       public static bool IsNullOrEmpty(object data)
       {
           //���Ϊnull
           if (data == null)
           {
               return true;
           }

           //���Ϊ""
           if (data.GetType() == typeof(String))
           {
               if (string.IsNullOrEmpty(data.ToString().Trim()))
               {
                   return true;
               }
           }

           //���ΪDBNull
           if (data.GetType() == typeof(DBNull))
           {
               return true;
           }

           //��Ϊ��
           return false;
       }
       #endregion

      #region ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
      /// <summary>
      /// ���Request��ѯ�ַ����ļ�ֵ���Ƿ������֣���󳤶�����
      /// </summary>
      /// <param name="req">Request</param>
      /// <param name="inputKey">Request�ļ�ֵ</param>
      /// <param name="maxLen">��󳤶�</param>
      /// <returns>����Request��ѯ�ַ���</returns>
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

      #region �ַ������� �������Ƚ�ȡ��Σ���ַ����滻
      //�ַ������� �������Ƚ�ȡ��Σ���ַ����滻
      public static string InputText(string inputString, int maxLength)
      {
         StringBuilder retVal = new StringBuilder();

         // ����Ƿ�Ϊ��
         if ((inputString != null) && (inputString != String.Empty))
         {
            inputString = inputString.Trim();

            //��鳤��
            if (inputString.Length > maxLength)
               inputString = inputString.Substring(0, maxLength);

            //�滻Σ���ַ�
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
            retVal.Replace("'", " ");// �滻������
         }
         return retVal.ToString();
      }
      #endregion

      #region �������Ĳ����ǲ���ĳЩ����õ������ַ����������Ŀǰ������������İ�ȫ���
      /// <summary>
      /// �������Ĳ����ǲ���ĳЩ����õ������ַ����������Ŀǰ������������İ�ȫ���
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

      #region ���һ���ַ����Ƿ��Ǵ����ֹ��ɵģ�һ�����ڲ�ѯ�ַ�����������Ч����֤��
      /// <summary>
      /// ���һ���ַ����Ƿ��Ǵ����ֹ��ɵģ�һ�����ڲ�ѯ�ַ�����������Ч����֤��(0����)
      /// </summary>
      /// <param name="_value">����֤���ַ�������</param>
      /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
      public static bool IsNumberId(string _value)
      {
          return QuickValidate("^[1-9]*[0-9]*$", _value);
      }
      #endregion

      #region ����ַ����Ƿ��зǷ�SQL�ַ� ����Bool �д�����
      /// <summary>
      ///  ����ַ����Ƿ��зǷ�SQL�ַ� ����Bool �д�����
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

      #region �Ƴ�SQL�Ƿ��ַ�  public static string ReplaceSQLFilter(string str)
      /// <summary>
      /// �Ƴ�SQL�Ƿ��ַ� �������ַ���
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

      #region ���˵����ϸ���ַ�  public static string Filtrate(string strSource)
      /// <summary>
      /// ���˵����ϸ���ַ�
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

      #region ������֤һ���ַ����Ƿ����ָ����������ʽ��
      /// <summary>
      /// ������֤һ���ַ����Ƿ����ָ����������ʽ��
      /// </summary>
      /// <param name="_express">������ʽ�����ݡ�</param>
      /// <param name="_value">����֤���ַ�����</param>
      /// <returns>�Ƿ�Ϸ���boolֵ��</returns>
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
