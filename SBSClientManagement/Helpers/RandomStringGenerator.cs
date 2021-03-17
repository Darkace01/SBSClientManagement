using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SBSClientManagement.Helpers
{
    public class RandomStringGenerator
    {
        private Random r;
        const string UPPERCASE = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string LOWERCASE = "abcdefghijklmnopqrstuvwxyz";
        const string NUMBERS = "0123456789";
        const string SYMBOLS = @"~`!@#$%^&*()-_=+<>?:,./\[]{}|'";

        public RandomStringGenerator()
        {
            r = new Random();
        }

        public RandomStringGenerator(int seed)
        {
            r = new Random(seed);
        }

        public virtual string NextString(int length)
        {
            return NextString(length, true, true, true, true);
        }

        public virtual string NextString(int length, bool lowerCase, bool upperCase, bool numbers, bool symbols)
        {
            char[] charArray = new char[length];
            string charPool = string.Empty;

            //Build character pool
            if (lowerCase)
                charPool += LOWERCASE;

            if (upperCase)
                charPool += UPPERCASE;

            if (numbers)
                charPool += NUMBERS;

            if (symbols)
                charPool += SYMBOLS;

            //Build the output character array
            for (int i = 0; i < charArray.Length; i++)
            {
                //Pick a random integer in the character pool
                int index = r.Next(0, charPool.Length);

                //Set it to the output character array
                charArray[i] = charPool[index];
            }

            return new string(charArray);
        }
    }


    public static class CypherHelper
    {
        public static string EncryptSha512(this string value, string salt)
        {

            var encoding = new ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(salt);
            HMACSHA512 hMACSHA = new HMACSHA512(keyByte);

            byte[] messageBytes = encoding.GetBytes(value);
            byte[] hashmessage = hMACSHA.ComputeHash(messageBytes);

            return ByteToString(hashmessage);
        }

        private static string ByteToString(byte[] buff)
        {
            string sbinary = "";

            for (int i = 0; i < buff.Length; i++)
            {
                sbinary += buff[i].ToString("X2"); // hex format
            }
            return (sbinary);
        }
    }
}
