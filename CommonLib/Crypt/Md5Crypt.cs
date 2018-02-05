using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Crypt
{
    /// <summary>
    /// 模块编号：帮助类-加解密
    /// 作用：MD5加密
    /// 作者：丁纪名
    /// 编写日期：2017-12-04
    /// </summary>
    public class Md5Crypt
    {
        /// <summary>
        /// MD5加密方法
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public string Encrypt(string source)
        {
            var bytes = Encoding.Default.GetBytes(source);//求Byte[]数组  
            var Md5 = new MD5CryptoServiceProvider().ComputeHash(bytes);//求哈希值  
            return Convert.ToBase64String(Md5);//将Byte[]数组转为净荷明文(其实就是字符串)  
        }
    }
}
