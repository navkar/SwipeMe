using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLib2
{
    public class RestClientConnection
    {
        private static RestClientConnection _instance;
        private RestClient _client;

        public RestClient Client => _client ?? (_client = new RestClient());

        private RestClientConnection()
        {
            _client = new RestClient();
        }

        public static RestClientConnection Instance => _instance ?? (_instance = new RestClientConnection());
    }
}
