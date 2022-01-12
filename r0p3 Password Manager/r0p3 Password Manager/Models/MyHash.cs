using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace r0p3_Password_Manager.Models
{
    public class MyHash
    {
        public static string hash(string pw)
        {
            SHA512Managed sha = new SHA512Managed();
            var hashedPw = sha.ComputeHash(Encoding.UTF8.GetBytes(pw));
            return Encoding.UTF8.GetString(hashedPw);
        }

        public static string doubleHash(string pw)
        {
            return hash(hash(pw));
        }

        public static bool compareLogin(string pw)
        {
            /*pw = doubleHash(pw);
            if (pw == Settings.Default.PasswordHash)
                return true;*/
            return false;
        }

        public static void saveFirstPW(string pw)
        {
            //pw = doubleHash(pw);
            /*Settings.Default.PasswordHash = pw;
            Settings.Default.Save();*/
        }
    }
}

