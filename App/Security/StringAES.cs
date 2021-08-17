using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PassGenCSharp.App.Security {
    class StringAES {
        private byte[] key;
        private byte[] vector;


        public StringAES() {
            key = GetKey();
            vector = GetIV();
        }

        public byte[] EncryptStringToBytes(string plainText) {
            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = vector;

            ICryptoTransform encryptor = aes.CreateEncryptor(key, vector);
            byte[] encrypted;
            using (MemoryStream msEncrypt = new MemoryStream()) {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt)) {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }

            return encrypted;
        }

        public string DecryptBytesToString(byte[] chiperText) {
            Aes aes = Aes.Create();
            aes.Key = key;
            aes.IV = vector;

            ICryptoTransform decryptor = aes.CreateDecryptor(key, vector);
            string decrypted;
            using (MemoryStream msDecrypt = new MemoryStream(chiperText)) {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt)) {
                        decrypted = srDecrypt.ReadToEnd();
                    }

                }
            }
            return decrypted;
        }

        static byte[] GetKey() {
            byte[] key;
            if (File.Exists("key.txt")) {
                var keyStrings = File.ReadAllLines("key.txt");
                key = new byte[keyStrings.Length];
                for (int i = 0; i < keyStrings.Length; i++) {
                    key[i] = byte.Parse(keyStrings[i]);
                }
                return key;
            }
            else {
                key = Aes.Create().Key;
                using (StreamWriter writer = new StreamWriter("key.txt")) {
                    for (int i = 0; i < key.Length; i++) {
                        writer.WriteLine(key[i]);
                    }
                }
                return key;
            }

        }

        static byte[] GetIV() {
            byte[] IV;
            if (File.Exists("IV.txt")) {
                var ivStrings = File.ReadAllLines("IV.txt");
                IV = new byte[ivStrings.Length];
                for (int i = 0; i < ivStrings.Length; i++) {
                    IV[i] = byte.Parse(ivStrings[i]);
                }
                return IV;
            }
            else {
                IV = Aes.Create().IV;
                using (StreamWriter writer = new StreamWriter("IV.txt")) {
                    for (int i = 0; i < IV.Length; i++) {
                        writer.WriteLine(IV[i]);
                    }
                }
                return IV;
            }
        }

        public string BytesArrayAsString(byte[] arr) {
            string result = "";
            for (int i = 0; i < arr.Length; i++) {
                result += arr[i] + " ";
            }

            return result;
        }

        public byte[] StringAsByteArray(string str) {
            string[] bytesStrings = str.Trim().Split(' ');
            byte[] bytes = new byte[bytesStrings.Length];
            for (int i = 0; i < bytesStrings.Length; i++) {
                bytes[i] = byte.Parse(bytesStrings[i]);
            }

            return bytes;
        }
    }
}
