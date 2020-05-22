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
        public static JsonElement? GetProperty(this JsonElement? language, string element)
        {
            if (language.Value.TryGetProperty(element, out JsonElement value))
                return value;
            else
                return null;
        }
        public static Hashtable LanguageJsonElementDictionary { get; set; } = Hashtable.Synchronized(new Hashtable());
        public static Hashtable LanguageByteArrayDictionary { get; set; } = Hashtable.Synchronized(new Hashtable());
        public static void ReadAllLanguageJson()
        {
            var langDictionary = new DirectoryInfo("./wwwroot/src/language");
            var fileInfos = langDictionary.GetFileSystemInfos();
            foreach (var file in fileInfos)
            {
                if (file.Extension == ".json")
                {
                    using var stream = new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
                    var fileName = file.Name.Replace(file.Extension, "");
                    LanguageJsonElementDictionary[fileName] = JsonDocument.Parse(stream).RootElement;
                    LanguageByteArrayDictionary[fileName] = stream;
                }
            }
        }
        public static bool TryAdd(this Hashtable hashtable, object key, object value)
        {
            try
            {
                hashtable.Add(key, value);
                return true;
            }
            catch
            {
                return false;
            }
        }
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
                    byte[] plaindata = System.Text.Encoding.Default.GetBytes(express);//将要加密的字符串转换为字节数组
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);//将加密后的字节数据转换为新的加密字节数组
                    return Convert.ToBase64String(encryptdata);//将加密后的字节数组转换为字符串
                }
            }
            /// <summary> 
            /// RSA解密数据 
            /// </summary> 
            /// <param name="express">要解密数据</param> 
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
                    return System.Text.Encoding.Default.GetString(decryptdata);
                }
            }

        }
    }
}
