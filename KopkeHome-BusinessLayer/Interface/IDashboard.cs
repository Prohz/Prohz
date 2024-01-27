using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IDashboard
    {

        Task<List<User>> GetContractors();

        Task<List<ContractorListViewModel>> SearchContractorsList(SearchContractorsViewModel model);

        Task<List<Categories>> GetCategoriesList(string Prefix, string zipcode);

        Task<List<ZipCode>> GetZipCodeList(string Prefix, int UserID);

        Task<ContractorProfileDetailsViewModel> GetContractorProfileDetails(int Id);
        Task<ContractorsReviewViewModel> ContractorsReview(ContractorsReviewViewModel model);
    }
}
