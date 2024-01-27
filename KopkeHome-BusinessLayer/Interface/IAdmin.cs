using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.AdminViewModels;

namespace KopkeHome_BusinessLayer.Interface
{
    public interface IAdmin
    {
        Task<List<CustomMembershipPlanRequestViewModel>> CustomMembershipPlanList();
        List<DocumentsVerificationViewModels> GetDocumntsListForVerification();

        Task<Response> UpdateDocumentVerification(DocumentsVerificationStatusUpdateViewModel model);

        Task<CustomZipcodesRequest> GetCustomReqById(int Id);
        Task<CustomZipcodesRequest> GetCustomReqByUserId(int Id);
        Task<Response> AddFAQ(FAQ model);
        Task<Response> DeleteFAQ(int Id);
        Task<FAQ> GetFAQById(int Id);

        Task<Response> UpdateFAQ(FAQ model);
        Task<List<FAQ>> GetAllFAQ();


        //Categories
        Task<Response> AddCategory(Categories model);
        Task<Response> DeleteCategory(int Id);
        Task<Categories> GetCategoryById(int Id);
        Task<Response> UpdateCategory(Categories model);
        Task<List<Categories>> GetAllCategories();

        Task<Response> AddPromoVideos(PromoVideos model);
        Task<Response> AddProhzLegalFiles(ProhzLegalFiles model);
        Task<List<ProhzLegalFiles>> GetAllProhzLegalFiles();

        Task<Response> DeletePromoVideo(int Id);
        Task<PromoVideos> GetPromoVideoById(int Id);

        Task<Response> UpdatePromoVideo(PromoVideos model);
        Task<List<PromoVideos>> GetAllPromoVideos();
        Task<AdminDashboardViewModel> GetDashboardStatus();
        Task<List<ProhzSalesAssciates>> GetAllProhzSalesPerson();


    }
}
