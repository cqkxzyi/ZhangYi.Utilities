/*正则表达式验证
 * 
 * 程序负责：zhangyi
 * 电话：13594663608
 * Email：kxyi-lover@163.com
 * QQ:284124391
 * 声明：仅限于您自己使用，不得进行商业传播，违者必究！
 * */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text.RegularExpressions;

namespace DotNet.zy.Utilities
{
    /// <summary>
    ///正则表达式验证
    /// </summary>
    public class RegexCheck
    {
        #region 验证输入字符串是否与模式字符串匹配
        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="pattern">模式字符串</param>        
        public static bool IsMatch(string input, string pattern)
        {
            return IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 验证输入字符串是否与模式字符串匹配，匹配返回true
        /// </summary>
        /// <param name="input">输入的字符串</param>
        /// <param name="pattern">模式字符串</param>
        /// <param name="options">筛选条件</param>
        public static bool IsMatch(string input, string pattern, RegexOptions options)
        {
            return Regex.IsMatch(input, pattern, options);
        }
        #endregion

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

        #region 判断是否包含特殊字符****

        #region 判断是否包含特殊字符
        /// <summary>
        /// 判断是否包含特殊字符
        /// </summary>
        public static bool IsHaveTeShuZiFu(string str)
        {
            string strReg = @"[',\?\\\+\|]+";
            bool selectUserName = Regex.IsMatch(str, strReg);
            return selectUserName;
        }
        #endregion

        #region 判断是否包含特殊字符(键盘上所有字符都验证)
        /// <summary>
        /// 判断是否包含特殊字符(键盘上所有字符都验证)
        /// </summary>
        public static bool IsHaveTeShuZiFuAll(string str)
        {
            string strReg = @"[~`·!！@#\$￥%\^…&\(\)（）_\|\\—\+\-\*/=\[\]【】\{\}｛｝;；:：'‘’“”""\,，\<\>《》\.。、\?？]+";
            bool selectUserName = Regex.IsMatch(str, strReg);
            return selectUserName;
        }
        #endregion

        #endregion~~~~~~~~~~~~~~~~~~~~~~~

        #region 验证是否为数字
        /// <summary>
        /// 验证是否为数字
        /// </summary>
        /// <param name="number">要验证的数字</param>        
        public static bool IsNumber(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*[.]?[0-9]*$";

            //验证
            return RegexCheck.IsMatch(number, pattern);
        }
        #endregion

        #region 各种数字检查****
        /// <summary>
        /// 判断是否int数字型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ChkNumer(string str)
        {
            bool temp = false;
            try
            {
                int t1 = Convert.ToInt32(str);
                temp = true;
            }
            catch
            {
                temp = false;
            }
            return temp;
        }  
        /// <summary>
        /// 验证是否为整数 如果为空，认为验证不合格 返回false
        /// </summary>
        /// <param name="number">要验证的整数</param>        
        public static bool IsInt(string number)
        {
            //如果为空，认为验证不合格
            if (IsNullOrEmpty(number))
            {
                return false;
            }

            //清除要验证字符串中的空格
            number = number.Trim();

            //模式字符串
            string pattern = @"^[0-9]+[0-9]*$";

            //验证
            return RegexCheck.IsMatch(number, pattern);
        }  

        /// <summary>
        /// 判断是否正整数
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ChkZhengNumer(string str)
        {
            bool temp = false;
            try
            {
                int t1 = Convert.ToInt32(str);
                if (t1 > 0)
                {
                    temp = true;
                }
            }
            catch
            {
                temp = false;
            }
            return temp;
        }

        /// <summary>
        /// 匹配金额 整数、小数都为正确。
        /// </summary>
        /// <param name="moneyStr"></param>
        /// <returns></returns>
        public static bool IsMoney(string moneyStr)
        {
            Regex regMoney = new Regex(@"^(\d+(?:[.]\d{1,2})|[1-9]\d*)$");
            return regMoney.IsMatch(moneyStr);
        }

        /// <summary>
        /// 是否数字字符串 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsNumberSign(string inputData)
        {
            Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
            Match m = RegNumberSign.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimal(string inputData)
        {
            Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
            Match m = RegDecimal.Match(inputData);
            return m.Success;
        }
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsDecimalSign(string inputData)
        {
            Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
            Match m = RegDecimalSign.Match(inputData);
            return m.Success;
        }

        /// <summary>
        /// 是否是正浮点数
        /// </summary>
        /// <param name="strFloatNum">待测试字符串</param>
        /// <param name="bInculudeZero">true:含0；false:不含0</param>
        /// <returns></returns>
        public static bool IsPositiveDecimal(string strFloatNum, bool bInculudeZero)
        {
            if (strFloatNum == null)
                return false;
            if (bInculudeZero) //0也认为是正浮点数
                return Regex.IsMatch(strFloatNum.Trim(), @"^\d+(\.\d+)?$");
            else
                return Regex.IsMatch(strFloatNum.Trim(), @"^(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*))$");
        }

        /// <summary>
        /// 是否是负浮点数
        /// </summary>
        /// <param name="strFloatNum">待测试字符串</param>
        /// <param name="bIncludeZero">true:含0；false:不含0</param>
        /// <returns>如果是</returns>
        public static bool IsNegativeDecimal(string strFloatNum, bool bIncludeZero)
        {
            if (strFloatNum == null)
                return false;
            if (bIncludeZero) //0也认为是负浮点数
                return Regex.IsMatch(strFloatNum.Trim(), @"^((-\d+(\.\d+)?)|(0+(\.0+)?))$");
            else
                return Regex.IsMatch(strFloatNum.Trim(), @"^(-(([0-9]+\.[0-9]*[1-9][0-9]*)|([0-9]*[1-9][0-9]*\.[0-9]+)|([0-9]*[1-9][0-9]*)))$");
        }

        #endregion

        #region 判断是否日期型
        /// <summary>
        /// 判断日期格式 如:2008-1-20 或2008-01-20 而且包含了对不同年份2月的天数，闰年的控制等等：
        /// </summary>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static bool isDataTime(string datetime)
        {
            return Regex.IsMatch(datetime, @"^((((1[6-9]|[2-9]\d)\d{2})-(0?[13578]|1[02])-(0?[1-9]|[12]\d|3[01]))|(((1[6-9]|[2-9]\d)\d{2})-(0?[13456789]|1[012])-(0?[1-9]|[12]\d|30))|(((1[6-9]|[2-9]\d)\d{2})-0?2-(0?[1-9]|1\d|2[0-8]))|(((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))-0?2-29-))$");
        }

        /// <summary>
        /// 判断是否日期型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ChkDate(string str)
        {
            try
            {
                DateTime t1 = DateTime.Parse(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #region
        /// <summary>
        /// 验证日期是否合法,对不规则的作了简单处理
        /// </summary>
        /// <param name="date">日期</param>
        public static bool IsDate(ref string date)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(date))
            {
                return true;
            }

            //清除要验证字符串中的空格
            date = date.Trim();

            //替换\
            date = date.Replace(@"\", "-");
            //替换/
            date = date.Replace(@"/", "-");

            //如果查找到汉字"今",则认为是当前日期
            if (date.IndexOf("今") != -1)
            {
                date = DateTime.Now.ToString();
            }

            try
            {
                //用转换测试是否为规则的日期字符
                date = Convert.ToDateTime(date).ToString("d");
                return true;
            }
            catch
            {
                //如果日期字符串中存在非数字，则返回false
                if (!IsInt(date))
                {
                    return false;
                }

                #region 对纯数字进行解析
                //对8位纯数字进行解析
                if (date.Length == 8)
                {
                    //获取年月日
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);
                    string day = date.Substring(6, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12 || Convert.ToInt32(day) > 31)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month + "-" + day).ToString("d");
                    return true;
                }

                //对6位纯数字进行解析
                if (date.Length == 6)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 2);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(month) > 12)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year + "-" + month).ToString("d");
                    return true;
                }

                //对5位纯数字进行解析
                if (date.Length == 5)
                {
                    //获取年月
                    string year = date.Substring(0, 4);
                    string month = date.Substring(4, 1);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = year + "-" + month;
                    return true;
                }

                //对4位纯数字进行解析
                if (date.Length == 4)
                {
                    //获取年
                    string year = date.Substring(0, 4);

                    //验证合法性
                    if (Convert.ToInt32(year) < 1900 || Convert.ToInt32(year) > 2100)
                    {
                        return false;
                    }

                    //拼接日期
                    date = Convert.ToDateTime(year).ToString("d");
                    return true;
                }
                #endregion

                return false;
            }
        }
        #endregion

        #endregion

        #region 检测是否有中文字符
        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData">传入的中文</param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 判断邮件地址
        /// <summary>
        /// 是否是浮点数 可带正负号
        /// </summary>
        /// <param name="inputData">输入字符串</param>
        /// <returns></returns>
        public static bool IsEmail(string inputData)
        {
            Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
            Match m = RegEmail.Match(inputData);
            return m.Success;
        }
        #endregion

        #region 判断邮箱格式
        public static bool isMail(string strEMail)
        {
            return Regex.IsMatch(strEMail, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        #endregion

        #region 判断网络地址
        /// <summary>
        /// 验证网址检测 
        /// </summary>
        /// <param name="strUrl"></param>
        /// <returns></returns>
        public static bool isUrl(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$");
        }
        #endregion

        #region  是否电话号码
        /// <summary>
        /// 是否电话号码
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsPhone(string inputData)
        {
            Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
            Match m = RegPhone.Match(inputData);
            return m.Success;
        }
        #endregion

        #region  固定电话号码格式

        public static bool isTel(string strTel)
        {
            return Regex.IsMatch(strTel, @"^\d{7,15}$");
        }
        #endregion

        #region 手机号码格式
        public static bool isCellphone(string strCellphone)
        {
            return Regex.IsMatch(strCellphone, @"^\d{11}|\d{12}$");
        }
        #endregion

        #region 判断身份证格式
        public static bool isIdCard(string strIdCard)
        {
            return Regex.IsMatch(strIdCard, @"^\d{15}|\d{18}$");
        }
        #endregion

        #region Email 检测
        /// <summary>
        /// Email 检测
        /// </summary>
        /// <param name="strEmail">待检测的Email</param>
        /// <returns>{true,false}</returns>
        public bool IsValidEmail(string strEmail)
        {
            //string pattern = @"^([0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$";
            return Regex.IsMatch(strEmail, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        #endregion

        #region 价格的检查
        /// <summary>
        /// 价格的检查
        /// </summary>
        /// <param name="strPrice"></param>
        /// <returns></returns>
        public bool IsValidPrice(String strPrice)
        {

            return Regex.IsMatch(strPrice, @"\d+(\.){0,1}\d{0,2}");
        }

        public static bool isProce(string strUrl)
        {
            return Regex.IsMatch(strUrl, @"^(0|[1-9]\d*)(\.\d{1,2})?$");
        }
        #endregion

        #region 验证内容长度 传递长度参数
        /// <summary>
        /// 验证内容长度
        /// </summary>
        /// <param name="len">要求长度</param>
        /// <param name="strContent">需要验证的字符串</param>
        /// <returns></returns>
        public static bool isContent(int len, string strContent)
        {
            return Regex.IsMatch(strContent, @"^(\s|\S){" + len + ",5000}$");
        }
        #endregion

        #region 验证IP地址是否合法
        /// <summary>
        /// 验证IP地址是否合法
        /// </summary>
        /// <param name="ip">要验证的IP地址</param>        
        public static bool IsIP(string ip)
        {
            //如果为空，认为验证合格
            if (IsNullOrEmpty(ip))
            {
                return true;
            }

            //清除要验证字符串中的空格
            ip = ip.Trim();

            //模式字符串
            string pattern = @"^((2[0-4]\d|25[0-5]|[01]?\d\d?)\.){3}(2[0-4]\d|25[0-5]|[01]?\d\d?)$";

            //验证
            return RegexCheck.IsMatch(ip, pattern);
        }
        #endregion

    }
}
