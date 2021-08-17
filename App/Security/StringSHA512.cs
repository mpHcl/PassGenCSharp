using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace PassGenCSharp.App.Security {
    public static class StringSHA512 {
        public static string GetStringHash(string str) {
            SHA512 sha512 = new SHA512Managed();
            return ByteArrToString(sha512.ComputeHash(StringToByteArr(str)));
        }

        static byte[] StringToByteArr(string str) {
            str.Trim();
            byte[] result = new byte[str.Length];

            for (int i = 0; i < str.Length; i++) {
                result[i] = (byte)str[i];
            }


            return result;
        }

        static string ByteArrToString(byte[] arr) {
            string result = "";
            for (int i = 0; i < arr.Length; i++) {
                result += (char)arr[i];
            }


            return result;
        }
    }
}
