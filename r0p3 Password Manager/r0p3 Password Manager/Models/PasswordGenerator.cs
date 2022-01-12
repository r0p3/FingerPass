using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r0p3_Password_Manager.Models
{
    class PasswordGenerator
    {
        public static string generate(int length, bool useLowerCase, bool useUpperCase, bool useNumbers, bool useSymbols)
        {
            string letters = "abcdefghijklmnopqrstuvwxyz";
            string numbers = "0123456789";
            string symobls = "!@$%&*=?+-<>";
            List<char> passwordChars = new List<char>();
            if (useLowerCase)
                passwordChars.AddRange(letters.ToCharArray());
            if (useUpperCase)
                passwordChars.AddRange(letters.ToUpper().ToCharArray());
            if (useNumbers)
                passwordChars.AddRange(numbers);
            if (useSymbols)
                passwordChars.AddRange(symobls);

            if (passwordChars.Count > 0 && length > 0)
            {
                Random rng = new Random();
                string returnString = "";
                for (int i = 0; i < length + 1; i++)
                {
                    returnString += passwordChars[rng.Next(0, passwordChars.Count)];

                    if (i == length - 1)
                    {
                        char[] tempChar = returnString.ToCharArray();
                        if (useLowerCase)
                            if (!tempChar.Intersect(letters.ToCharArray()).Any())
                                i = 0;
                        if (useUpperCase)
                            if (!tempChar.Intersect(letters.ToUpper().ToCharArray()).Any())
                                i = 0;
                        if (useNumbers)
                            if (!tempChar.Intersect(numbers.ToCharArray()).Any())
                                i = 0;
                        if (useSymbols)
                            if (!tempChar.Intersect(symobls.ToCharArray()).Any())
                                i = 0;
                        if (i == 0)
                        {
                            returnString = "";
                        }
                    }
                }
                return returnString;
            }
            else
                return "";
        }
    }
}
