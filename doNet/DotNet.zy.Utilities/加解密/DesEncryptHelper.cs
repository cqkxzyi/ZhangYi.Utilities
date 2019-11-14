using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using NewLife;
using XCode;


namespace Utils
{
    public class DesEncryptHelper
    {
        private const string KEY_64 = "MyEmbaSy";
        private const string IV_64 = "MyEmbaSy";

        #region Des加密
        /// <summary>
        /// Des加密
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string DesEncrypt(string clearText)
        {
            if (clearText == null || clearText == string.Empty)
            { 
                return string.Empty;
            }
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                byte[] inputByteArray = Encoding.UTF8.GetBytes(clearText);
                des.Key = ASCIIEncoding.ASCII.GetBytes(KEY_64);
                des.IV = ASCIIEncoding.ASCII.GetBytes(IV_64);
                des.Mode = CipherMode.ECB;


                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                using (CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    cs.Close();
                }
                string str = Convert.ToBase64String(ms.ToArray());
                ms.Close();
                return str;
            }

        }
        #endregion

        #region Des解密
        /// <summary>
        /// Des解密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DesDecrypt(string encryptedText)
        {
            
            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                des.Key = ASCIIEncoding.ASCII.GetBytes(KEY_64);
                des.IV = ASCIIEncoding.ASCII.GetBytes(IV_64);
                des.Mode = CipherMode.ECB;

                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {
                    byte[] inputByteArray = Convert.FromBase64String(encryptedText);
                    using (CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(inputByteArray, 0, inputByteArray.Length);

                        cs.FlushFinalBlock();

                        cs.Close();
                    }
                }
                catch
                {
                    return encryptedText;
                }
                string str = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return str;
            }
        }
        #endregion

  
    }
}
