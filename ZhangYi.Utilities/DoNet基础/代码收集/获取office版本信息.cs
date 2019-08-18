using System;
using System.Collections.Generic;
using System.Text;
using System.Messaging;
using System.Windows.Forms;

namespace WpsTest
{

    public class OfficeOperator
    {

        /// <summary>
        /// 检测MS-Office是否正确安装
        /// 通过注册表检测
        /// </summary>
        /// <param name="version">获得安装的版本号，如office2000,office2003,office2007</param>
        /// <returns></returns>
        public static bool IsInstall(out string Version)
        {
            bool result = false;
            string officePath = "";
            string officeVersion = "";
            
            Version = "";
            GetOfficePath(out officePath, out officeVersion);

            if (!string.IsNullOrEmpty(officeVersion) && !string.IsNullOrEmpty(officePath))
            {
                result = true;
                Version = officeVersion;
            }

            return result;
        }

        /// <summary>
        /// 获取当前某个版本Office的安装路径
        /// </summary>
        /// <param name="Path">返回当前系统Office安装路径</param>
        /// <param name="Version">返回当前系统Office版本信息</param>
        public static void GetOfficePath(out string Path,out string Version)
        {
            string strPathResult = "";
            string strVersionResult = "";
            string strKeyName = "Path";
            object objResult = null;
            Microsoft.Win32.RegistryValueKind regValueKind;
            Microsoft.Win32.RegistryKey regKey = null;
            Microsoft.Win32.RegistryKey regSubKey = null;

            try
            {
                regKey = Microsoft.Win32.Registry.LocalMachine;

                if (regSubKey == null)
                {//office97
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\8.0\Common\InstallRoot", false);
                    strVersionResult = "office97";
                    strKeyName = "OfficeBin";
                }

                if (regSubKey == null)
                {//Office2000
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\9.0\Common\InstallRoot", false);
                    strVersionResult = "office2000";
                    strKeyName = "Path";
                }

                if (regSubKey == null)
                {//officeXp
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\10.0\Common\InstallRoot", false);
                    strVersionResult = "officeXP";
                    strKeyName = "Path";
                }

                if (regSubKey == null)
                {//Office2003
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\11.0\Common\InstallRoot", false);
                    strVersionResult = "office2003";
                    strKeyName = "Path";
                }

                if (regSubKey == null)
                {//office2007 
                    regSubKey = regKey.OpenSubKey(@"SOFTWARE\Microsoft\Office\12.0\Common\InstallRoot", false);
                    strVersionResult = "office2007";
                    strKeyName = "Path";
                }

                objResult = regSubKey.GetValue(strKeyName);
                regValueKind = regSubKey.GetValueKind(strKeyName);
                if (regValueKind == Microsoft.Win32.RegistryValueKind.String)
                {
                    strPathResult = objResult.ToString();
                }
            }
            catch (System.Security.SecurityException ex)
            {
                throw new System.Security.SecurityException("您没有读取注册表的权限", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("读取注册表出错!", ex);
            }
            finally
            {

                if (regKey != null)
                {
                    regKey.Close();
                    regKey = null;
                }

                if (regSubKey != null)
                {
                    regSubKey.Close();
                    regSubKey = null;
                }
            }

            Path = strPathResult;
            Version = strVersionResult;
        }



        private void btnGetOfficePath_Click(object sender, EventArgs e)
        {
            string officePath = "";
            string officeVersion = "";
            try
            {
                OfficeOperator.GetOfficePath(out officePath, out officeVersion);
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法获取系统Office信息");
            }
            if (!string.IsNullOrEmpty(officePath) && !string.IsNullOrEmpty(officeVersion))
            {
                MessageBox.Show(string.Format("版本:{0}\r\n安装路径:{1}", officeVersion, officePath));
            }
        }

        //是否安装及版本
        private void btnIsInstall_Click(object sender, EventArgs e)
        {
            try
            {
                string version = "";
                if (OfficeOperator.IsInstall(out version))
                {
                    MessageBox.Show(string.Format("当前安装了{0}", version));
                }
                else
                {
                    MessageBox.Show("您当前还没有安装微软Office系列软件");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法获取系统Office信息");
            }
        }
    }
}


       

