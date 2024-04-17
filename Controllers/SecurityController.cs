using BasicAuthWebAPI.Helper;
using BasicAuthWebAPI.Model;
using BasicAuthWebAPI.Model.Request;
using BasicAuthWebAPI.Model.Response;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BasicAuthWebAPI.Controllers
{
    public class SecurityController : ApiController
    {
        public SecurityController()
        {
            AppSettings.ReadAppSettings();
        }

        //[HttpGet]
        //[Route("Encrypt")]
        //public ApiResponse<EncryptResponse> EncryptData([FromBody] EncryptRequest request)
        //{
        //    try
        //    {
        //        var cipherText = ConverterClass.Encrypt(request);
        //        return new ApiResponse<EncryptResponse> { StatusCode = 200, Data = cipherText };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<EncryptResponse> { StatusCode = 500, ErrorMessage = ex.Message };
        //    }
        //}

        [HttpGet]
        [Route("encryptV1")]
        public ApiResponse<EncryptResponse> EncryptText(string plainText)
        {
            try
            {
                string encryptedText = EncryptDecrypt.EncryptText(plainText, "@abcd");
                EncryptResponse response = new EncryptResponse { CipherText = encryptedText };
                return new ApiResponse<EncryptResponse> { StatusCode = 200, Data = response };
            }
            catch (Exception ex)
            {
                return new ApiResponse<EncryptResponse> { StatusCode = 500, ErrorMessage = ex.Message };
            }
        }


        //[HttpPost]
        //[Route("Decrypt")]
        //public ApiResponse<DecryptResponse> DecryptData([FromBody] DecryptRequest request)
        //{
        //    try
        //    {
        //        var plainText = ConverterClass.Decrypt(request);
        //        return new ApiResponse<DecryptResponse> { StatusCode = 200, Data = plainText };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new ApiResponse<DecryptResponse> { StatusCode = 500, ErrorMessage = ex.Message };
        //    }
        //}
        
        [HttpPost]
        [Route("Sum")]
        [BasicAPIAuthentication]
        public int Sum(int num1, int num2)
        {
            try
            {
                int sum = num1 + num2;
                return (sum);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
