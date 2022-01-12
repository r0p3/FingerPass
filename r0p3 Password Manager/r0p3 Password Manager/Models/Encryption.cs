using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace r0p3_Password_Manager.Models
{
    public class Encryption
    {
        private string key;

        public object Hash { get; private set; }

        public Encryption(string key)
        {
            this.key = key;
        }
        public Encryption()
        {

        }



        public string encrypt(string plainText, string key)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(MyHash.hash(key)), new byte[8], 1).GetBytes(32);
                    ICryptoTransform cryptoTransform = aes.CreateEncryptor(aes.Key, new byte[16]);
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Write))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            {
                                streamWriter.Write(plainText);
                            }
                            return Convert.ToBase64String(memoryStream.ToArray());
                        }
                    }
                }
            }
            catch (Exception)
            {

            }
            return "";
        }

        public string decrypt(string cipher, string key)
        {
            try
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(MyHash.hash(key)), new byte[8], 1).GetBytes(32);
                    ICryptoTransform cryptoTransform = aes.CreateDecryptor(aes.Key, new byte[16]);
                    using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(cipher)))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(memoryStream, cryptoTransform, CryptoStreamMode.Read))
                        {
                            using (StreamReader streamReader = new StreamReader(cryptoStream))
                            {
                                return streamReader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                e.Message.ToString();
            }
            return "";
        }
    }
}
