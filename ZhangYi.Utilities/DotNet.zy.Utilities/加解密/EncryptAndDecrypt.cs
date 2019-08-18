using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using System.Xml;
using DotNet.zy.Utilities;

/// <summary>
/// 密码保护工具。
/// </summary>
public static class AESEncrypt
{

    #region AesDecrypt 生成预定义的密匙。
    /// <summary>
    /// 生成预定义的密匙。
    /// </summary>
    /// <returns>密匙。</returns>
    public static byte[] GenerateKey()
    {
        var key = new byte[]
            {
                0x53, 0x4F, 0x46, 0x4D, 0x49, 0x54, 0x2D, 0x62, 
                0xE7, 0x6D, 0xF2, 0x79, 0xFD, 0x84, 0x0B, 0x8F, 
                0x16, 0x9B, 0x21, 0xA6, 0x2D, 0xB2, 0x38, 0xBD
            };

        return key;
    }
    #endregion GenerateKey

    #region AesDecrypt  生成预定义的IV值。
    /// <summary>
    /// 生成预定义的IV值。
    /// </summary>
    /// <returns>IV值。</returns>
    public static byte[] GenerateIV()
    {
        var iv = new byte[] 
            { 
                0x5A, 0x4C, 0x2D, 0xB2, 0xFC, 0x49, 0x93, 0xDE, 
                0x2A, 0x75, 0xBF, 0x0C, 0x56, 0xA1, 0xEB, 0x38
            };

        return iv;
    }
    #endregion GenerateIV

    #region AES算法进行加密。

    /// <summary>
    /// 使用AES算法进行加密。
    /// </summary>
    /// <param name="data">明文。</param>
    /// <param name="key">密匙。</param>
    /// <param name="iv">IV值。</param>
    /// <returns>密文。</returns>
    public static string AesEncrypt(string data)
    {
        var mStream = new MemoryStream();
        var cryptoTransform = new AesCryptoServiceProvider().CreateEncryptor(GenerateKey(), GenerateIV());
        var cStream = new CryptoStream(mStream, cryptoTransform, CryptoStreamMode.Write);
        byte[] toEncrypt = Encoding.UTF8.GetBytes(data);
        cStream.Write(toEncrypt, 0, toEncrypt.Length);
        cStream.FlushFinalBlock();
        byte[] ret = mStream.ToArray();
        cStream.Close();
        mStream.Close();
        return Convert.ToBase64String(ret);
    }
    #endregion AesEncrypt

    #region AES算法进行解密。
    /// <summary>
    /// 使用AES算法进行解密。
    /// </summary>
    /// <param name="ciphertext">密文。</param>
    /// <param name="key">密匙。</param>
    /// <param name="iv">IV值。</param>
    /// <returns>明文。</returns>
    public static string AesDecrypt(string ciphertext)
    {
        var data = Convert.FromBase64String(ciphertext);
        var mStream = new MemoryStream();
        var cryptoTransform = new AesCryptoServiceProvider().CreateDecryptor(GenerateKey(), GenerateIV());
        var cStream = new CryptoStream(mStream, cryptoTransform, CryptoStreamMode.Write);
        cStream.Write(data, 0, data.Length);
        cStream.FlushFinalBlock();
        byte[] ret = mStream.ToArray();
        cStream.Close();
        mStream.Close();
        return Encoding.UTF8.GetString(ret);
    }
    #endregion AesDecrypt
}

public static class DESEncrypt
{
    #region ========DES加密========
    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public static string Encrypt(string txt)
    {
        return Encrypt(txt, "a");
    }
    /// <summary> 
    /// 加密数据 
    /// </summary> 
    /// <param name="txt"></param> 
    /// <param name="sKey"></param> 
    /// <returns></returns> 
    public static string Encrypt(string txt, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        byte[] inputByteArray;
        inputByteArray = Encoding.Default.GetBytes(txt.ToString());
        des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        StringBuilder ret = new StringBuilder();
        foreach (byte b in ms.ToArray())
        {
            ret.AppendFormat("{0:X2}", b);
        }
        return ret.ToString();
    }

    #endregion

    #region ========DES解密========
    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="Text"></param>
    /// <returns></returns>
    public static string Decrypt(string Text)
    {
        return Decrypt(Text, "a");
    }
    /// <summary> 
    /// 解密数据 
    /// </summary> 
    /// <param name="Text"></param> 
    /// <param name="sKey"></param> 
    /// <returns></returns> 
    public static string Decrypt(string Text, string sKey)
    {
        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        int len;
        len = Text.Length / 2;
        byte[] inputByteArray = new byte[len];
        int x, i;
        for (x = 0; x < len; x++)
        {
            i = Convert.ToInt32(Text.Substring(x * 2, 2), 16);
            inputByteArray[x] = (byte)i;
        }
        des.Key = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        des.IV = ASCIIEncoding.ASCII.GetBytes(System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(sKey, "md5").Substring(0, 8));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
        cs.Write(inputByteArray, 0, inputByteArray.Length);
        cs.FlushFinalBlock();
        return Encoding.Default.GetString(ms.ToArray());
    }

    #endregion


    #region ========DES加密2========
    /// <summary>
    /// 加密字符串
    /// </summary>
    /// <param name="pToEncrypt">待加密字符串</param>
    /// <returns></returns>
    public static string Encrypt2(string pToEncrypt)
    {
        byte[] desKey = new byte[] { 0x16, 0x29, 0x98, 0x15, 0x07, 0x08, 0x05, 0x03 };
        byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x08, 0x05, 0x03 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        try
        {
            //把字符串放到byte数组中
            //原来使用的UTF8编码，改成Unicode编码了，不行
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            //byte[] inputByteArray=Encoding.Unicode.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量
            //原文使用ASCIIEncoding.ASCII方法的GetBytes方法
            //使得输入密码必须输入英文文本
            des.Key = desKey;		// ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = desIV;			//ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            //Write the byte array into the crypto stream
            //(It will end up in the memory stream)
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get the data back from the memory stream, and into a string
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                //Format as hex
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }
        catch
        {
            return pToEncrypt;
        }
        finally
        {
            des = null;
        }
    }
    #endregion

    #region ========DES解密2========
    /// <summary>
    /// 解密字符串
    /// </summary>
    /// <param name="pToDecrypt">待解密字符串</param>
    /// <returns></returns>
    public static string Decrypt2(string pToDecrypt)
    {
        byte[] desKey = new byte[] { 0x16, 0x29, 0x98, 0x15, 0x07, 0x08, 0x05, 0x03 };
        byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x08, 0x05, 0x03 };

        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        try
        {
            //Put the input string into the byte array
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }

            //建立加密对象的密钥和偏移量，此值重要，不能修改
            des.Key = desKey;			//ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = desIV;				//ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            //Flush the data through the crypto stream into the memory stream
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            //Get the decrypted data back from the memory stream
            //建立StringBuild对象，CreateDecrypt使用的是流对象，必须把解密后的文本变成流对象
            StringBuilder ret = new StringBuilder();

            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        catch
        {
            return pToDecrypt;
        }
        finally
        {
            des = null;
        }
    }

    #endregion
}


/// <summary>
/// MD5加解密
/// </summary>
public static class MD5Encrypt
{

    #region 16位MD5加密
    /// <summary>
    /// 16位MD5加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string MD5Encrypt16(string password)
    {
        var md5 = new MD5CryptoServiceProvider();
        string t2 = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(password)), 4, 8);
        t2 = t2.Replace("-", "");
        return t2.ToUpper();
    }
    #endregion

    #region 32位MD5加密
    /// <summary>
    /// 32位MD5加密
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public static string MD5Encrypt32(string password)
    {
        string cl = password;
        string pwd = "";
        MD5 md5 = MD5.Create(); //实例化一个md5对像
        // 加密后是一个字节类型的数组，这里要注意编码UTF8/Unicode等的选择　
        byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(cl));
        // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
        for (int i = 0; i < s.Length; i++)
        {
            // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
            pwd = pwd + s[i].ToString("X");
        }
        return pwd;
    }
    #endregion



    #region MD5加密算法
    /// <summary>
    /// MD5加密算法
    /// </summary>
    /// <param name="strSource">加密字符串</param>
    /// <returns>返回加密后的暗文</returns>
    public static string MD5Encrypt1(string strSource)
    {
        if (string.IsNullOrEmpty(strSource))
        {
            return string.Empty;
        }
        MD5CryptoServiceProvider objMD5CryptoServiceProvider = new MD5CryptoServiceProvider();
        byte[] objHashCode = Encoding.UTF8.GetBytes(strSource);
        objHashCode = objMD5CryptoServiceProvider.ComputeHash(objHashCode);
        StringBuilder sbEnText = new StringBuilder();

        foreach (byte b in objHashCode)
        {
            sbEnText.AppendFormat("{0:x2}", b);
        }
        objMD5CryptoServiceProvider.Clear();
        return sbEnText.ToString().ToUpper();
    }
    #endregion

    #region MD5加密
    /// <summary>
    /// 128位MD5算法加密字符串
    /// </summary>
    /// <param name="text">要加密的字符串</param>    
    public static string MD5Encrypt2(string text)
    {
        //如果字符串为空，则返回
        if (StringValidate.IsNullOrEmpty<string>(text))
        {
            return "";
        }
        //返回MD5值的字符串表示
        byte[] objHashCode = Encoding.UTF8.GetBytes(text);

        try
        {
            //创建MD5密码服务提供程序
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            //计算传入的字节数组的哈希值
            byte[] result = md5.ComputeHash(objHashCode);

            //释放资源
            md5.Clear();

            //返回MD5值的字符串表示
            return Convert.ToBase64String(result);
        }
        catch
        {
            return "";
        }
    }

    #endregion
}


/// <summary>
/// 编码、解码字符串
/// </summary>
public static class StrEncode
{
    #region 编码
    /// <summary>
    /// 编码
    /// </summary>
    /// <param name="codeType"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string EncodeBase64(string codeType, string code)
    {
        string encode = "";
        byte[] bytes = Encoding.GetEncoding(codeType).GetBytes(code);
        try
        {
            encode = Convert.ToBase64String(bytes);
        }
        catch
        {
            encode = code;
        }
        return encode;
    }
    #endregion

    #region 解码
    /// <summary>
    /// 解码
    /// </summary>
    /// <param name="codeType"></param>
    /// <param name="code"></param>
    /// <returns></returns>
    public static string DecodeBase64(string codeType, string code)
    {
        string decode = "";
        byte[] bytes = Convert.FromBase64String(code);
        try
        {
            decode = Encoding.GetEncoding(codeType).GetString(bytes);
        }
        catch
        {
            decode = code;
        }
        return decode;
    }
    #endregion
}



public class DESTest
{
    //Md5(实际上是DES) 加解密例子。
    // 加密字符串
    public string EncryptString(string sInputString, string sKey)
    {
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        byte[] data = Encoding.UTF8.GetBytes(sInputString);
        DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        ICryptoTransform desencrypt = DES.CreateEncryptor();
        byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
        return BitConverter.ToString(result);
    }
    // 解密字符串
    public string DecryptString(string sInputString, string sKey)
    {
        string[] sInput = sInputString.Split("-".ToCharArray());
        byte[] data = new byte[sInput.Length];
        for (int i = 0; i < sInput.Length; i++)
        {
            data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
        }
        DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
        DES.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
        DES.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
        ICryptoTransform desencrypt = DES.CreateDecryptor();
        byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
        return Encoding.UTF8.GetString(result);
    }

    public void Test()
    {
        DESTest des = new DESTest();
        string key = des.GenerateKey();
        string s0 = "中国软件 - zhangyi";
        string s1 = des.EncryptString(s0, key);
        string s2 = des.DecryptString(s1, key);
        Console.WriteLine("原串: [{0}]", s0);
        Console.WriteLine("加密: [{0}]", s1);
        Console.WriteLine("解密: [{0}]", s2);
    }
    // 创建Key
    public string GenerateKey()
    {
        DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
        return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
    }
}