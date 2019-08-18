using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace DotNet.zy.Utilities
{
    public class ClientIPHelper
    {
        /// <summary> 
        /// 获取客户端IP地址 
        /// </summary> 
        /// <returns></returns> 
        public static string GetIpAddress()
        {
            string result = String.Empty;
            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 
                if (result.IndexOf(".") == -1) //没有“.”肯定是非IPv4格式 
                {
                    result = null;
                }
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                return temparyip[i]; //找到不是内网的地址
                            }
                        }
                    }
                    else if (IsIPAddress(result)) //代理即是IP格式 
                    {
                        return result;
                    }
                    else
                    {
                        result = null; //代理中的内容 非IP，取IP 
                    }
                }
            }
            string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (result == null || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }

        public static long GetIpAddressNum(string ip = "")
        {
            try
            {
                if (string.IsNullOrEmpty(ip))
                {
                    ip = GetIpAddress();
                }

                //string strIp = GetIpAddress();
                //if (!string.IsNullOrEmpty(ip))
                //{
                //    strIp = ip;
                //}
                string strIp = ip;
                string[] ips = strIp.Split('.');
                long[] ipNums = ips.Select(i => Int64.Parse(i.Trim())).ToArray();
                long ipNum = ipNums[0] * 256 * 256 * 256
                                + ipNums[1] * 256 * 256
                                + ipNums[2] * 256
                                + ipNums[3];
                return ipNum;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary> 
        /// 获取客户端域名 
        /// </summary> 
        /// <returns></returns> 
        public string GetURL()
        {
            try
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.UrlReferrer.ToString();
                //foreach (String o in HttpContext.Current.Request.ServerVariables) 
                //{ 
                // result += o + "=" + HttpContext.Current.Request.ServerVariables[o] + "<br/>"; 
                //} 
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary> 
        /// 判断是否IP地址 
        /// </summary> 
        /// <param name="str"></param> 
        /// <returns></returns> 
        public static bool IsIPAddress(string str)
        {
            if (str == null || str == string.Empty || str.Length < 7 || str.Length > 15)
            {
                return false;
            }
            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str);
        }

        /// <summary>
        /// 获取客户端IP地址（无视代理）
        /// </summary>
        /// <returns>若失败则返回回送地址</returns>
        public static string GetHostAddress()
        {
            string userHostAddress = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(userHostAddress))
            {
                userHostAddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }

            //最后判断获取是否成功，并检查IP地址的格式（检查其格式非常重要）
            if (!string.IsNullOrEmpty(userHostAddress) && IsIPAddress(userHostAddress))
            {
                return userHostAddress;
            }
            return "127.0.0.1";
        }

    }
}