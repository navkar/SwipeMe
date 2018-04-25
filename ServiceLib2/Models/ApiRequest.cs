using System;

namespace ServiceLib2.Models
{
    public class ApiRequest
    {
        public Uri RequestUrl { get; set; }

        public bool LoadStubData { get; set; }

        public string RequestString { get; set; }

        public string BearerToken { get; set; }

        public string CacheTableName { get; set; }

        public int CacheTimeOutValue { get; set; }

        public DateTime CachedDate { get; set; }

        public string RequestId { get; set; }
    }
}
