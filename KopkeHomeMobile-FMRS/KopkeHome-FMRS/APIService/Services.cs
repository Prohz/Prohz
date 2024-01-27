using KopkeHome_FMRS.Helper;
using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace KopkeHome_FMRS.ServiceHelper
{
    public class Services : IServices
    {
        #region Private Members
        private static ServiceHelpers serviceHelpers;
        JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            DateTimeZoneHandling = DateTimeZoneHandling.Utc,
            NullValueHandling = NullValueHandling.Ignore
        };
        #endregion
        #region Constructor
        public Services()
        {
            serviceHelpers = ServiceHelpers.Instance;
            serializerSettings.Converters.Add(new StringEnumConverter());
        }
        #endregion 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        public async Task<ResponseModel> UserLogin(LoginRequestMode loginRequestModel)
        {
            ResponseModel loginResponseModel = new ResponseModel();
            try
            {
                string Serializeobject = JsonConvert.SerializeObject(loginRequestModel);
                ResponseModel response = await serviceHelpers.PostRequest(Serializeobject, Constants.AccountSignIn, false, Constants.token);
                loginResponseModel = response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); 
            }
            return loginResponseModel;
        }

        /// <summary>
        /// This service is used for the get the release ticket detail
        /// </summary>
        /// <returns></returns>
        public async Task<GetUserByEmailResponseModel> GetUserbyEmail(string Email)
        {
            GetUserByEmailResponseModel releaseticketDeatil = null;
            try
            {
                var jsonRequestForLogin = Email;
                var response = await ServiceHelpers.PostRequest1(jsonRequestForLogin, Constants.BaseUrl, Constants.GetUserByEmailId, false, string.Empty);
                if (response.error)
                {
                    releaseticketDeatil = JsonConvert.DeserializeObject<GetUserByEmailResponseModel>(response.data, serializerSettings);
                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return releaseticketDeatil;
        }
         
        public async Task<List<ZipCodeListResponseModel>> ZipCodeList(string Prefix, string UserID)
        {
            List<ZipCodeListResponseModel> zipCodeListResponseModel = new List<ZipCodeListResponseModel>();
            ZipCodeListRequestModel zipCodeListRequestModel = new ZipCodeListRequestModel();
            try
            {
                zipCodeListRequestModel.Prefix = Prefix;
                zipCodeListRequestModel.UserID = UserID;

                zipCodeListResponseModel = await serviceHelpers.MultiPartZipCodeList(zipCodeListRequestModel, Constants.GetZipCodeList, true, Constants.token);
            }
            catch (Exception ex)
            {
                Crashes.TrackError (ex);
            }
            return zipCodeListResponseModel;
        }

        ///// <summary>
        /////
        ///// </summary>
        ///// <param name="Email"></param>
        ///// <returns></returns>
        public async Task<List<CategoriesListResponseModel>> CategoryList(string Prefix, int zipCode)
        {
            List<CategoriesListResponseModel> categoriesListResponseModel = new List<CategoriesListResponseModel>();
            CategoriesListRequestModel categoriesListRequestModel = new CategoriesListRequestModel();
            try
            {
                categoriesListRequestModel.Prefix = Prefix;
                categoriesListRequestModel.Zipcode = zipCode;
                categoriesListResponseModel = await serviceHelpers.MultiPartCategoryList(categoriesListRequestModel, Constants.GetCategoryList, true, Constants.token);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return categoriesListResponseModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SearchContractors"></param>
        /// <returns></returns>
        public async Task<List<SearchContractorsListResponse>> SearchContractors(SearchContractorsListRequest searchContractorsListRequest)
        {
            List<SearchContractorsListResponse> SearchContractorsListResponse = new List<SearchContractorsListResponse>();
            try
            {
                string Serializeobject = JsonConvert.SerializeObject(searchContractorsListRequest);
                var Response = await serviceHelpers.PostRequestContractor(Serializeobject, Constants.SearchContractorList, true, Constants.token);
                SearchContractorsListResponse = Response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return SearchContractorsListResponse;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserWorkStatus"></param>
        /// <returns></returns>
        public async Task<UserWorkStatusResponseModel> UserWorkStatus(UserWorkStatusRequestModel userWorkStatusRequestModel)
        {
            UserWorkStatusResponseModel UserWorkStatusResponseModel = new UserWorkStatusResponseModel();
            try
            {
                string Serializeobject = JsonConvert.SerializeObject(userWorkStatusRequestModel);
                UserWorkStatusResponseModel response = await serviceHelpers.PostRequestFilter(Serializeobject, Constants.AccountWorkStatus, false, Constants.token);
                UserWorkStatusResponseModel = response;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return UserWorkStatusResponseModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateContractorImageRequestModel"></param>
        /// <returns></returns>
        public async Task<UpdateContractorImageResponseModel> UpdateImage(UpdateContractorImageRequestModel updateContractorImageRequestModel )
        {
            UpdateContractorImageResponseModel updateContractorImageResponseModel = new UpdateContractorImageResponseModel();
            try
            {
                string Serializeobject = JsonConvert.SerializeObject(updateContractorImageRequestModel);
                var response = await serviceHelpers.PostRequestFor(Serializeobject, Constants.BaseUrl, Constants.ContractorReview, true, Constants.token);
                if (response.error)
                {
                    updateContractorImageResponseModel = JsonConvert.DeserializeObject<UpdateContractorImageResponseModel>(response.data);
                };              
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex); 
            }
            return updateContractorImageResponseModel;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contractorProfileDetailsRequest"></param>
        /// <returns></returns>
        public async Task<ContractorProfileDetailsResponse> ContractorProfile(ContractorProfileDetailsRequest contractorProfileDetailsRequest)
        {
            ContractorProfileDetailsResponse contractorProfileDetailsResponse = new ContractorProfileDetailsResponse();
            try
            {
                string Serializeobject = contractorProfileDetailsRequest.Id;
                var response = await ServiceHelpers.PostRequest1(Serializeobject, APIHttpHelper.BaseUrl, Constants.GetBusinessProfileDetail, true, Constants.token);
                if (response.error)
                {
                    contractorProfileDetailsResponse = JsonConvert.DeserializeObject<ContractorProfileDetailsResponse>(response.data);
                };
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return contractorProfileDetailsResponse;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<GetUserByIdResponseModel> UserById(string Id)
        {
            GetUserByIdResponseModel GetUserByIdResponseModel = new GetUserByIdResponseModel();
            try
            {
                string serviceUrl = string.Empty;
                serviceUrl = Constants.GetUserById;
                string Serializeobject = Id;               
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return GetUserByIdResponseModel;
        }
        /// <summary>
        /// This task is used to send request for fogot password
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            HttpResponseMessage forgotPasswordResponseModel = new HttpResponseMessage();           
            MultipartFormDataContent dataContent = null;
            try
            {
                dataContent = new MultipartFormDataContent();
                dataContent.Add(new StringContent(forgotPasswordRequestModel.Email.ToString()), nameof(forgotPasswordRequestModel.Email));
                forgotPasswordResponseModel = await ServiceHelpers.PostMultipartRequest(dataContent, Constants.ForgotPassword);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return forgotPasswordResponseModel;
        }
        /// <summary>
        /// This task is used to reset password request with new password
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<HttpResponseMessage> ResetPassword(ResetPasswordRequestModel requestModel)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                var Content = await Task.Run(() => JsonConvert.SerializeObject(requestModel));
                httpResponseMessage = await ServiceHelpers.PostRequestGood(Content, Constants.ResetPassword, true, Constants.ResetPasswordToken);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> GetStateList()
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                 httpResponseMessage = await ServiceHelpers.GetRequest(null, Constants.BaseUrl+Constants.GetStateList, false);             
            }
            
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> GetCityList(GetCityListRequestModel requestModel)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {               
                MultipartFormDataContent dataContent = null;               
                    dataContent = new MultipartFormDataContent();
                    dataContent.Add(new StringContent(requestModel.Id), nameof(GetCityListRequestModel.Id));
                    httpResponseMessage = await ServiceHelpers.PostMultipartRequest(dataContent, Constants.GetCityList);                
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> GetZipListByCityName(GetZipListByCitNameRequestModel requestModel)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                MultipartFormDataContent dataContent = null;
                dataContent = new MultipartFormDataContent();
                dataContent.Add(new StringContent(requestModel.CityName), nameof(GetZipListByCitNameRequestModel.CityName));
                httpResponseMessage = await ServiceHelpers.PostMultipartRequest(dataContent, Constants.GetZipCodeListByCityName);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return httpResponseMessage;
        }
        public async Task<HttpResponseMessage> UpdateAccountBasicInfo(UpdateBasicInfoRequestModel requestModel)
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                var Content = await Task.Run(() => JsonConvert.SerializeObject(requestModel));
                httpResponseMessage = await ServiceHelpers.PostRequestGood(Content,Constants.AcoountUpdateBasicInfo, false,string.Empty);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetActivePlanDetail(GetActivePlanRequestModel requestModel)
        {
            HttpResponseMessage httpResponseMessage = null;
            MultipartFormDataContent dataContent = null;
            try
            {                       
                dataContent = new MultipartFormDataContent();
                dataContent.Add(new StringContent(requestModel.UserId), nameof(GetActivePlanRequestModel.UserId));
                httpResponseMessage = await ServiceHelpers.PostMultipartRequest(dataContent, Constants.GetActivePlanDetail);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }          
            return httpResponseMessage;
        }
    }

}
