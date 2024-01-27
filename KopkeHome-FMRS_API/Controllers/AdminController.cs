using KopkeHome_DataAccessLayer.Repository;
using KopkeHome_ModelLayer;
using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.DataModel.MembershipBenifits;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.AdminViewModels;
using KopkeHome_UtilityLayer;
using Microsoft.AspNetCore.Mvc;

namespace KopkeHome_FMRS_API.Controllers
{
#nullable disable

    [Route("[controller]/[action]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAccount service;
        private readonly IMembership _membership;
        private readonly IAdmin _AdminService;
        private readonly IEmailService _email;
        private readonly IRepository<VerifyOTP> _OtpRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountController> _logger;
        public AdminController(IMembership membership, IAdmin AdminService, IConfiguration configuration, IAccount _service, IEmailService email, IRepository<VerifyOTP> OtpRepository, ILogger<AccountController> logger)
        {
            _membership = membership;
            service = _service;
            _email = email;
            _OtpRepository = OtpRepository;
            _logger = logger;
            _configuration = configuration;
            _AdminService = AdminService;
        }

        /// <summary>
        /// Gets list of custom plan requests
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<List<CustomMembershipPlanRequestViewModel>> CustomMembershipPlanRequestsList()
        {
            try
            {

                var CustomMembershipPlanRequestsList = await _AdminService.CustomMembershipPlanList();
                return CustomMembershipPlanRequestsList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets List of documnets for verification
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<DocumentsVerificationViewModels>> GetDocumntsListForVerification()
        {
            try
            {

                var CustomMembershipPlanRequestsList = _AdminService.GetDocumntsListForVerification();
                return CustomMembershipPlanRequestsList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Updates documents.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdateDocumentVerification(DocumentsVerificationStatusUpdateViewModel model)
        {
            try
            {

                return await _AdminService.UpdateDocumentVerification(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// Adds FAQ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> AddFAQ(FAQ model)
        {
            try
            {

                return await _AdminService.AddFAQ(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// Updates FAQ
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdateFAQ(FAQ model)
        {
            try
            {

                return await _AdminService.UpdateFAQ(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// deletes FAQ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> DeleteFAQ([FromForm] string Id)
        {
            try
            {

                return await _AdminService.DeleteFAQ(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// Gets custom request by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CustomZipcodesRequest> GetCustomReqById([FromForm] string Id)
        {
            try
            {

                return await _AdminService.GetCustomReqById(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets custom memebership reuqest by user id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<CustomZipcodesRequest> GetCustomReqByUserId([FromForm] string Id)
        {
            try
            {

                return await _AdminService.GetCustomReqByUserId(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets FAQ by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<FAQ> GetFAQById([FromForm] string Id)
        {
            try
            {

                return await _AdminService.GetFAQById(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets all FAQ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<FAQ>> GetAllFAQ()
        {
            try
            {

                return await _AdminService.GetAllFAQ();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }



        #region PromoVideos

        /// <summary>
        /// Gets video by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PromoVideos> GetPromoVideoById([FromForm] string Id)
        {
            try
            {

                return await _AdminService.GetPromoVideoById(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets all promo video
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<PromoVideos>> GetAllPromoVideos()
        {
            try
            {

                return await _AdminService.GetAllPromoVideos();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// saves promo video
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> AddLegalFiles(ProhzLegalFiles model)
        {
            try
            {

                return await _AdminService.AddProhzLegalFiles(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// saves promo video
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> AddPromoVideo(PromoVideos model)
        {
            try
            {

                return await _AdminService.AddPromoVideos(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// updates promo video
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdatePromoVideo(PromoVideos model)
        {
            try
            {

                return await _AdminService.UpdatePromoVideo(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// deletes promo video
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> DeletePromoVideo([FromForm] string Id)
        {
            try
            {

                return await _AdminService.DeletePromoVideo(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        #endregion
        /// <summary>
        /// Gets Admin dashboard info
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AdminDashboardViewModel> GetDashboardStatus()
        {
            try
            {

                return await _AdminService.GetDashboardStatus();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// Gets list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ProhzLegalFiles>> GetAllProhzLegalFiles()
        {
            try
            {

                return await _AdminService.GetAllProhzLegalFiles();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        
        /// <summary>
        /// Gets list of users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<User>> GetUserList()
        {
            try
            {

                return await service.GetUserList();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        #region Categories

        /// <summary>
        /// Gets category by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Categories> GetCategoryById([FromForm] string Id)
        {
            try
            {

                return await _AdminService.GetCategoryById(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// Gets all categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Categories>> GetAllCategories()
        {
            try
            {

                return await _AdminService.GetAllCategories();


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        /// <summary>
        /// saves category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> AddCategory(Categories model)
        {
            try
            {

                return await _AdminService.AddCategory(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// updates category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> UpdateCategory(Categories model)
        {
            try
            {

                return await _AdminService.UpdateCategory(model);


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
        /// <summary>
        /// deletes category
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Response> DeleteCategory([FromForm] string Id)
        {
            try
            {

                return await _AdminService.DeleteCategory(Convert.ToInt32(Id));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }


        #endregion

        /// <summary>
        /// gets all sales persons name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<ProhzSalesAssciates>> GetAllProhzSalesPerson()
        {
            try
            {
                return await _AdminService.GetAllProhzSalesPerson();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }

        }

    }
}
