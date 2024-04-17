using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BasicAuthWebAPI.Helper
{
    public class EncryptDecrypt
    {
        private static string _password;
        private static string _HashAlgorithm;
        private static int _PasswordIterations;
        private static string _InitialVector;
        private static int _KeySize;

        public static int KeySize
        {
            get => _KeySize;
            set => _KeySize = value;
        }

        public static string InitialVector
        {
            get => _InitialVector;
            set => _InitialVector = value;
        }

        public static string password
        {
            get => _password;
            set => _password = value;
        }

        public static int PasswordIterations
        {
            get => _PasswordIterations;
            set => _PasswordIterations = value;
        }

        public static string HashAlgorithm
        {
            get => _HashAlgorithm;
            set => _HashAlgorithm = value;
        }

        public static string DecryptText(string encryptedText, string salt)
        {
            InitPasswordDetails();
            return DecryptValue(encryptedText, password, salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);
        }
        public static string EncryptText(string plainText, string salt)
        {
            InitPasswordDetails();
            return EncryptValue(plainText, password, salt, HashAlgorithm, PasswordIterations, InitialVector, KeySize);

        }
        private static string EncryptValue(string PlainText, string Password,
                                     string Salt, string HashAlgorithm,
                                    int PasswordIterations, string InitialVector, int KeySize)
        {

            if (string.IsNullOrEmpty(PlainText))

                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);

            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);

            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(PlainText);

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);

            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();

            SymmetricKey.Mode = CipherMode.CBC;

            byte[] CipherTextBytes = null;

            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
            {

                using (MemoryStream MemStream = new MemoryStream())
                {

                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
                    {

                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);

                        CryptoStream.FlushFinalBlock();

                        CipherTextBytes = MemStream.ToArray();

                        MemStream.Close();

                        CryptoStream.Close();

                    }
                }
            }
            SymmetricKey.Clear();

            return Convert.ToBase64String(CipherTextBytes);
        }
        private static string DecryptValue(string CipherText, string Password,
       string Salt, string HashAlgorithm,
       int PasswordIterations, string InitialVector, int KeySize)
        {

            if (string.IsNullOrEmpty(CipherText))

                return "";

            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(InitialVector);

            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(Salt);

            byte[] CipherTextBytes = Convert.FromBase64String(CipherText);

            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(Password, SaltValueBytes, HashAlgorithm, PasswordIterations);

            byte[] KeyBytes = DerivedPassword.GetBytes(KeySize / 8);

            RijndaelManaged SymmetricKey = new RijndaelManaged();

            SymmetricKey.Mode = CipherMode.CBC;

            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];

            int ByteCount = 0;

            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
            {
                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
                {
                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
                    {
                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);

                        MemStream.Close();

                        CryptoStream.Close();
                    }
                }
            }

            SymmetricKey.Clear();

            return Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount);

        }
        private static void InitPasswordDetails()
        {
            password = "P@ssw0rddo!!a6";
            HashAlgorithm = "SHA256";
            PasswordIterations = 2;
            InitialVector = "OFRna73m*aze01xY";
            KeySize = 256;
        }
    }
}