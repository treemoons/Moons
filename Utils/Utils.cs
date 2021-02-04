using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;
namespace CommonUtils
{
    static public class Utils
    {

    }


    /// <summary>
    /// encrypt cookie with Base64
    /// </summary>
    public struct LoginCookieBase64
    {
        /// <summary>
        /// [username] The string representation, in base 64, of the contents of inArray
        /// </summary>
        /// <returns></returns>
        static public string GetCookieUserNameBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("username"));
        /// <summary>
        /// [password]The string representation, in base 64, of the contents of inArray
        /// </summary>
        /// <returns></returns>
        static public string GetCookiePasswordBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("password"));
        /// <summary>
        /// [isremembered]The string representation, in base 64, of the contents of inArray
        /// </summary>
        /// <returns></returns>
        static public string GetCookieRememberBase64 => Convert.ToBase64String(Encoding.ASCII.GetBytes("isremembered"));
    }
    /// <summary>
    ///  Decrypt/Encrypt data via RSA 
    /// </summary>
    public class RSAData
    {
        /// <summary> 
        /// RSA加密数据 
        /// </summary> 
        /// <param name="express">要加密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public static string RSAEncryption(string express, string KeyContainerName = null)
        {

            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "default"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] plaindata = System.Text.Encoding.ASCII.GetBytes(express);//将要加密的字符串转换为字节数组
                byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
            }
        }
        /// <summary> 
        /// RSA解密数据 
        /// </summary> 
        /// <param name="ciphertext">要解密数据</param> 
        /// <param name="KeyContainerName">密匙容器的名称</param> 
        /// <returns></returns> 
        public static string RSADecrypt(string ciphertext, string KeyContainerName = null)
        {
            System.Security.Cryptography.CspParameters param = new System.Security.Cryptography.CspParameters();
            param.KeyContainerName = KeyContainerName ?? "default"; //密匙容器的名称，保持加密解密一致才能解密成功
            using (System.Security.Cryptography.RSACryptoServiceProvider rsa = new System.Security.Cryptography.RSACryptoServiceProvider(param))
            {
                byte[] encryptdata = Convert.FromBase64String(ciphertext);
                byte[] decryptdata = rsa.Decrypt(encryptdata, false);
                return System.Text.Encoding.ASCII.GetString(decryptdata);
            }
        }
    }

    /// <summary>
    /// 写下日志，记录操作信息
    /// </summary>
    public static class LogText
    {
        /// <summary>
        /// 存放日志的类型
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 数据库执行类
            /// </summary>
            ImplemnetationTrance = 0,
            /// <summary>
            /// 模型数据类
            /// </summary>
            ModelsLibraryTrance,
            /// <summary>
            /// 主网站类
            /// </summary>
            MainWebTrance
        }
        /// <summary>
        /// logText全局锁
        /// </summary>
        private static object locks = new object();
        /// <summary>
        /// 将文本写入文件(可以自行创建路径)
        /// </summary>
        /// <param name="text">要写入的文本</param>
        /// <param name="relationPath">要写入文件的文件夹路径(定位路径)</param>
        /// <param name="path">相对路径下中间加入"\log\"之后的路径（一般为Log日志的类型）</param>
        public static void WriteLog(string text, string relationPath = "", LogType path = LogType.ImplemnetationTrance)
        {
            if (relationPath == "")
                relationPath = Environment.CurrentDirectory;
            string fullPath = relationPath + @"\log\" + path.ToString();
            lock (locks)
            {
                DirectoryInfo directory = new DirectoryInfo(fullPath);
                if (!directory.Exists)
                {
                    directory.Create();//创建文件夹
                }
                using (FileStream file = new FileStream(fullPath + $@"\{DateTime.Now.ToString("yyyyMMdd")}.txt", FileMode.Append, FileAccess.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(text);
                    file.Write(bytes, 0, bytes.Length);
                }
            }
        }
        /// <summary>
        /// 规范日志的格式，例如（标题：2019-10-1 12：22：11 内容 ）
        /// </summary>
        /// <param name="LogName">日志的标题</param>
        /// <param name="LogContent">日志的内容</param>
        /// <returns>完整的整理好格式的字符串</returns>
        public static string FormatLog(string LogName, string LogContent) 
            => $"{LogName}({ DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒")})：\n{LogContent}\n\r";

            
        /// <summary>
        /// 书写默认格式的日志
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="content">内容</param>
        /// <param name="type">类型（相对路径下中间加入"\log\"之后的路径）</param>
        public static void WriteLogs(string title, string content, LogType type = LogType.ImplemnetationTrance)
            => WriteLog(FormatLog(title, content), Environment.CurrentDirectory, type);

    }

}
