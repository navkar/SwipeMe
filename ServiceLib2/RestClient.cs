using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using ServiceLib2.Models;

namespace ServiceLib2
{
    public class RestClient
    {
        private readonly HttpClient _client;
        public RestClient()
        {
            _client = new HttpClient { Timeout = TimeSpan.FromSeconds(180) };
        }

        public async Task<ApiResponse<T>> GetAsync<T>(ApiRequest request)
        {
            if (request == null)
            {
                return new ApiResponse<T>
                {
                    Error = new ApiError { Code = HttpStatusCode.NotFound, Message = "empty request" },
                    HasCachedData = false,
                    Success = false
                };
            }

            Debug.WriteLine(string.Format("Url: {0}", request.RequestUrl));
            Debug.WriteLine(string.Format("Request data: {0} ", request.RequestString));

            try
            {
                AddHeaders(request.BearerToken);
                var response = await _client.GetAsync(request.RequestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(string.Format("Url: {0}", request.RequestUrl));
                    Debug.WriteLine(string.Format("Response data: {0} ", responseString));

                    try
                    {
                        var responseObject = JsonConvert.DeserializeObject<T>(responseString);
                        return new ApiResponse<T>
                        {
                            Value = responseObject != null ? responseObject : default(T),
                            HasCachedData = false,
                            Success = true,
                            Response = response,
                            Error = new ApiError
                            {
                                Code = response.StatusCode,
                                Message = response.ReasonPhrase
                            }
                        };
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("* * * API EXCEPTION * * *" + ex.Message, ex.InnerException, ex.StackTrace);

                        return new ApiResponse<T>
                        {
                            HasCachedData = false,
                            Success = false,
                            Error = new ApiError
                            {
                                Code = HttpStatusCode.NotFound,
                                Message = ex.Message
                            }
                        };
                    }
                }

                return new ApiResponse<T>
                {
                    HasCachedData = false,
                    Success = false,
                    Error = new ApiError
                    {
                        Code = response.StatusCode,
                        Message = response.ReasonPhrase
                    }
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse<T>
                {
                    Error = new ApiError { Code = HttpStatusCode.NotFound, Message = ex.Message },
                    HasCachedData = false,
                    Success = false
                };
            }
        }

        #region POST
        public async Task<ApiResponse<T>> PostAsync<T>(ApiRequest request)
        {
            if (null == request)
            {
                return new ApiResponse<T>
                {
                    // Setting default response status code as Not Found
                    Error = new ApiError { Code = HttpStatusCode.NotFound, Message = "Empty request" },
                    HasCachedData = false,
                    Success = false
                };
            }

            Debug.WriteLine(string.Format("Url: {0}", request.RequestUrl));
            Debug.WriteLine(string.Format("Request data: {0} ", request.RequestString));

            try
            {
                var content = new StringContent(request.RequestString, Encoding.UTF8, "application/json");
                AddHeaders(request.BearerToken);

                var response = await _client.PostAsync(request.RequestUrl.ToString(), content);
                
                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(string.Format("Url: {0}", request.RequestUrl));
                    Debug.WriteLine(string.Format("Response data: {0} ", responseString));

                    var responseObject = default(T);
                    try
                    {
                        responseObject = JsonConvert.DeserializeObject<T>(responseString);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception: " + ex.Message + ex.InnerException + ex.StackTrace);
                    }
                    try
                    {
                        var serviceResponse = JsonConvert.DeserializeObject<ServiceResponse>(responseString);
                        if (serviceResponse != null)
                        {
                            if (serviceResponse != null && 
                                (serviceResponse.StatusCode == 1111 || serviceResponse.StatusCode == 2222))
                            {
                                return new ApiResponse<T>
                                {
                                    Value = responseObject != null ? responseObject : default(T),
                                    HasCachedData = false,
                                    Success = true,
                                    Response = response,
                                    Error = new ApiError
                                    {
                                        Code = HttpStatusCode.InternalServerError,
                                        Message = serviceResponse.StatusMessage
                                    }
                                };
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Exception: " + ex.Message + ex.InnerException + ex.StackTrace);
                    }

                    return new ApiResponse<T>
                    {
                        Value = responseObject != null ? responseObject : default(T),
                        HasCachedData = false,
                        Success = true,
                        Response = response,
                        Error = new ApiError
                        {
                            Code = response.StatusCode,
                            Message = response.ReasonPhrase
                        }
                    };
                }
                return new ApiResponse<T>
                {
                    HasCachedData = false,
                    Success = false,
                    Error = new ApiError
                    {
                        Code = response.StatusCode,
                        Message = response.ReasonPhrase
                    }
                };
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Exception: " + ex.Message + ex.InnerException + ex.StackTrace);

                return new ApiResponse<T>
                {
                    Error = new ApiError
                    {
                        Code = HttpStatusCode.NotFound,
                        Message = ex.Message
                    },
                    HasCachedData = false,
                    Success = false
                };
            }
        }
        #endregion

        private void AddHeaders(string bearerToken)
        {
            _client.DefaultRequestHeaders.Clear();

            if (!string.IsNullOrEmpty(bearerToken))
            {
                _client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", bearerToken));
            }

            _client.DefaultRequestHeaders.Add("Content-Type", "application/json");
            Debug.WriteLine("Headers: " + _client.DefaultRequestHeaders);
        }
    }
}
