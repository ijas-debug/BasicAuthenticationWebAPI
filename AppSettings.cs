using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BasicAuthWebAPI
{
    public class AppSettings
    {
        #region
        //public static string Password { get; set; }
        //public static string Salt { get; set; }
        //public static string HashAlgorithm { get; set; }
        //public static int PasswordIterations { get; set; }
        //public static string InitialVector { get; set; }
        //public static int KeySize { get; set; }

        //public static void ReadAppSettings()
        //{
        //    Password = ConfigurationManager.AppSettings["Password"];
        //    Salt = ConfigurationManager.AppSettings["Salt"];
        //    HashAlgorithm = ConfigurationManager.AppSettings["HashAlgorithm"];
        //    PasswordIterations = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordIterations"]);
        //    InitialVector = ConfigurationManager.AppSettings["InitialVector"];
        //    KeySize = Convert.ToInt32(ConfigurationManager.AppSettings["KeySize"]);
        //}
        #endregion

        public static bool EnableAPIAuthentication { get; private set; }
        public static string AuthenticationUsername { get; private set; }
        public static string AuthenticationPassword { get; private set; }
        public static string AuthenticationSalt { get; private set; }
        public static string SaltKey { get; private set; }
       // public static bool SAF_RSAEncypt1 { get; private set; }
        public static string HashAlgorithm { get; private set; }
        public static string InitialVector { get; private set; }
        public static string PasswordEnc { get; private set; }
        public static int PasswordIterations { get; private set; }
        public static int KeySize { get; private set; }

        public static void ReadAppSettings()
        {
            EnableAPIAuthentication = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableAPIAuthentication"]);
            AuthenticationUsername = ConfigurationManager.AppSettings["AuthenticationUsername"];
            AuthenticationPassword = ConfigurationManager.AppSettings["AuthenticationPassword"];
            AuthenticationSalt = ConfigurationManager.AppSettings["AuthenticationSalt"];
            SaltKey = ConfigurationManager.AppSettings["SaltKey"];
           // SAF_RSAEncypt1 = Convert.ToBoolean(ConfigurationManager.AppSettings["SAF.RSAEncypt1"]);
            HashAlgorithm = ConfigurationManager.AppSettings["HashAlgorithm"];
            InitialVector = ConfigurationManager.AppSettings["InitialVector"];
            PasswordEnc = ConfigurationManager.AppSettings["passwordEnc"];
            PasswordIterations = Convert.ToInt32(ConfigurationManager.AppSettings["PasswordIterations"]);
            KeySize = Convert.ToInt32(ConfigurationManager.AppSettings["KeySize"]);
        }
    }
}