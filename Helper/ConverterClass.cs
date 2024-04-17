//using BasicAuthWebAPI.Model;
//using BasicAuthWebAPI.Model.Request;
//using BasicAuthWebAPI.Model.Response;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using System.Web;

//namespace BasicAuthWebAPI.Helper
//{
//    public class ConverterClass
//    {
//        public static EncryptResponse Encrypt(EncryptRequest request)
//        {
//            EncryptResponse obj = new EncryptResponse();
//            if (string.IsNullOrEmpty(request.PlainText)) return obj;

//            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(AppSettings.InitialVector);
//            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(AppSettings.Salt);
//            byte[] PlainTextBytes = Encoding.UTF8.GetBytes(request.PlainText);
//            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(AppSettings.Password, SaltValueBytes, AppSettings.HashAlgorithm, AppSettings.PasswordIterations);
//            byte[] KeyBytes = DerivedPassword.GetBytes(AppSettings.KeySize / 8);
//            RijndaelManaged SymmetricKey = new RijndaelManaged();
//            SymmetricKey.Mode = CipherMode.CBC;
//            byte[] CipherTextBytes = null;

//            using (ICryptoTransform Encryptor = SymmetricKey.CreateEncryptor(KeyBytes, InitialVectorBytes))
//            {
//                using (MemoryStream MemStream = new MemoryStream())
//                {
//                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Encryptor, CryptoStreamMode.Write))
//                    {
//                        CryptoStream.Write(PlainTextBytes, 0, PlainTextBytes.Length);
//                        CryptoStream.FlushFinalBlock();
//                        CipherTextBytes = MemStream.ToArray();
//                        MemStream.Close();
//                        CryptoStream.Close();
//                    }
//                }
//            }

//            SymmetricKey.Clear();
//            return new EncryptResponse { CipherText = Convert.ToBase64String(CipherTextBytes) };
//        }

//        public static DecryptResponse Decrypt(DecryptRequest request)
//        {
//            DecryptResponse obj = new DecryptResponse();
//            if (string.IsNullOrEmpty(request.CipherText)) return obj;

//            byte[] InitialVectorBytes = Encoding.ASCII.GetBytes(AppSettings.InitialVector);
//            byte[] SaltValueBytes = Encoding.ASCII.GetBytes(AppSettings.Salt);
//            byte[] CipherTextBytes = Convert.FromBase64String(request.CipherText);
//            PasswordDeriveBytes DerivedPassword = new PasswordDeriveBytes(AppSettings.Password, SaltValueBytes, AppSettings.HashAlgorithm, AppSettings.PasswordIterations);
//            byte[] KeyBytes = DerivedPassword.GetBytes(AppSettings.KeySize / 8);
//            RijndaelManaged SymmetricKey = new RijndaelManaged();
//            SymmetricKey.Mode = CipherMode.CBC;
//            SymmetricKey.Padding = PaddingMode.PKCS7;
//            byte[] PlainTextBytes = new byte[CipherTextBytes.Length];
//            int ByteCount = 0;

//            using (ICryptoTransform Decryptor = SymmetricKey.CreateDecryptor(KeyBytes, InitialVectorBytes))
//            {
//                using (MemoryStream MemStream = new MemoryStream(CipherTextBytes))
//                {
//                    using (CryptoStream CryptoStream = new CryptoStream(MemStream, Decryptor, CryptoStreamMode.Read))
//                    {
//                        ByteCount = CryptoStream.Read(PlainTextBytes, 0, PlainTextBytes.Length);
//                        MemStream.Close();
//                        CryptoStream.Close();
//                    }
//                }
//            }

//            SymmetricKey.Clear();
//            return new DecryptResponse { PlainText = Encoding.UTF8.GetString(PlainTextBytes, 0, ByteCount) };
//        }
//    }
//}