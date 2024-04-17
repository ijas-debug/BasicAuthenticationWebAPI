using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BasicAuthWebAPI.Model.Request
{
    public class DecryptRequest
    {
        public string CipherText { get; set; }
    }
}