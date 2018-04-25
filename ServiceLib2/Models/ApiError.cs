using System.Net;

namespace ServiceLib2.Models
{
    public class ApiError
    {
        public string Message { get; set; }
        public HttpStatusCode Code { get; set; }
    }
}
