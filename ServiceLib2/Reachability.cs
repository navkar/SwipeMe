using System;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace ServiceLib2
{
    public class Reachability
    {
        public static string endpoint = "api.iautoscan.com";
        public static Reachability Instance => Singleton<Reachability>.Instance;
        public async Task<bool> IsApiReachable()
        {
            return await CrossConnectivity.Current.IsReachable(endpoint, 3000);
        }

        public bool IsNetworkConnected()
        {
            return CrossConnectivity.Current.IsConnected;
        }

    }
}
