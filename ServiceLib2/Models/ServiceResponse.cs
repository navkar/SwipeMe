using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLib2.Models
{
    public class ServiceResponse
    {
        public bool Status { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
    }
}
