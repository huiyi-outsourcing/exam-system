using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace ExamSystem.utils {
    public class EncryptHelper {
        public static String encrypt(String pwd) {
            MD5CryptoServiceProvider provider = new MD5CryptoServiceProvider();
            byte[] source = Encoding.UTF8.GetBytes(pwd);
            byte[] target = provider.ComputeHash(source);

            return Convert.ToBase64String(target);
        }
    }
}
