using System.Net;
using System.Net.Http;

namespace ServiceLib2.Models
{
    public class ApiResponse<T>
    {
        public bool HasCachedData { get; set; }

        public bool HasStubData { get; set; }

        public HttpResponseMessage Response { get; set; }

        public NetworkStatusError NetworkError { get; set; }

        public string CachedResponse { get; set; }

        public string StubbedResponse { get; set; }

        public ApiError Error { get; set; }

        public bool Success { get; set; }

        public T Value;
    }
}
