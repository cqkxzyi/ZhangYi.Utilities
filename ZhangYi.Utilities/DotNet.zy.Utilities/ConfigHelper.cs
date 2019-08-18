/** web.config������
 * 
 * �������ţ�IT 
 * ������zhangyi
 * ������ϵ�������С�������
 * Email��kxyi-lover@163.com
 * MSN��10011
 * QQ:284124391
 * ���������������Լ�ʹ�ã����ý�����ҵ������Υ�߱ؾ���
 * */
using System;
using System.Configuration;

namespace DotNet.zy.Utilities
{
    /// <summary>
    /// web.config������
    /// </summary>
    public sealed class ConfigHelper
    {
        #region ���AppSettings���ýڵ�����
        /// <summary>
        /// ���AppSettings���ýڵ�����
        /// </summary>
        /// <param name="AppName">������</param>
        /// <returns></returns>
        public static string GetAppStr(string AppName)
        {
            try
            {
                //string a= ConfigurationManager.ConnectionStrings["ConnDonsonUPM"].ToString();
                return ConfigurationManager.AppSettings[AppName];
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region ��ȡAppSettings�е������ַ�����Ϣ �������ڻ�����
        /// <summary>
        /// ��ȡAppSettings�е������ַ�����Ϣ �������ڻ�����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetConfigString(string key)
        {
            string CacheKey = "AppSettings-" + key;
            object objModel = CacheHelper.GetCache(CacheKey);
            if (objModel == null)
            {
                try
                {
                    objModel = ConfigurationManager.AppSettings[key];
                    if (objModel != null)
                    {
                        CacheHelper.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(180), TimeSpan.Zero);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return objModel.ToString();
        }
        #endregion

        #region �õ�AppSettings�е�����Bool��Ϣ
        /// <summary>
        /// �õ�AppSettings�е�����Bool��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool GetConfigBool(string key)
        {
            bool result = false;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = bool.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }
            return result;
        }
        #endregion

        #region �õ�AppSettings�е�����Decimal��Ϣ
        /// <summary>
        /// �õ�AppSettings�е�����Decimal��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal GetConfigDecimal(string key)
        {
            decimal result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = decimal.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        #endregion

        #region �õ�AppSettings�е�����int��Ϣ
        /// <summary>
        /// �õ�AppSettings�е�����int��Ϣ
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static int GetConfigInt(string key)
        {
            int result = 0;
            string cfgVal = GetConfigString(key);
            if (null != cfgVal && string.Empty != cfgVal)
            {
                try
                {
                    result = int.Parse(cfgVal);
                }
                catch (FormatException)
                {
                    // Ignore format exceptions.
                }
            }

            return result;
        }
        #endregion
    }
}
