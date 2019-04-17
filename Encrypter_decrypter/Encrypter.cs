using System;
using System.Security.Cryptography;
using System.Text;

namespace HomeManagerSecurity
{
    public class Encrypter
    {
        private readonly string Hash = "h0m3M@n@g3r";

        public string Encrypt(string s)
        {
            string encrypted;
            byte[] data = UTF8Encoding.UTF8.GetBytes(s);
            using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(Hash));
                using(TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key=keys, Mode=CipherMode.ECB, Padding = PaddingMode.PKCS7 })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    encrypted = Convert.ToBase64String(results, 0, results.Length);                    
                }
            }
            return encrypted;
        }
    }
}
