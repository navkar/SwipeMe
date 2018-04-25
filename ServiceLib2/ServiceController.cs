using DataLib2;
using Plugin.Connectivity;
using ServiceLib2.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLib2
{
    public class ServiceController
    {
        public static ServiceController Instance => Singleton<ServiceController>.Instance;

        private RestClient RestClient => RestClientConnection.Instance.Client;

        public bool UseCache(int timeoutInSeconds, DateTime cachedDate)
        {
            return ((DateTime.UtcNow.Second - cachedDate.Second) < timeoutInSeconds);
        }

        public async Task<ApiResponse<T>> PerformHttpRequestWithData<T>(ApiRequest request, bool isGet)
        {
            try
            {
                var cachedDate = await CacheManager.Instance.GetCachedDateForTable(request.CacheTableName, request.RequestId);
                if (cachedDate != null)
                {
                    Debug.WriteLine("cachedDate: " + cachedDate);

                    var cachedDateTimeValue = Convert.ToDateTime(cachedDate);
                    if (UseCache(request.CacheTimeOutValue, cachedDateTimeValue))
                    {
                        var cachedResponse = await CacheManager.Instance.ReturnCachedResponse(request.CacheTableName, request.RequestId);

                        if (cachedResponse != null)
                        {
                            var response = new ApiResponse<T>
                            {
                                HasCachedData = true,
                                CachedResponse = cachedResponse
                            };
                            return response;
                        }
                    }
                }

                if (!CrossConnectivity.Current.IsConnected)
                {
                    return new ApiResponse<T>
                    {
                        Error = new ApiError
                        {
                            Code = HttpStatusCode.InternalServerError,
                            Message = "check Your internet connection"
                        },
                        HasCachedData = false,
                        Success = false
                    };
                }

                if (isGet)
                {
                    var response = await RestClient.GetAsync<T>(request);
                    return response;
                }
                else
                {
                    var response = await RestClient.PostAsync<T>(request);
                    return response;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("-ex.mEssage-  " + ex.Message + ex.InnerException + ex.StackTrace);

                return new ApiResponse<T>
                {
                    Error = new ApiError
                    {
                        Code = HttpStatusCode.InternalServerError,
                        Message = "check Your internet connection"
                    },
                    HasCachedData = false,
                    Success = false
                };
            }
        }

    }
}
