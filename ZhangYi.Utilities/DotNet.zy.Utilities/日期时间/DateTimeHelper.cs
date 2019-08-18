using System;
using System.Collections.Generic;
using System.Globalization;

namespace DotNet.zy.Utilities
{
    public class DateFormat
    {
        #region 得到随机日期
        /// <summary>
        /// 得到随机日期
        /// </summary>
        /// <param name="time1">起始日期</param>
        /// <param name="time2">结束日期</param>
        /// <returns>间隔日期之间的 随机日期</returns>
        public static DateTime GetRandomTime(DateTime time1, DateTime time2)
        {
            Random random = new Random();
            DateTime minTime = new DateTime();
            DateTime maxTime = new DateTime();

            System.TimeSpan ts = new System.TimeSpan(time1.Ticks - time2.Ticks);

            // 获取两个时间相隔的秒数
            double dTotalSecontds = ts.TotalSeconds;
            int iTotalSecontds = 0;

            if (dTotalSecontds > System.Int32.MaxValue)
            {
                iTotalSecontds = System.Int32.MaxValue;
            }
            else if (dTotalSecontds < System.Int32.MinValue)
            {
                iTotalSecontds = System.Int32.MinValue;
            }
            else
            {
                iTotalSecontds = (int)dTotalSecontds;
            }


            if (iTotalSecontds > 0)
            {
                minTime = time2;
                maxTime = time1;
            }
            else if (iTotalSecontds < 0)
            {
                minTime = time1;
                maxTime = time2;
            }
            else
            {
                return time1;
            }

            int maxValue = iTotalSecontds;

            if (iTotalSecontds <= System.Int32.MinValue)
                maxValue = System.Int32.MinValue + 1;

            int i = random.Next(System.Math.Abs(maxValue));

            return minTime.AddSeconds(i);
        }
        #endregion

        #region 输出为短日期
        /// <summary>
        /// 输出为短日期
        /// </summary>
        /// <param name="dateStr"></param>
        /// <returns></returns>
        public static string GetShortDate(string dateStr)
        {
            if (dateStr != string.Empty)
                return Convert.ToDateTime(dateStr).ToShortDateString();
            else
                return String.Empty;
        }
        #endregion

        #region  格式化日期
        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="DateStr">需要转换的日期</param>
        /// <param name="DateModel">要转换成的格式[1:yyyy-mm-dd]</param>
        /// <returns></returns>
        public static string DateTransform(DateTime DateStr, string DateModel)
        {
            string Restr = "";
            if (DateStr.ToString() != string.Empty)
                switch (DateModel)
                {
                    case "yyyy/MM/dd":
                        Restr = DateStr.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM/dd/yyyy":
                        Restr = DateStr.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy-MM-dd":
                        Restr = DateStr.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy-M-d":
                        Restr = DateStr.ToString("yyyy-M-d", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM-dd-yyyy":
                        Restr = DateStr.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy.MM.dd":
                        Restr = DateStr.ToString("yyyy.MM.dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM.dd.yyyy":
                        Restr = DateStr.ToString("MM.dd.yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy年MM月dd日":
                        Restr = DateStr.ToString("yyyy年MM月dd日", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM月dd日yyyy年":
                        Restr = DateStr.ToString("MM月dd日yyyy年", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "all":
                        Restr = DateStr.ToString();
                        break;
                    default:
                        Restr = DateStr.ToShortDateString();
                        break;
                }
            return Restr;
        }
        /// <summary>
        /// 日期转换
        /// </summary>
        /// <param name="DateStr">需要转换的日期</param>
        /// <param name="DateModel">要转换成的格式[1:yyyy-mm-dd]</param>
        /// <returns></returns>
        public static string DateTransform(string StrDate, string DateModel)
        {
            string Restr = "";
            DateTime DateStr = DateTime.Parse(StrDate);
            if (DateStr.ToString() != string.Empty)
                switch (DateModel)
                {
                    case "yyyy/MM/dd":
                        Restr = DateStr.ToString("yyyy/MM/dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM/dd/yyyy":
                        Restr = DateStr.ToString("MM/dd/yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy-MM-dd":
                        Restr = DateStr.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy-M-d":
                        Restr = DateStr.ToString("yyyy-M-d", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM-dd-yyyy":
                        Restr = DateStr.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy.MM.dd":
                        Restr = DateStr.ToString("yyyy.MM.dd", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM.dd.yyyy":
                        Restr = DateStr.ToString("MM.dd.yyyy", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "yyyy年MM月dd日":
                        Restr = DateStr.ToString("yyyy年MM月dd日", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "MM月dd日yyyy年":
                        Restr = DateStr.ToString("MM月dd日yyyy年", DateTimeFormatInfo.InvariantInfo);
                        break;
                    case "all":
                        Restr = DateStr.ToString();
                        break;
                    default:
                        Restr = DateStr.ToShortDateString();
                        break;
                }
            return Restr;
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="s1"></param>
        /// <returns></returns>
        public string getShortDate(DateTime s1)
        {
            string ss = string.Format("{0:yyyy-MM-dd HH:mm:ss}", s1);
            return ss;
        }

        #endregion

        #region 是否日期格式
        /// <summary>
        /// 是否日期格式
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 把秒转换成分钟
        /// <summary>
        /// 把秒转换成分钟
        /// </summary>
        /// <returns></returns>
        public static int SecondToMinute(int Second)
        {
            decimal mm = (decimal)((decimal)Second / (decimal)60);
            return Convert.ToInt32(Math.Ceiling(mm));
        }
        #endregion

        #region 返回某年某月最后一天
        /// <summary>
        /// 返回某年某月最后一天
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns>日</returns>
        public static int GetMonthLastDate(int year, int month)
        {
            DateTime lastDay = new DateTime(year, month, new System.Globalization.GregorianCalendar().GetDaysInMonth(year, month));
            int Day = lastDay.Day;
            return Day;
        }
        #endregion

        #region 返回每月的第一天和最后一天
        /// <summary>
        /// 返回每月的第一天和最后一天
        /// </summary>
        /// <param name="month">月份</param>
        /// <param name="firstDay">第一天</param>
        /// <param name="lastDay">最后一天</param>
        public static void ReturnDateFormat(int month, out string firstDay, out string lastDay)
        {
            int year = DateTime.Now.Year + month / 12;
            if (month != 12)
            {
                month = month % 12;
            }
            switch (month)
            {
                case 1:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 2:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    if (DateTime.IsLeapYear(DateTime.Now.Year))
                        lastDay = DateTime.Now.ToString(year + "-0" + month + "-29");
                    else
                        lastDay = DateTime.Now.ToString(year + "-0" + month + "-28");
                    break;
                case 3:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString("yyyy-0" + month + "-31");
                    break;
                case 4:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 5:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 6:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 7:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 8:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-31");
                    break;
                case 9:
                    firstDay = DateTime.Now.ToString(year + "-0" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-0" + month + "-30");
                    break;
                case 10:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
                case 11:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-30");
                    break;
                default:
                    firstDay = DateTime.Now.ToString(year + "-" + month + "-01");
                    lastDay = DateTime.Now.ToString(year + "-" + month + "-31");
                    break;
            }
        }
        #endregion

        #region 返回时间差(中文标示)
        /// <summary>
        /// 返回时间差
        /// </summary>
        /// <param name="DateTime1"></param>
        /// <param name="DateTime2"></param>
        /// <returns></returns>
        public static string DateDiff(DateTime DateTime1, DateTime DateTime2)
        {
            string dateDiff = null;
            try
            {
                TimeSpan ts = DateTime2 - DateTime1;
                if (ts.Days >= 1)
                {
                    dateDiff = DateTime1.Month.ToString() + "月" + DateTime1.Day.ToString() + "日";
                }
                else
                {
                    if (ts.Hours > 1)
                    {
                        dateDiff = ts.Hours.ToString() + "小时前";
                    }
                    else
                    {
                        dateDiff = ts.Minutes.ToString() + "分钟前";
                    }
                }
            }
            catch
            { }
            return dateDiff;
        }
        #endregion

        #region 获得两个日期的间隔
        /// <summary>
        /// 获得两个日期的间隔
        /// </summary>
        /// <param name="DateTime1">日期一。</param>
        /// <param name="DateTime2">日期二。</param>
        /// <returns>日期间隔TimeSpan。</returns>
        public static TimeSpan DateDiff2(DateTime DateTime1, DateTime DateTime2)
        {
            TimeSpan ts1 = new TimeSpan(DateTime1.Ticks);
            TimeSpan ts2 = new TimeSpan(DateTime2.Ticks);
            TimeSpan ts = ts1.Subtract(ts2).Duration();
            return ts;
        }
        #endregion

        #region 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// <summary>
        /// 将时间格式化成 年月日 的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt">年月日分隔符</param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatDate(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("yyyy{0}MM{1}dd", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        #endregion

        #region 将时间格式化成(时分秒)的形式,如果时间为null，返回当前系统时间
        /// <summary>
        /// 将时间格式化成(时分秒)的形式,如果时间为null，返回当前系统时间
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Separator"></param>
        /// <returns></returns>
        public string GetFormatTime(DateTime dt, char Separator)
        {
            if (dt != null && !dt.Equals(DBNull.Value))
            {
                string tem = string.Format("hh{0}mm{1}ss", Separator, Separator);
                return dt.ToString(tem);
            }
            else
            {
                return GetFormatDate(DateTime.Now, Separator);
            }
        }
        #endregion

        #region 获取星期的中文名
        /// <summary>
        /// 获取星期的中文名
        /// </summary>
        public string GetWeek_cn(string inputType, int returnType)
        {
            string temp = "";
            switch (inputType)
            {
                case "Monday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期一";
                            break;
                        case 2:
                            temp = "周一";
                            break;
                        case 3:
                            temp = "一";
                            break;
                    }
                    break;
                case "Tuesday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期二";
                            break;
                        case 2:
                            temp = "周二";
                            break;
                        case 3:
                            temp = "二";
                            break;
                    }
                    break;
                case "Wednesday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期三";
                            break;
                        case 2:
                            temp = "周三";
                            break;
                        case 3:
                            temp = "三";
                            break;
                    }
                    break;
                case "Thursday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期四";
                            break;
                        case 2:
                            temp = "周四";
                            break;
                        case 3:
                            temp = "四";
                            break;
                    }
                    break;
                case "Friday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期五";
                            break;
                        case 2:
                            temp = "周五";
                            break;
                        case 3:
                            temp = "五";
                            break;
                    }
                    break;
                case "Saturday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期六";
                            break;
                        case 2:
                            temp = "周六";
                            break;
                        case 3:
                            temp = "六";
                            break;
                    }
                    break;
                case "Sunday":
                    switch (returnType)
                    {
                        case 1:
                            temp = "星期日";
                            break;
                        case 2:
                            temp = "周日";
                            break;
                        case 3:
                            temp = "日";
                            break;
                    }
                    break;
            }
            return temp;
        }
        #endregion

        #region 根据开始时间和结束时间查找中间所有的日期
        /// <summary>
        /// 根据开始时间和结束时间查找中间所有的日期
        /// </summary>
        /// <param name="dtBeginTime">开始时间</param>
        /// <param name="dtEndTime">结束时间</param>
        /// <returns>日期集合</returns>
        public static List<String> GetDaysByStartEndTime(DateTime dtBeginTime, DateTime dtEndTime)
        {
            List<String> list = new List<string>();
            if (dtBeginTime.Year == dtEndTime.Year)
            {
                if (dtBeginTime.Month == dtEndTime.Month)
                {
                    DateTime dtTemp = ConvertHelper.SafeCastDateTime(dtBeginTime.Year + "-" + dtBeginTime.Month);
                    list = GetMonthDays(dtTemp);
                }
                else
                {
                    int months = dtEndTime.Month - dtBeginTime.Month;
                    for (int i = 1; i <= months; i++)
                    {
                        int Tempmon = dtBeginTime.Month + i;
                        DateTime dtTemp = ConvertHelper.SafeCastDateTime(dtBeginTime.Year + "-" + Tempmon);
                        list = GetMonthDays(dtTemp);
                    }
                }
            }
            else
            {
                int years = dtEndTime.Year - dtBeginTime.Year;
                for (int i = 0; i < years; i++)
                {

                }
            }
            return list;
        }
        #endregion

        #region 获取某月所有的日期
        /// <summary>
        /// 获取某月所有的日期
        /// </summary>
        /// <param name="datetime">某年某月</param>
        /// <returns>日期集合</returns>
        public static List<String> GetMonthDays(DateTime datetime)
        {
            List<String> list = new List<string>();
            //获取指定月份的最后一天是多少
            int nEndMonth = DateTime.DaysInMonth(datetime.Year, datetime.Month);
            //年份前缀
            int sYearModel = datetime.Year;
            //月份前缀
            int sMonthModel = datetime.Month;
            for (int i = 1; i <= nEndMonth; i++)
            {
                string sTemp = string.Empty;
                //如果日子小于10，那么自动在前面加一个0，否则直接加上日子
                if (i < 10)
                {
                    sTemp = sYearModel + "-" + sMonthModel + "-0" + i;
                }
                else
                {
                    sTemp = sYearModel + "-" + sMonthModel + "-" + i;
                }
                list.Add(sTemp);
            }
            return list;
        }
        #endregion


        #region Unix时间戳转为C#格式时间
        /// <summary>
        /// Unix时间戳转为C#格式时间
        /// </summary>
        /// <param name=”timeStamp”></param>
        /// <returns></returns>
        public static DateTime GetTime(string timeStamp)
        {
            DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime); 

            return dtStart.Add(toNow);
        }
        #endregion

        #region C#格式时间转换为Unix时间戳
        /// <summary>
        /// C#格式时间转换为Unix时间戳
        /// </summary>
        /// <param name=”time”></param>
        /// <returns></returns>
        public static int ConvertDateTimeInt(DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));

            return (int)(time - startTime).TotalSeconds;
        }
        #endregion
    }
}
