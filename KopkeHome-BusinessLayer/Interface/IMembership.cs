using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.APIRequestModels;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IMembership
    {
        Task<List<MembershipBenifitsPlan>> GetMembershipPlans();
        Task<MembershipBenifitsPlan> GetMembershipPlansById(int Id);
        Task<CustomZipcodesRequest> SaveCustomZipcodeRequest(CustomZipcodesRequest model);
        Task<List<GetZipCodesByCityNameViewModel>> GetZipCodesByCityName(string CityName);
        Task<Response> AddZipcodeAndCategories(MembershipZipcodesAndCategoriesRequestModel model);
        Task<CustomZipcodesRequest> UpdateCustomZipcodeRequest(CustomZipcodesRequest model);
        Task<CustomZipcodesRequest> GetCustomPlanDetailsByUserId(int UserId);
    }
}
