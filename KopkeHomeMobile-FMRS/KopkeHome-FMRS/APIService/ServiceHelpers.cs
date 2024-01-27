using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using KopkeHome_FMRS.Models.ResponseModels;
using KopkeHome_FMRS.Helper;
using Newtonsoft.Json;
using Microsoft.Maui.ApplicationModel.Communication;
using KopkeHome_FMRS.Models.RequestModels;
using Microsoft.AppCenter.Crashes;


namespace KopkeHome_FMRS.ServiceHelper
{
    public class ServiceHelpers
    {

        #region Private Members   
        ResponseModel responseModel = null;
        UserWorkStatusResponseModel userWorkStatusResponseModel = null; 
        List<SearchContractorsListResponse> searchContractorsListResponse = null;
        private List<CategoriesListResponseModel> categoriesListResponseModel = null;
        private List<ZipCodeListResponseModel> zipcodeResponseModel = null;
        private static ServiceHelpers instance = null;
        #endregion

        #region Property
        public static ServiceHelpers Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceHelpers();
                }
                return instance;
            }
        }
        #endregion
        #region Methods
        public async Task<ResponseModel> PostRequest(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));
                    }
                    string baseUrl = Constants.BaseUrl;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(120);
                    var uri = new Uri(serviceUrl);


                    if (strMethod == Constants.ForgotPassword)
                    {
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        form.Add(new StringContent(strSerializedData), "email");
                        HttpResponseMessage response1 = await httpClient.PostAsync(uri, form);
                        response1.EnsureSuccessStatusCode();
                        httpClient.Dispose();
                        result = response1.Content.ReadAsStringAsync().Result;
                        responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
                        return this.responseModel;
                    }
                    if (strMethod == "Account/GetUserByEmail")
                    {
                        MultipartFormDataContent form = new MultipartFormDataContent();
                        form.Add(new StringContent(strSerializedData), "email");
                        HttpResponseMessage response1 = await httpClient.PostAsync(uri, form);
                        response1.EnsureSuccessStatusCode();
                        httpClient.Dispose();
                        result = response1.Content.ReadAsStringAsync().Result;
                        responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);
                        return this.responseModel;
                    }
                    using (StringContent content = new StringContent(strSerializedData, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = null;

                        response = await httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            responseModel = JsonConvert.DeserializeObject<ResponseModel>(result);

                            return this.responseModel;
                        }
                        else
                        {
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.NotAcceptable:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.BadRequest:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.Unauthorized:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = true,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.NotFound:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.InternalServerError:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                default:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.responseModel = new ResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                            }
                        }
                        this.responseModel = new ResponseModel
                        {
                            //IsSuccess = false,
                            //IsUnauthorized = false,
                            //Data = result
                        };
                        return this.responseModel;
                    }
                }
            }
            catch (Exception ex)
            {
                // Crashes.TrackError(ex);
                Console.WriteLine("Exception" + ex.ToString());
                this.responseModel = new ResponseModel
                {
                    //IsSuccess = false,
                    //IsUnauthorized = false,
                    //Data = string.Empty
                };
                return this.responseModel;
            }
        }

        public async Task<ResponseModel> PostRequestFor(string serializedData, string baseUrl, string method, bool isHeader, string token)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var result = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIHttpHelper.Bearer, token);
                    }
                    var serviceUrl = baseUrl + method;
                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                    using (var content = new StringContent(serializedData, Encoding.UTF8, APIHttpHelper.ApplicationJsonText))
                    {
                        using (var response = await httpClient.PostAsync(uri, content))
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            responseModel = new ResponseModel
                            {
                                error = response.IsSuccessStatusCode,
                                data = result
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Crashes.TrackError(ex);
            }
            return responseModel;
        }


        public static async Task<ResponseModel> PostRequest1(string serializedData, string baseUrl, string method, bool isHeader, string token)
        {
            ResponseModel responseModel = new ResponseModel();
            try
            {
                var result = string.Empty;
                using (var httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIHttpHelper.Bearer, token);
                    }
                    var serviceUrl = baseUrl + method;
                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);

                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(serializedData), "email");
                    form.Add(new StringContent(serializedData), "id");
                    HttpResponseMessage response = await httpClient.PostAsync(uri, form);
                    response.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    result = response.Content.ReadAsStringAsync().Result;
                    responseModel = new ResponseModel
                    {
                        error = response.IsSuccessStatusCode,
                        data = result
                    };
                    return responseModel;
                }
            }
            catch (Exception ex)
            {
               // Crashes.TrackError(ex);
            }
            return responseModel;
        }


        public async Task<List<SearchContractorsListResponse>> PostRequestContractor(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));
                    }
                    string baseUrl = Constants.BaseUrl;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(120);
                    var uri = new Uri(serviceUrl);

                    using (StringContent content = new StringContent(strSerializedData, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = null;
                        response = await httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            searchContractorsListResponse = JsonConvert.DeserializeObject<List<SearchContractorsListResponse>>(result);

                            return this.searchContractorsListResponse;
                        }
                        else
                        {
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.NotAcceptable:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                                case HttpStatusCode.BadRequest:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                                case HttpStatusCode.Unauthorized:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                                case HttpStatusCode.NotFound:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                                case HttpStatusCode.InternalServerError:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                                default:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                                    {
                                    };
                                    break;
                            }
                        }
                        this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                        {
                            //IsSuccess = false,
                            //IsUnauthorized = false,
                            //Data = result
                        };
                        return this.searchContractorsListResponse;
                    }
                }
            }
            catch (Exception ex)
            {
                // Crashes.TrackError(ex);
                Console.WriteLine("Exception" + ex.ToString());
                this.searchContractorsListResponse = new List<SearchContractorsListResponse>
                {
                    //IsSuccess = false,
                    //IsUnauthorized = false,
                    //Data = string.Empty
                };
                return this.searchContractorsListResponse;
            }
        }

        public async Task<UserWorkStatusResponseModel> PostRequestFilter(string strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));
                    }
                    string baseUrl = Constants.BaseUrl;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(120);
                    var uri = new Uri(serviceUrl);

                    using (StringContent content = new StringContent(strSerializedData, Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage response = null;
                        response = await httpClient.PostAsync(uri, content);
                        if (response.IsSuccessStatusCode)
                        {
                            result = response.Content.ReadAsStringAsync().Result;
                            userWorkStatusResponseModel = JsonConvert.DeserializeObject<UserWorkStatusResponseModel>(result);
                            return this.userWorkStatusResponseModel;
                        }
                        else
                        {
                            switch (response.StatusCode)
                            {
                                case HttpStatusCode.NotAcceptable:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.BadRequest:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.Unauthorized:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = true,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.NotFound:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                case HttpStatusCode.InternalServerError:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                                default:
                                    result = response.Content.ReadAsStringAsync().Result;
                                    this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                                    {
                                        //IsSuccess = false,
                                        //IsUnauthorized = false,
                                        //Data = result
                                    };
                                    break;
                            }
                        }
                        this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                        {
                            //IsSuccess = false,
                            //IsUnauthorized = false,
                            //Data = result
                        };
                        return this.userWorkStatusResponseModel;
                    }
                }
            }
            catch (Exception ex)
            {
                // Crashes.TrackError(ex);
                Console.WriteLine("Exception" + ex.ToString());
                this.userWorkStatusResponseModel = new UserWorkStatusResponseModel
                {
                    //IsSuccess = false,
                    //IsUnauthorized = false,
                    //Data = string.Empty
                };
                return this.userWorkStatusResponseModel;
            }
        }

        public async Task<List<ZipCodeListResponseModel>> MultiPartZipCodeList(ZipCodeListRequestModel strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                zipcodeResponseModel = new List<ZipCodeListResponseModel>();
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));
                    }
                    string baseUrl = Constants.BaseUrl;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(120);
                    var uri = new Uri(serviceUrl);

                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(strSerializedData.Prefix), "prefix");
                    form.Add(new StringContent(strSerializedData.UserID), "userID");

                    HttpResponseMessage response1 = await httpClient.PostAsync(uri, form);
                    response1.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    result = response1.Content.ReadAsStringAsync().Result;
                    zipcodeResponseModel = JsonConvert.DeserializeObject<List<ZipCodeListResponseModel>>(result);
                    return this.zipcodeResponseModel;
                }
                
            }
            catch(Exception ex)
            {
                
            }
            return this.zipcodeResponseModel;
        }

        public async Task<List<CategoriesListResponseModel>> MultiPartCategoryList(CategoriesListRequestModel strSerializedData, string strMethod, bool isHeader, string token)
        {
            try
            {
                categoriesListResponseModel = new List<CategoriesListResponseModel>();
                string result = string.Empty;
                using (HttpClient httpClient = new HttpClient())
                {
                    if (isHeader)
                    {
                        httpClient.DefaultRequestHeaders.Clear();
                        httpClient.DefaultRequestHeaders.Add("Authorization", string.Concat("Bearer ", token));
                    }
                    string baseUrl = Constants.BaseUrl;
                    string serviceUrl = string.Empty;
                    serviceUrl = baseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(120);
                    var uri = new Uri(serviceUrl);

                    MultipartFormDataContent form = new MultipartFormDataContent();
                    form.Add(new StringContent(strSerializedData.Prefix), "prefix");
                    form.Add(new StringContent(strSerializedData.Zipcode.ToString()), "zipcode");
                    HttpResponseMessage response1 = await httpClient.PostAsync(uri, form);
                    response1.EnsureSuccessStatusCode();
                    httpClient.Dispose();
                    result = response1.Content.ReadAsStringAsync().Result;
                    categoriesListResponseModel = JsonConvert.DeserializeObject<List<CategoriesListResponseModel>>(result);
                    return this.categoriesListResponseModel;
                }
            }
            catch (Exception ex)
            {

            }
            return this.categoriesListResponseModel;
        }
        /// <summary>
        /// PostMultipartRequest used to get multipart data
        /// </summary>
        /// <param name="dataContent"></param>
        /// <param name="strMethod"></param>
        /// <returns></returns>
        public static async Task<HttpResponseMessage> PostMultipartRequest(MultipartFormDataContent dataContent, string strMethod)
        {
            HttpResponseMessage response = null;
            HttpRequestMessage httpRequest = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    string serviceUrl = Constants.BaseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                    httpRequest = new HttpRequestMessage(new HttpMethod("POST"), uri);
                    httpRequest.Content = dataContent;
                    response = await httpClient.SendAsync(httpRequest);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> PostRequestGood(string strSerializedData, string strMethod, bool isContantRequire, string Token  )
        {
            HttpResponseMessage response = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (isContantRequire)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIHttpHelper.Bearer,Token);
                    }
                    string serviceUrl = Constants.BaseUrl + strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                    var content = new StringContent(strSerializedData, Encoding.UTF8, Constants.ApplicationType);
                    response = await httpClient.PostAsync(uri, content);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return response;
        }

        public static async Task<HttpResponseMessage> GetRequest(string strSerializedData, string strMethod, bool isContantRequire)
        {
            HttpResponseMessage response = null;
            try
            {
                using (var httpClient = new HttpClient())
                {
                    if (isContantRequire)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(APIHttpHelper.Bearer, Constants.ResetPasswordToken);
                    }
                    string serviceUrl = strMethod;
                    httpClient.Timeout = TimeSpan.FromSeconds(100);
                    var uri = new Uri(serviceUrl);
                  //  var content = new StringContent(strSerializedData, Encoding.UTF8, Constants.ApplicationType);
                    response = await httpClient.GetAsync(uri);
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return response;
        }
        #endregion
    }
}
