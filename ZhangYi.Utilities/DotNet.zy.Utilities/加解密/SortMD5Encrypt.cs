using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DotNet.Utilities.加解密
{
    /// <summary>
    /// MD5排序加密
    /// </summary>
    public class SortMD5Encrypt
    {
        /// <summary>
        /// 秘钥
        /// </summary>
        private static string G_Key = AppDomain.CurrentDomain.BaseDirectory + ConfigurationManager.AppSettings["key"];
        

        /// <summary>
        /// 加密参数
        /// </summary>
        /// <param name="sParaTemp"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string BuildRequest(SortedDictionary<string, string> sParaTemp)
        {
            string key = G_Key;

            //获取过滤后的数组
            Dictionary<string, string> dicPara = new Dictionary<string, string>();
            dicPara = FilterPara(sParaTemp);

            //组合参数数组
            string prestr = CreateLinkString(dicPara);
            //拼接支付密钥
            string stringSignTemp = prestr + "&key=" + key;

            //获得加密结果
            string myMd5Str = GetMD5(stringSignTemp);

            //返回转换为大写的加密串
            //return myMd5Str;
            return myMd5Str.ToUpper();
        }


        /// <summary>
        /// 除去数组中的空值和签名参数并以字母a到z的顺序排序
        /// </summary>
        /// <param name="dicArrayPre">过滤前的参数组</param>
        /// <returns>过滤后的参数组</returns>
        public static Dictionary<string, string> FilterPara(SortedDictionary<string, string> dicArrayPre)
        {
            Dictionary<string, string> dicArray = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> temp in dicArrayPre)
            {
                if (temp.Key != "sign" && !string.IsNullOrEmpty(temp.Value))
                {
                    dicArray.Add(temp.Key, temp.Value);
                }
            }

            return dicArray;
        }

        /// <summary>
        /// 组合参数数组
        /// </summary>
        /// <param name="dicArray"></param>
        /// <returns></returns>
        public static string CreateLinkString(Dictionary<string, string> dicArray)
        {
            StringBuilder prestr = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in dicArray)
            {
                prestr.Append(temp.Key + "=" + temp.Value + "&");
            }

            int nLen = prestr.Length;
            prestr.Remove(nLen - 1, 1);

            return prestr.ToString();
        }

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        public static string GetMD5(string pwd)
        {
            MD5 md5Hasher = MD5.Create();

            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(pwd));

            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
