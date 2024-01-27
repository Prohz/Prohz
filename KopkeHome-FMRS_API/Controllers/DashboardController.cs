using KopkeHome_ModelLayer.DataModel;
using KopkeHome_ModelLayer.ViewModels;
using KopkeHome_ModelLayer.ViewModels.DashboardViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KopkeHome_FMRS_API.Controllers
{

    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("[controller]/[action]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly IDashboard _service;
        private readonly IAccount _Userservice;
        public DashboardController(IDashboard service, IAccount Userservice, ILogger<DashboardController> logger)
        {
            _service = service;
            _logger = logger;
            _Userservice = Userservice;
        }

        /// <summary>
        /// Gets the list of contractors by zipcode and category
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IEnumerable<ContractorListViewModel>> SearchContractorsList(SearchContractorsViewModel model)
        {
            try
            {

                var List = await _service.SearchContractorsList(model);
                return List;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets contractors by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ContractorProfileDetailsViewModel> GetContractorProfileDetails([FromForm] string Id)
        {
            try
            {
                var ConvertedId = Convert.ToInt32(Id);

                var FullDetails = await _service.GetContractorProfileDetails(ConvertedId);
                return FullDetails;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        ///// <summary>
        ///// Gets list of contractors.
        ///// </summary>
        ///// <returns></returns>
        [HttpGet]
        public async Task<User> GetContractors()
        {
            try
            {

                return null;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }

        /// <summary>
        /// Gets categories list
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        [HttpPost]

        public async Task<List<Categories>> GetCategoriesList([FromForm] string Prefix, [FromForm] string zipcode)
        {
            try
            {

                var Categories = await _service.GetCategoriesList(Prefix, zipcode);
                return Categories;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }



        /// <summary>
        /// Gets zip codes list
        /// </summary>
        /// <param name="Prefix"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<List<ZipCode>> GetZipCodeList([FromForm] string Prefix, [FromForm] string UserID)
        {
            try
            {

                var ZipCodeList = await _service.GetZipCodeList(Prefix, Convert.ToInt32(UserID));
                return ZipCodeList;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }

        }
       
        [HttpPost]
        public async Task<ContractorsReviewViewModel> ContractorsReview(ContractorsReviewViewModel model)
        {
            try
            {

                return await _service.ContractorsReview(model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;

            }
        }
    }
}
