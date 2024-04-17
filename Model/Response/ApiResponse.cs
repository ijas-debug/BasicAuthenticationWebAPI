using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthWebAPI.Model.Response
{
    public class ApiResponse<T>
    {
        public T Data { get; set; }
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}