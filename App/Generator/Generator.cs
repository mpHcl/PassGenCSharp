using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassGenCSharp.App.Generator {
    public class Generator {
        public static string generatePasswordAlfabetical(int size) {
            string alfabet = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            string result = "";
            var random = new Random();
            for (int i = 0; i < size; i++) {
                int index = random.Next(0, alfabet.Length - 1);
                result += alfabet[index];
            }


            return result;
        }

        public static string generatePasswordAlfanumerical(int size) {
            string chars = "1234567890AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            string result = "";
            var random = new Random();
            for (int i = 0; i < size; i++) {
                int index = random.Next(0, chars.Length - 1);
                result += chars[index];
            }


            return result;
        }

        public static string generatePasswordPrintableChars(int size) {
            string chars = "1234567890AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz!@#$%^&*()_+{}[]|\\:;\"<,>.?/";
            string result = "";
            var random = new Random();
            for (int i = 0; i < size; i++) {
                int index = random.Next(0, chars.Length - 1);
                result += chars[index];
            }


            return result;
        }
    }
}
