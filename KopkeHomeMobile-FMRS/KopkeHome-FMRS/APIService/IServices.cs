using KopkeHome_FMRS.Models;
using KopkeHome_FMRS.Models.RequestModels;
using KopkeHome_FMRS.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KopkeHome_FMRS.ServiceHelper
{
    public interface IServices
    {
        Task<ResponseModel> UserLogin(LoginRequestMode loginRequestModel);
        Task<HttpResponseMessage> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel);
        Task<List<ZipCodeListResponseModel>> ZipCodeList(string Prefix, string UserID);
        Task<List<CategoriesListResponseModel>> CategoryList(string Prefix, int zipCode);
        Task<List<SearchContractorsListResponse>> SearchContractors(SearchContractorsListRequest searchContractorsListRequest);
        Task<UserWorkStatusResponseModel> UserWorkStatus(UserWorkStatusRequestModel userWorkStatusRequestModel);
        Task<GetUserByEmailResponseModel> GetUserbyEmail(string Email);
        Task<GetUserByIdResponseModel> UserById(string Id);
        Task<UpdateContractorImageResponseModel> UpdateImage(UpdateContractorImageRequestModel updateContractorImageRequestModel );
        Task<ContractorProfileDetailsResponse> ContractorProfile(ContractorProfileDetailsRequest contractorProfileDetailsRequest);
        Task<HttpResponseMessage> ResetPassword(ResetPasswordRequestModel resetPasswordRequestModel);
        Task<HttpResponseMessage> GetStateList();
        Task<HttpResponseMessage> GetCityList(GetCityListRequestModel requestModel);
        Task<HttpResponseMessage> GetZipListByCityName(GetZipListByCitNameRequestModel requestModel);
        Task<HttpResponseMessage> UpdateAccountBasicInfo(UpdateBasicInfoRequestModel requestModel);
        Task<HttpResponseMessage> GetActivePlanDetail(GetActivePlanRequestModel requestModel);
    }
}
